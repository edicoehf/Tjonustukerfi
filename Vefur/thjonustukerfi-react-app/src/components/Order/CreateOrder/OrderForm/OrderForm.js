import React from "react";
import { orderFormValuesType, orderFormFunctionsType } from "../../../../types";
import useGetCategories from "../../../../hooks/useGetCategories";
import useGetServices from "../../../../hooks/useGetServices";
import AddItems from "../AddItems/AddItems";
import ViewItems from "../ViewItems/ViewItems";
import AddCustomer from "../AddCustomer/AddCustomer";
import "./OrderForm.css";
import ProgressComponent from "../../../Feedback/ProgressComponent/ProgressComponent";

const OrderForm = ({ values, functions }) => {
    const { items, customer } = values;
    const { addItems, removeItem, addCustomer } = functions;
    const {
        services,
        error: servicesError,
        isLoading: servicesLoading,
    } = useGetServices();
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
    values: orderFormValuesType,
    functions: orderFormFunctionsType,
};

export default OrderForm;
