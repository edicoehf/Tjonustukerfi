import React from "react";
import CreateOrderActions from "../CreateOrderActions/CreateOrderActions";
import useCreateOrder from "../../../../hooks/useCreateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import "./CreateOrderView.css";

const initialState = {
    customer: null,
    items: [],
};

const CreateOrderView = () => {
    const { error: sendError, handleCreate, isProcessing } = useCreateOrder();
    const {
        addItems,
        removeItem,
        addCustomer,
        handleSubmit,
        resetFields,
        items,
        customer,
        errors,
    } = useOrderForm(initialState, orderValidate, handleCreate);

    const createOrder = () => {
        if (!isProcessing) {
            handleSubmit();
        }
    };

    return (
        <div className="create-order-view">
            <h1>Ný pöntun</h1>
            <OrderForm
                values={{ items, customer }}
                functions={{ addItems, removeItem, addCustomer }}
            />
            {sendError && <p className="error">Gat ekki sent inn pöntun</p>}
            {errors.customer && <p className="error">{errors.customer}</p>}
            {errors.items && <p className="error">{errors.items}</p>}
            <CreateOrderActions
                createOrder={createOrder}
                cancelOrder={resetFields}
            />
        </div>
    );
};

export default CreateOrderView;
