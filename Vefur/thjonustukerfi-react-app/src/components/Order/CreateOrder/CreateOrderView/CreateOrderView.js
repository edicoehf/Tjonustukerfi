import React from "react";
import CreateOrderActions from "../CreateOrderActions/CreateOrderActions";
import useCreateOrder from "../../../../hooks/useCreateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import SuccessToaster from "../../../Feedback/SuccessToaster/SuccessToaster";
import { useHistory } from "react-router-dom";
import "./CreateOrderView.css";

const initialState = {
    customer: null,
    items: [],
};

const CreateOrderView = () => {
    const [success, setSuccess] = React.useState(false);

    const receivedSuccess = () => {
        setSuccess(false);
    };

    const handleSuccess = () => {
        setSuccess(true);
    };

    const {
        error: sendError,
        handleCreate,
        isProcessing,
        orderId,
    } = useCreateOrder(handleSuccess);

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

    const history = useHistory();

    const redirectToOrder = () => {
        history.push(`/order/${orderId}`);
    };

    console.log(orderId);

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
                isProcessing={isProcessing}
            />
            <SuccessToaster
                success={success}
                receivedSuccess={receivedSuccess}
                message="Pöntun tókst!"
                cb={redirectToOrder}
                cbText="Skoða"
            />
        </div>
    );
};

export default CreateOrderView;
