import React from "react";
import useUpdateOrder from "../../../../hooks/useUpdateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import UpdateOrderActions from "../UpdateOrderActions/UpdateOrderActions";
import useGetOrderById from "../../../../hooks/useGetOrderById";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import { orderType } from "../../../../types";
import useGetCustomerById from "../../../../hooks/useGetCustomerById";
import { Redirect } from "react-router-dom";
import "./UpdateOrderView.css";
import ProgressComponent from "../../../Feedback/ProgressComponent/ProgressComponent";

// Component used in the update order page -> see below
const UpdateOrder = ({ order }) => {
    // Get customers details from the order so it can be displayed in the orderform
    const { customer: fetchedCustomer } = useGetCustomerById(order.customerId);

    // Was the update cancelled
    const [cancel, setCancel] = React.useState(false);
    // Cancel the update
    const handleCancel = () => {
        setCancel(true);
    };

    const initialState = {
        customer: null,
        items: order.items.map((item) => {
            return {
                id: item.id,
                category: item.categoryId.toString(),
                service: item.serviceId.toString(),
                amount: 1,
                categoryName: item.category,
                serviceName: item.service,
                details: item.details,
                sliced: item.json.sliced,
                filleted: item.json.filleted,
                otherCategory: item.json.otherCategory,
                otherService: item.json.otherService,
            };
        }),
    };

    // Use update order hook
    const {
        updateError: sendError,
        handleUpdate,
        isProcessing,
        hasUpdated,
    } = useUpdateOrder(order.id);

    // Use orderform hook
    const {
        addItems,
        removeItem,
        addCustomer,
        handleSubmit,
        items,
        customer,
        errors,
    } = useOrderForm(initialState, orderValidate, handleUpdate);

    // Add the customer to the orderform once its loaded
    React.useEffect(() => {
        if (!customer && Object.keys(fetchedCustomer).length > 0) {
            addCustomer(fetchedCustomer);
        }
    });

    // Submit the order update if not processing
    const updateOrder = () => {
        if (!isProcessing) {
            handleSubmit();
        }
    };

    // Redirect to order if update is successful
    const renderRedirect = () => {
        if (hasUpdated || cancel) {
            return <Redirect to={`/order/${order.id}`} />;
        }
    };

    return (
        <>
            {renderRedirect()}
            <OrderForm
                values={{ items, customer }}
                functions={{ addItems, removeItem, addCustomer }}
            />
            {sendError && <p className="error">Gat ekki sent inn pöntun</p>}
            {errors.customer && <p className="error">{errors.customer}</p>}
            {errors.items && <p className="error">{errors.items}</p>}
            <UpdateOrderActions
                updateOrder={updateOrder}
                cancelUpdate={handleCancel}
                isLoading={isProcessing}
            />
        </>
    );
};

UpdateOrder.prototype = {
    /** Order to update */
    order: orderType,
};

/**
 * Page for updating order
 *
 * @component
 * @category Order
 */
const UpdateOrderView = ({ match }) => {
    // Get id from url
    const id = match.params.id;
    // Get order
    const { order, error } = useGetOrderById(id);
    // Get services
    const { services, error: serviceError } = useGetServices();
    // Get categories
    const { categories, error: categoryError } = useGetCategories();

    // Check if objects are loaded
    const loaded = (obj) => {
        return Object.keys(obj).length > 0;
    };

    return (
        <div className="update-order-view">
            <>
                <h1>Uppfæra pöntun</h1>
                {(order.id && loaded(services) && loaded(categories)) ||
                error ? (
                    !error && !categoryError && !serviceError ? (
                        <UpdateOrder order={order} />
                    ) : (
                        <p className="error">Gat ekki sótt pöntun</p>
                    )
                ) : (
                    <ProgressComponent
                        isLoading={
                            !order.id || loaded(services) || loaded(categories)
                        }
                    />
                )}
            </>
        </div>
    );
};

export default UpdateOrderView;
