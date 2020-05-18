import React from "react";
import { orderFormValuesType, orderFormFunctionsType } from "../../../../types";
import useGetCategories from "../../../../hooks/useGetCategories";
import useGetServices from "../../../../hooks/useGetServices";
import AddItems from "../AddItems/AddItems";
import ViewItems from "../ViewItems/ViewItems";
import AddCustomer from "../AddCustomer/AddCustomer";
import "./OrderForm.css";
import ProgressComponent from "../../../Feedback/ProgressComponent/ProgressComponent";

/**
 * Form that handles creating a new order, selecting items, viewing selected items and adding a customer.
 *
 * @component
 * @category Order
 */
const OrderForm = ({ values, functions }) => {
    // Destruct values to get a existing items and customer
    const { items, customer } = values;
    // Destruct functions to get the functions used to add and remove items and customer
    const { addItems, removeItem, addCustomer } = functions;
    // Get available services
    const {
        services,
        error: servicesError,
        isLoading: servicesLoading,
    } = useGetServices();
    // Get available categories
    const {
        categories,
        error: categoriesError,
        isLoading: categoriesLoading,
    } = useGetCategories();

    return (
        <div className="order-form">
            {servicesLoading || categoriesLoading ? (
                <ProgressComponent
                    isLoading={servicesLoading || categoriesLoading}
                />
            ) : !servicesError && !categoriesError ? (
                <>
                    <AddItems
                        addItems={addItems}
                        categories={categories}
                        services={services}
                    />
                    <ViewItems items={items} remove={removeItem} />
                    <AddCustomer
                        customer={customer}
                        addCustomer={addCustomer}
                    />
                </>
            ) : (
                <p className="error">
                    Gat ekki sótt þær vörur og þjónustur sem í boði eru
                </p>
            )}
        </div>
    );
};

OrderForm.propTypes = {
    /** Existing items and customer, used if updating existing order, else leave empty */
    values: orderFormValuesType,
    /** Functions used to add/remove items and customer */
    functions: orderFormFunctionsType,
};

export default OrderForm;
