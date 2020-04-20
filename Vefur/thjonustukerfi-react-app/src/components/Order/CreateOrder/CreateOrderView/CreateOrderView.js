import React from "react";
import AddItems from "../AddItems/AddItems";
import ViewItems from "../ViewItems/ViewItems";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import AddCustomer from "../AddCustomer/AddCustomer";
import "./CreateOrderView.css";
import CreateOrderActions from "../OrderActions/CreateOrderActions";
import useCreateOrder from "../../../../hooks/useCreateOrder";
import orderValidate from "../OrderValidate/OrderValidate";

const CreateOrderView = () => {
    const [items, setItems] = React.useState([]);
    const [customer, setCustomer] = React.useState(null);
    const [errors, setErrors] = React.useState({});

    const { services } = useGetServices();
    const { categories } = useGetCategories();
    const { error: sendError, handleCreate, isProcessing } = useCreateOrder();

    const addItems = (newItem, cb) => {
        newItem.serviceName =
            services[
                services.findIndex((s) => s.id.toString() === newItem.service)
            ].name;
        newItem.categoryName =
            categories[
                categories.findIndex(
                    (c) => c.id.toString() === newItem.category
                )
            ].name;
        const ids = items.map((item) => item.id);
        newItem.id = ids.reduce((acc, curr) => Math.max(acc, curr), 0) + 1;
        setItems([...items, newItem]);
        cb();
    };

    const removeItem = (itemToRemove) => {
        setItems(items.filter((item) => itemToRemove.id !== item.id));
    };

    const addCustomer = (customer) => {
        setCustomer(customer);
    };

    const createOrder = () => {
        const err = orderValidate(customer, items);
        setErrors(err);
        if (!isProcessing && Object.keys(err).length === 0) {
            console.log("INSIDE");
            let order = {
                customerId: null,
                items: [],
            };
            order.customerId = customer.id;
            items.forEach((item) => {
                for (var i = 0; i < item.amount; i++) {
                    console.log(item);
                    order.items.push({
                        categoryId: item.category,
                        serviceId: item.service,
                    });
                }
            });
            console.log(order);
            handleCreate({
                customerId: 14,
                items: [
                    {
                        categoryId: 3,
                        serviceId: 2,
                    },
                ],
            });
            if (!sendError) {
                setCustomer(null);
                setItems([]);
            }
        }
    };

    const cancelOrder = () => {
        setCustomer(null);
        setItems([]);
    };
    return (
        <div className="create-order-view-container">
            <div className="create-order-view">
                <AddItems
                    addItems={addItems}
                    categories={categories}
                    services={services}
                />
                <ViewItems items={items} remove={removeItem} />
                <AddCustomer customer={customer} addCustomer={addCustomer} />
            </div>
            {sendError && <p className="error">Gat ekki sent inn pöntun</p>}
            {errors.category && <p className="error">{errors.category}</p>}
            {errors.service && <p className="error">{errors.service}</p>}
            <CreateOrderActions
                createOrder={createOrder}
                cancelOrder={cancelOrder}
            />
        </div>
    );
};

export default CreateOrderView;
