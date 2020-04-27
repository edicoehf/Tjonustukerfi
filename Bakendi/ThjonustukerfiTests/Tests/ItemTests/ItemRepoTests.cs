using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiTests.Tests.ItemTests
{
    [TestClass]
    public class ItemRepoTests
    {
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ThjonuskerfiDB-Item")
                .EnableSensitiveDataLogging()
                .Options;
            
            FillDatabase(); // Fills database and updates the mapper with the current context
        }

        [TestCleanup]
        public void Cleanup()
        {
            ClearDatabase();
            _mapper = null;
            _options = null;
        }

        [TestMethod]
        public void Fill_database_should_have_an_inMemory_database_ready()
        {
            //*Arrange
            using(var mockContext = new DataContext(_options))
            {
                //* Act

                //* Assert
                Assert.IsNotNull(mockContext);
                Assert.IsTrue(mockContext.Order.Any());
                Assert.IsTrue(mockContext.Customer.Any());
                Assert.IsTrue(mockContext.ItemOrderConnection.Any());
                Assert.IsTrue(mockContext.Item.Any());
                Assert.IsTrue(mockContext.ItemTimestamp.Any());
                Assert.IsTrue(mockContext.Service.Any());
                Assert.IsTrue(mockContext.State.Any());
            }
        }

        [TestMethod]
        public void SearchItems_should_return_the_correct_ID()
        {
            //* Arrange
            string barcodeToSearch = "50500002";
            using(var mockContext = new DataContext(_options))
            {
                // Create repo
                var itemRepo = new ItemRepo(mockContext, _mapper);
                // Get correct entity
                var correctEntity = mockContext.Item.FirstOrDefault(i => i.Barcode == barcodeToSearch);

                //* Act
                var returnValue = itemRepo.SearchItem(barcodeToSearch);

                //* Assert
                Assert.IsNotNull(returnValue);
                Assert.IsInstanceOfType(returnValue, typeof(long));
                Assert.AreEqual(correctEntity.Id, returnValue);
            }
        }

        [TestMethod]
        public void SearchItems_should_throw_NotFoundException()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                var itemRepo = new ItemRepo(mockContext, _mapper);

                string inp = "This should never work as a barcode I would think...";

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.SearchItem(inp));
            }
        }

        [TestMethod]
        public void EditItem_should_edit_correct_items()
        {
            //* Arrange
            long categoryId = 1;
            long stateID = 3;
            long serviceID = 2;
            long orderID = 100;
            long itemID = 1;
            long orderChange = 101;
            var itemInput = new EditItemInput
            {
                CategoryId = categoryId,
                StateId = stateID,
                ServiceID = serviceID,
                OrderId = orderChange
            };

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // get everything before change
                // order:
                var oldconnections1 = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList1 = new List<Item>();
                foreach (var item in oldconnections1)
                {
                    oldItemList1.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                // note: there are two orders because we are also changing the order id in this update
                var oldOrder1ListSize = oldItemList1.Count;
                var oldOrder2ListSize = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderChange).Count();

                // number of timestamps of the item before change
                var oldTimestampCount = mockContext.ItemTimestamp.Where(ts => ts.ItemId == itemID).Count();

                // item:
                var trackedEntity = oldItemList1.FirstOrDefault(i => i.Id == itemID);
                var oldItemEntity = new Item()  // Copying item values (not the reference to the object)
                {
                    Id = trackedEntity.Id,
                    CategoryId = trackedEntity.CategoryId,
                    StateId = trackedEntity.StateId,
                    ServiceId = trackedEntity.ServiceId,
                    Barcode = trackedEntity.Barcode,
                    JSON = trackedEntity.JSON,
                    DateCreated = trackedEntity.DateCreated,
                    DateModified = trackedEntity.DateModified,
                    DateCompleted = trackedEntity.DateCompleted
                };

                //* Act
                itemRepo.EditItem(itemInput, itemID);

                // Should be updated order
                var newconnections1 = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> newItemList1 = new List<Item>();
                foreach (var item in newconnections1)
                {
                    newItemList1.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                var newOrder1ListSize = newItemList1.Count;

                // Item
                var changedItem = mockContext.Item.FirstOrDefault(i => i.Id == itemID);
                var newOrder2ListSize = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderChange).Count();

                // number of timestamps after change
                var newTimestampCount = mockContext.ItemTimestamp.Where(ts => ts.ItemId == itemID).Count();

                //* Assert
                Assert.IsNotNull(changedItem);
                Assert.AreNotEqual(oldItemEntity, changedItem);             // items have been updated
                Assert.AreEqual(oldOrder1ListSize - 1, newOrder1ListSize);  // has been removed from the order
                Assert.AreEqual(oldOrder2ListSize + 1, newOrder2ListSize);  // has been moved to other list

                // Check that item properties have been updated correctly
                Assert.AreEqual(categoryId, changedItem.CategoryId);
                Assert.AreEqual(stateID, changedItem.StateId);
                Assert.AreEqual(serviceID, changedItem.ServiceId);
                Assert.AreEqual(orderChange, mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == changedItem.Id).OrderId);

                // check that there is one new timestamp
                Assert.AreEqual(oldTimestampCount + 1, newTimestampCount);
            }
        }

        [TestMethod]
        public void EditItem_should_throw_correct_exceptions()
        {
            //* Arrange
            // All these inputs should throw exceptions, but for different reasons
            var input1 = new EditItemInput { OrderId = null };
            var input2 = new EditItemInput { StateId = -1 };
            var input3 = new EditItemInput { ServiceID = -1 };
            var input4 = new EditItemInput { OrderId = -1 };
            var input5 = new EditItemInput { CategoryId = -1 };

            long itemID = 2;

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<BadRequestException>(() => itemRepo.EditItem(input1, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input2, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input3, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input4, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input5, itemID));
            }
        }

        [TestMethod]
        public void CompleteItem_should_update_entity_to_done_state()
        {
            //* Arrange
            long itemID = 1;

            using(var mockContext = new DataContext(_options))
            {
                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var oldStateId = mockContext.Item.FirstOrDefault(i => i.Id == itemID).StateId;
                var oldTimestampSize = mockContext.ItemTimestamp.Where(ts => ts.ItemId == itemID).Count();

                //* Act
                itemRepo.CompleteItem(itemID);

                //* Assert
                var itemEntity = mockContext.Item.FirstOrDefault(i => i.Id == itemID);

                Assert.IsNotNull(itemEntity);
                Assert.AreNotEqual(oldStateId, itemEntity.StateId);
                Assert.AreEqual(5, itemEntity.StateId); //TODO: same as in the method in repo, too hardcoded state as five. Change later
                Assert.AreEqual(oldTimestampSize + 1, mockContext.ItemTimestamp.Where(ts => ts.ItemId == itemID).Count());  // did we add one more timestamp?
            }
        }

        [TestMethod]
        public void CompleteItem_should_throw_NotFoundException()
        {
            //* Arrange
            long itemID = -1;

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.CompleteItem(itemID));
            }
        }

        [TestMethod]
        public void GetItemById_should_return_itemStateDTO()
        {
            //* Arrange
            long itemID = 1;
            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var itemEntity = mockContext.Item.FirstOrDefault(i => i.Id == itemID);
                
                // Same item the function should find
                var itemDTO = _mapper.Map<ItemStateDTO>(itemEntity);
                itemDTO.OrderId = mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemEntity.Id).OrderId;
                itemDTO.State = mockContext.State.FirstOrDefault(s => s.Id == itemEntity.StateId).Name;
                itemDTO.Category = mockContext.Category.FirstOrDefault(c => c.Id == itemEntity.CategoryId).Name;
                itemDTO.Service = mockContext.Service.FirstOrDefault(s => s.Id == itemEntity.ServiceId).Name;

                //* Act
                var retVal = itemRepo.GetItemById(itemID);

                //* Assert
                Assert.IsNotNull(retVal);
                Assert.IsInstanceOfType(retVal, typeof(ItemStateDTO));
                Assert.AreEqual(itemDTO, retVal);
            }
        }

        [TestMethod]
        public void GetItemById_should_throw_NotFoundException()
        {
            //* Arrange
            long itemID = -1;
            using(var mockContext = new DataContext(_options))
            {
                var itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.GetItemById(itemID));
            }
        }

        [TestMethod]
        public void RemoveItem_should_remove_a_single_item()
        {
            //* Arrange
            long orderID = 100;
            long itemID = 1;

            using(var mockContext = new DataContext(_options))
            {
                // Create repo
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var itemOrderConnections = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                var oldItemOrderSize = itemOrderConnections.Count;              // Item order connection size before remove
                var oldTimestampSize = mockContext.ItemTimestamp.Where(ts => ts.ItemId == itemID).Count();  // number of timestamps that are related to this item
                var oldTimestampTableSize = mockContext.ItemTimestamp.Count();  // size of timestamp table before delete
                
                var oldItemList = new List<Item>();
                foreach (var item in itemOrderConnections)  // get the list of items
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                var oldItemListCount = oldItemList.Count;   // size of the list before remove

                //* Act
                itemRepo.RemoveItem(itemID);

                itemOrderConnections = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                var newItemOrderSize = itemOrderConnections.Count;  // Item order connection size after remove

                var newItemList = new List<Item>();
                foreach (var item in itemOrderConnections)
                {
                    newItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                var newItemListCount = newItemList.Count;           // Item list size after remove

                //* Assert
                Assert.AreEqual(oldItemOrderSize - 1, newItemOrderSize);    // check the item order connections list
                Assert.AreEqual(oldItemListCount - 1, newItemListCount);    // check the size of the item list itself
                Assert.AreEqual(oldTimestampTableSize - oldTimestampSize, mockContext.ItemTimestamp.Count());   // did we remove the correct amount of timestamps?
            }
        }

        [TestMethod]
        public void RemoveItem_should_throw_NotFoundException()
        {
            //* Arrange
            long itemID = -1;

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.RemoveItem(itemID));
            }
        }

        [TestMethod]
        public void ChangeItemStateByIdScanner_should_change_state_of_item_to_2_and_order_connected_to_it_should_not_have_completeDate()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                // This item is the only Item in the order at this moment
                long itemId = 2;
                long statechangeID = 2;
                // constructing the barcode string
                string stateChange = $"{mockContext.State.FirstOrDefault(s => s.Id == statechangeID).Name}-hilla1A";

                var input = new List<ItemStateChangeInputIdScanner>()
                {
                    new ItemStateChangeInputIdScanner
                    {
                        ItemId = itemId,
                        StateChangeBarcode = stateChange
                    }
                };

                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // Get old state
                var oldItemState = mockContext.Item.FirstOrDefault(i => i.Id == itemId).StateId;

                //* Act
                itemRepo.ChangeItemStateByIdScanner(input);

                //* Assert
                var item = mockContext.Item.FirstOrDefault(i => i.Id == itemId);    // get item to check

                Assert.IsNotNull(item);
                Assert.AreNotEqual(oldItemState, item.StateId);
                Assert.AreEqual(statechangeID, item.StateId);

                // check order connected
                var order = mockContext.Order.FirstOrDefault(o =>
                    o.Id == mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId).OrderId);

                Assert.IsNotNull(order);
                Assert.IsNull(order.DateCompleted); // since the order is not complete, it should not have a date completed
            }
        }

        [TestMethod]
        public void ChangeItemStateByIdScanner_should_change_state_of_item_to_5_and_order_connected_to_it_should_have_completeDate()
        {
            //* Arrange

            using(var mockContext = new DataContext(_options))
            {
                long itemId1 = 1;
                long itemId2 = 2;
                long statechangeID = 5;
                // Create the barcode string
                string stateChange = $"{mockContext.State.FirstOrDefault(s => s.Id == statechangeID).Name}-hilla1A";

                // change both items in order to complete
                var input = new List<ItemStateChangeInputIdScanner>()
                {
                    new ItemStateChangeInputIdScanner
                    {
                        ItemId = itemId1,
                        StateChangeBarcode = stateChange
                    },
                    new ItemStateChangeInputIdScanner
                    {
                        ItemId = itemId2,
                        StateChangeBarcode = stateChange
                    }
                };

                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // get old states
                var oldItemState1 = mockContext.Item.FirstOrDefault(i => i.Id == itemId1).StateId;
                var oldItemState2 = mockContext.Item.FirstOrDefault(i => i.Id == itemId2).StateId;

                //* Act
                itemRepo.ChangeItemStateByIdScanner(input);

                //* Assert
                // get items to check
                var item1 = mockContext.Item.FirstOrDefault(i => i.Id == itemId1);
                var item2 = mockContext.Item.FirstOrDefault(i => i.Id == itemId2);

                Assert.IsNotNull(item1);
                Assert.AreNotEqual(oldItemState1, item1.StateId);
                Assert.AreEqual(statechangeID, item1.StateId);

                Assert.IsNotNull(item2);
                Assert.AreNotEqual(oldItemState2, item2.StateId);
                Assert.AreEqual(statechangeID, item2.StateId);

                // check order connected
                var order = mockContext.Order.FirstOrDefault(o =>
                    o.Id == mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId1).OrderId);

                Assert.IsNotNull(order);
                Assert.IsNotNull(order.DateCompleted);  // now the order should be complete
            }
        }

        [TestMethod]
        public void ChangeItemStateByIdScanner_should_return_an_invalid_list()
        {
            //* Arrange
            long invalidId = -1;
            string invalidState = @"Í Vinnslu-{location:""hilla1A""}";

            long validId = 2;
            string validState = @"Sótt-hilla1A}";

            // The input with valid and invalid variables
            var input = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = invalidId, StateChangeBarcode = validState },
                new ItemStateChangeInputIdScanner { ItemId = validId, StateChangeBarcode = invalidState },
                new ItemStateChangeInputIdScanner { ItemId = validId, StateChangeBarcode = validState }
            };

            // Expected return from the function
            var expectedReturn = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = invalidId, StateChangeBarcode = validState },
                new ItemStateChangeInputIdScanner { ItemId = validId, StateChangeBarcode = invalidState }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                var itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act
                var returnedValue = itemRepo.ChangeItemStateByIdScanner(input);

                //* Assert
                Assert.IsNotNull(returnedValue);
                Assert.AreEqual(expectedReturn.Count, returnedValue.Count);
                foreach (var inp in returnedValue)
                {
                    Assert.IsTrue(expectedReturn.Contains(inp));
                }
            }
        }

        [TestMethod]
        public void ChangeItemStateByIdScanner_should_return_an_empty_list()
        {
            //* Arrange
            long validId = 2;
            string validState = "Sótt-hilla1A";

            // The input with valid and invalid variables
            var input = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = validId, StateChangeBarcode = validState }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                var itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act
                var returnedValue = itemRepo.ChangeItemStateByIdScanner(input);

                //* Assert
                Assert.IsNotNull(returnedValue);
                Assert.AreEqual(0, returnedValue.Count);
            }
        }

        [TestMethod]
        public void ChangeItemStateByIdScanner_should_throw_correct_exceptions()
        {
            //* Arrange
            var input1 = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = -1, StateChangeBarcode = @"Kælir1-hilla1A" }
            };
            var input2 = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = 2, StateChangeBarcode = @"Í Vinnslu-{location:""hilla1A""}" }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Mock repo
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                // The exceptions are thrown because there is no valid inputs
                Assert.ThrowsException<NotFoundException>(() => itemRepo.ChangeItemStateByIdScanner(input1));  // Invalid itemId
                Assert.ThrowsException<NotFoundException>(() => itemRepo.ChangeItemStateByIdScanner(input2));  // Invalid StateID
            }
        }

        [TestMethod]
        public void GetItemEntity_should_return_correct_entity()
        {
            //* Arrange
            long validID = 2;

            using(var mockContext = new DataContext(_options))
            {
                // Mock Repo
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var correctEntity = mockContext.Item.FirstOrDefault(i => i.Id == validID);

                //* Act
                var value = itemRepo.GetItemEntity(validID);

                //* Assert
                Assert.IsNotNull(value);
                Assert.IsInstanceOfType(value, typeof(Item));
                Assert.AreEqual(correctEntity, value);
            }
        }

        [TestMethod]
        public void GetItemEntity_should_throw_NotFoundException()
        {
            //* Arrange
            var invalidID = -1;

            using(var mockContext = new DataContext(_options))
            {
                // Mock repo
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.GetItemEntity(invalidID));
            }
        }

        [TestMethod]
        public void ChangeItemState_should_change_state_of_item_to_4_and_order_connected_to_it_should_have_completeDate_as_null()
        {
            //* Arrange

            using(var mockContext = new DataContext(_options))
            {
                // This item is the only Item in the order at this moment
                long itemId = 2;
                long statechangeID = 4;

                var input = new List<ItemStateChangeInput>()
                {
                    new ItemStateChangeInput
                    {
                        ItemId = itemId,
                        StateChangeTo = statechangeID,
                        Location = "hilla1A"
                    }
                };

                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // get old state
                var oldItemState = mockContext.Item.FirstOrDefault(i => i.Id == itemId).StateId;

                //* Act
                itemRepo.ChangeItemState(input);

                //* Assert
                var item = mockContext.Item.FirstOrDefault(i => i.Id == itemId);    // Get item to check

                Assert.IsNotNull(item);
                Assert.AreNotEqual(oldItemState, item.StateId);
                Assert.AreEqual(statechangeID, item.StateId);

                // check order connected
                var order = mockContext.Order.FirstOrDefault(o =>
                    o.Id == mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId).OrderId);

                Assert.IsNotNull(order);
                Assert.IsNull(order.DateCompleted);  // now the order is not complete
            }
        }

        [TestMethod]
        public void ChangeItemState_should_return_an_invalid_list()
        {
            //* Arrange
            long invalidId = -1;
            long invalidState = 500;

            long validId = 2;
            long validState = 5;

            string location = "hilla bara";

            // The input with valid and invalid variables
            var input = new List<ItemStateChangeInput>()
            {
                new ItemStateChangeInput { ItemId = invalidId, StateChangeTo = validState, Location = location },
                new ItemStateChangeInput { ItemId = validId, StateChangeTo = invalidState, Location = location },
                new ItemStateChangeInput { ItemId = validId, StateChangeTo = validState, Location = location }
            };

            // Expected return from the function
            var expectedReturn = new List<ItemStateChangeInput>()
            {
                new ItemStateChangeInput { ItemId = invalidId, StateChangeTo = validState, Location = location },
                new ItemStateChangeInput { ItemId = validId, StateChangeTo = invalidState, Location = location }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                var itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act
                var returnedValue = itemRepo.ChangeItemState(input);

                //* Assert
                Assert.IsNotNull(returnedValue);
                Assert.AreEqual(expectedReturn.Count, returnedValue.Count);
                foreach (var inp in returnedValue)
                {
                    Assert.IsTrue(expectedReturn.Contains(inp));
                }
            }
        }

        [TestMethod]
        public void ChangeItemState_should_return_an_empty_list()
        {
            //* Arrange
            long validId = 2;
            long validState = 5;

            // The input with valid and invalid variables
            var input = new List<ItemStateChangeInput>()
            {
                new ItemStateChangeInput { ItemId = validId, StateChangeTo = validState, Location = "lost" }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Needs an updated automapper that has access to the current instance of db context
                UpdateMapper(mockContext);

                var itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act
                var returnedValue = itemRepo.ChangeItemState(input);

                //* Assert
                Assert.IsNotNull(returnedValue);
                Assert.AreEqual(0, returnedValue.Count);
            }
        }

        [TestMethod]
        public void ChangeItemState_should_throw_correct_exceptions()
        {
            //* Arrange
            var input1 = new List<ItemStateChangeInput>()
            {
                new ItemStateChangeInput { ItemId = -1, StateChangeTo = 2, Location = "some location" }
            };
            var input2 = new List<ItemStateChangeInput>()
            {
                new ItemStateChangeInput { ItemId = 2, StateChangeTo = 20000, Location = "some other location" }
            };

            using(var mockContext = new DataContext(_options))
            {
                // Mock repo
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                // The exceptions are thrown because there is no valid inputs
                Assert.ThrowsException<NotFoundException>(() => itemRepo.ChangeItemState(input1));  // Invalid itemId
                Assert.ThrowsException<NotFoundException>(() => itemRepo.ChangeItemState(input2));  // Invalid StateID
            }
        }

        [TestMethod]
        public void GetItemBarcodeById_should_return_correct_barcode()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var item = mockContext.Item.FirstOrDefault();
                var expectedBarcode = item.Barcode;

                //* Act
                var barcode = itemRepo.GetItemBarcodeById(item.Id);

                Assert.IsNotNull(barcode);
                Assert.AreEqual(expectedBarcode, barcode);
            }
        }

        [TestMethod]
        public void GetItemBarcodeById_should_throw_NotFoundException()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.GetItemBarcodeById(-100));
            }
        }

        [TestMethod]
        public void GetOrderIdWithItemId_should_return_the_correct_orderId()
        {
            long itemID = 1;

            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // Get correct order ID
                var correctOrderID = mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemID).OrderId;

                //* Act
                var returnedID = itemRepo.GetOrderIdWithItemId(itemID);

                //* Assert
                Assert.IsNotNull(returnedID);
                Assert.AreEqual(correctOrderID, returnedID);
            }
        }

        [TestMethod]
        public void GetOrderIdWithItemId_should_throw_NotFoundException()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.GetOrderIdWithItemId(-100));
            }
        }

        //**********     Helper functions     **********//
        private void FillDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                UpdateMapper(mockContext);  // update the mapper with the new context

                //! Building Db
                // variables used
                DateTime modifiedDate = DateTime.Now;
                string serviceName = "Birkireyking";
                string serviceName2 = "Taðreyking";

                //* customer 1
                long orderId = 100;
                long customerId = 50;
                string customerName = "Viggi Siggi";
                string OrderBarCode = "20200001";
                // Mock entity
                Order mockOrder = new Order
                {
                    Id = orderId,
                    CustomerId = customerId,
                    Barcode = OrderBarCode,
                    DateCreated = DateTime.MinValue,
                    DateModified = modifiedDate,
                    DateCompleted = DateTime.MaxValue
                };

                Customer mockCustomer = new Customer
                {
                    Id = customerId,
                    Name = customerName,
                    SSN = "1308943149",
                    Email = "viggi@siggi.is",
                    Phone = "5812345",
                    Address = "Bakkabakki 1",
                    PostalCode = "800"
                };

                //* customer 2
                long orderId2 = 101;
                long customerId2 = 51;
                string customerName2 = "Kalli Valli";
                string OrderBarCode2 = "20200002";

                // Mock entity
                Order mockOrder2 = new Order
                {
                    Id = orderId2,
                    CustomerId = customerId2,
                    Barcode = OrderBarCode2,
                    DateCreated = DateTime.MinValue,
                    DateModified = modifiedDate,
                    DateCompleted = DateTime.MaxValue
                };

                Customer mockCustomer2 = new Customer
                {
                    Id = customerId2,
                    Name = customerName2,
                    SSN = "1308943149",
                    Email = "valli@kalli.is",
                    Phone = "5812345",
                    Address = "Gulligull 1",
                    PostalCode = "400"
                };

                // just for order 1
                List<ItemOrderConnection> mockIOConnect = new List<ItemOrderConnection>()
                {
                    new ItemOrderConnection
                    {
                        Id = 1,
                        ItemId = 1,
                        OrderId = orderId
                    },
                    new ItemOrderConnection
                    {
                        Id = 2,
                        ItemId = 2,
                        OrderId = orderId
                    }
                };

                // adding items
                List<Item> mockItems = new List<Item>()
                {
                    new Item
                    {
                        Id = 1,
                        CategoryId = 1,
                        StateId = 1,
                        ServiceId = 1,
                        Barcode = "50500001",
                        JSON = @"{location:""Vinnslu""}",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    },
                    new Item
                    {
                        Id = 2,
                        CategoryId = 2,
                        StateId = 1,
                        ServiceId = 1,
                        Barcode = "50500002",
                        JSON = @"{location:""Vinnslu""}",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    }
                };

                // Adding timestamp
                List<ItemTimestamp> mockTimestamps = new List<ItemTimestamp>();
                foreach (var item in mockItems)
                {
                    mockTimestamps.Add(_mapper.Map<ItemTimestamp>(item));
                }

                // Adding service
                var MockServiceList = new List<Service>()
                {
                    new Service()
                    {
                        Name = serviceName,
                        Id = 1
                    },
                    new Service()
                    {
                        Name = serviceName2,
                        Id = 2
                    }
                };

                // Build a list of size 20, make it queryable for the database mock
                var orders = Builder<Order>.CreateListOfSize(20)
                    .TheFirst(1)
                    .With(o => o.Id = mockOrder.Id)
                    .With(o => o.CustomerId = mockOrder.CustomerId)
                    .With(o => o.Barcode = mockOrder.Barcode)
                    .With(o => o.DateCreated = mockOrder.DateCreated)
                    .With(o => o.DateModified = mockOrder.DateModified)
                    .With(o => o.DateCompleted = mockOrder.DateCompleted)
                    .TheNext(1)
                    .With(o => o.Id = mockOrder2.Id)
                    .With(o => o.CustomerId = mockOrder2.CustomerId)
                    .With(o => o.Barcode = mockOrder2.Barcode)
                    .With(o => o.DateCreated = mockOrder2.DateCreated)
                    .With(o => o.DateModified = mockOrder2.DateModified)
                    .With(o => o.DateCompleted = mockOrder2.DateCompleted)
                    .TheRest()
                    .With(o => o.Barcode = "30200001")
                    .With(o => o.CustomerId = 2)
                    .Build();

                // Build a list of size 20, make it queryable for the database mock
                var customers = Builder<Customer>.CreateListOfSize(20)
                    .TheFirst(1)
                    .With(c => c.Id = mockCustomer.Id)
                    .With(c => c.Name = mockCustomer.Name)
                    .With(c => c.SSN = mockCustomer.SSN)
                    .With(c => c.Email = mockCustomer.Email)
                    .With(c => c.Phone = mockCustomer.Phone)
                    .With(c => c.Address = mockCustomer.Address)
                    .With(c => c.PostalCode = mockCustomer.PostalCode)
                    .TheNext(1)
                    .With(c => c.Id = mockCustomer2.Id)
                    .With(c => c.Name = mockCustomer2.Name)
                    .With(c => c.SSN = mockCustomer2.SSN)
                    .With(c => c.Email = mockCustomer2.Email)
                    .With(c => c.Phone = mockCustomer2.Phone)
                    .With(c => c.Address = mockCustomer2.Address)
                    .With(c => c.PostalCode = mockCustomer2.PostalCode)
                    .Build();

                // Adding states for lookup
                var states = new List<State>()
                {
                    // states fyrir Reykofninn
                    new State() {Name = "Vinnslu", Id = 1},
                    new State() {Name = "Kælir1", Id = 2},
                    new State() {Name = "Kælir2", Id = 3},
                    new State() {Name = "Frystir", Id = 4},
                    new State() {Name = "Sótt", Id = 5}
                };

                // Adding Types
                var categories = new List<Category>()
                {
                    // states fyrir reykofninn
                    new Category() {Name = "Lax", Id = 1},
                    new Category() {Name = "Silungur", Id = 2}
                };

                // Adding all entities to the in memory database
                mockContext.Order.AddRange(orders);
                mockContext.Customer.AddRange(customers);
                mockContext.ItemOrderConnection.AddRange(mockIOConnect);
                mockContext.Item.AddRange(mockItems);
                mockContext.ItemTimestamp.AddRange(mockTimestamps);
                mockContext.Service.AddRange(MockServiceList);
                mockContext.State.AddRange(states);
                mockContext.Category.AddRange(categories);
                mockContext.SaveChanges();
                //! Building DB done
            }
        }

        /// <summary>Updates the private mapper to have the current mock data context</summary>
        private void UpdateMapper(DataContext context)
        {
            // Needs a seperete automapper that has access to the current instance of db context
            var myProfile = new MappingProfile(context);   // Create a new profile like the one we implemented
            var myConfig = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));   // Setup a configuration with our profile
            _mapper = new Mapper(myConfig); // Create a new mapper with our profile
        }

        private void ClearDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                mockContext.Order.RemoveRange(mockContext.Order);
                mockContext.Customer.RemoveRange(mockContext.Customer);
                mockContext.ItemOrderConnection.RemoveRange(mockContext.ItemOrderConnection);
                mockContext.Item.RemoveRange(mockContext.Item);
                mockContext.ItemTimestamp.RemoveRange(mockContext.ItemTimestamp);
                mockContext.Service.RemoveRange(mockContext.Service);
                mockContext.State.RemoveRange(mockContext.State);
                mockContext.Category.RemoveRange(mockContext.Category);
                mockContext.SaveChanges();
            }
        }
    }
}