import React from "react";
import { orderFormValuesType, orderFormFunctionsType } from "../../../../types";
import useGetCategories from "../../../../hooks/useGetCategories";
import useGetServices from "../../../../hooks/useGetServices";
import AddItems from "../AddItems/AddItems";
import ViewItems from "../ViewItems/ViewItems";
import AddCustomer from "../AddCustomer/AddCustomer";
import "./OrderForm.css";

const OrderForm = ({ values, functions }) => {
    const { items, customer } = values;
    const { addItems, removeItem, addCustomer } = functions;
    const { services } = useGetServices();
    const { categories } = useGetCategories();

    return (
        <div className="order-form">
            <AddItems
                addItems={addItems}
                categories={categories}
                services={services}
            />
            <ViewItems items={items} remove={removeItem} />
            <AddCustomer customer={customer} addCustomer={addCustomer} />
        </div>
    );
};

OrderForm.propTypes = {
    values: orderFormValuesType,
    functions: orderFormFunctionsType,
};

export default OrderForm;
