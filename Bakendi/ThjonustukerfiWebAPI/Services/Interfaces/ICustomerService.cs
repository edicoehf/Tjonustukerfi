using System.Collections;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    /// <summary>Service functions for Customer.</summary>
    public interface ICustomerService
    {
        /// <summary>Creates a new customer.</summary>
        /// <returns>A customer DTO of the customer that was just created.</returns>
        long CreateCustomer(CustomerInputModel customer);

        /// <summary>Gets a customer with the given ID.</summary>
        /// <returns>A detailed customer DTO.</returns>
        CustomerDetailsDTO GetCustomerById(long id);

        /// <summary>Updates a customer with the given ID and input.</summary>
        void UpdateCustomerDetails(CustomerInputModel customer, long id);
        void UpdateCustomerEmail(CustomerEmailInputModel customer, long id);

        /// <summary>Deletes a customer with the given ID.</summary>
        /// <returns>An empty list if person is deleted, else list of active orders for that customer.</returns>
        List<OrderDTO> DeleteCustomerById(long id);

        /// <summary>Gets all customers.</summary>
        /// <returns>A list of all customers.</returns>
        IEnumerable GetAllCustomers();

        /// <summary>Deletes customer and all of their active orders</summary>
        void DeleteCustomerByIdAndOrders(long customerId);

        /// <summary>Gets all orders ready to be picked up by customer ID</summary>
        /// <returns>List of OrderDTO, empty list if none exist</returns>
        List<OrderDTO> GetPickupOrdersByCustomerId(long customerId);

        /// <summary>Gets all orders (not archived) by customer ID</summary>
        /// <returns>List of OrderDTO, empty list if none exist</returns>
        List<OrderDTO> GetOrdersByCustomerId(long customerId);
    }
}