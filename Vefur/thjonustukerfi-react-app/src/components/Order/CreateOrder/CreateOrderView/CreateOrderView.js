import React from "react";
import CreateOrderActions from "../CreateOrderActions/CreateOrderActions";
import useCreateOrder from "../../../../hooks/useCreateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import SuccessToaster from "../../../Feedback/SuccessToaster/SuccessToaster";
import { useHistory } from "react-router-dom";
import PrintService from "../../../../services/printService"
import "./CreateOrderView.css";

const initialState = {
    customer: null,
    items: [],
};

/**
 * Page for creating a new order
 *
 * @component
 * @category Order
 */
const CreateOrderView = () => {
    // Was the creation of the order successful
    const [success, setSuccess] = React.useState(false);

    // The creation has been marked successful, time for a new one (reset)
    const receivedSuccess = () => {
        setSuccess(false);
    };

    // Set the creation of the order successful
    const handleSuccess = (data) => {
        setSuccess(true);
//        console.log("CreateOrderView:handleSuccess");
//        console.log(data);
        PrintService.printOrder(data);
    };

    // Use create order hook, send handlesuccess as cb to be called on success
    const {
        error: sendError,
        handleCreate,
        isProcessing,
        orderId
    } = useCreateOrder(handleSuccess);

    // Use order form, validates and calls uses handleCreate on submit
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

    // Create order if not processing
    const createOrder = () => {
        if (!isProcessing) {
            handleSubmit();
        }
    };

    // Get history
    const history = useHistory();
    // Send user to order details page, used for toaster - can click the toaster to navigate there.
    const redirectToOrder = () => {
        history.push(`/order/${orderId}`);
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
