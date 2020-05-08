import React from "react";
import useUpdateOrder from "../../../../hooks/useUpdateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import UpdateOrderActions from "../UpdateOrderActions/UpdateOrderActions";
import useGetOrderById from "../../../../hooks/useGetOrderById";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import { orderType, idType } from "../../../../types";
import useGetCustomerById from "../../../../hooks/useGetCustomerById";
import { Redirect } from "react-router-dom";
import "./UpdateOrderView.css";
import ProgressComponent from "../../../Feedback/ProgressComponent/ProgressComponent";

const UpdateOrder = ({ order }) => {
    const { customer: fetchedCustomer } = useGetCustomerById(order.customerId);

    const [cancel, setCancel] = React.useState(false);
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

    const {
        updateError: sendError,
        handleUpdate,
        isProcessing,
        hasUpdated,
    } = useUpdateOrder(order.id);

    const {
        addItems,
        removeItem,
        addCustomer,
        handleSubmit,
        items,
        customer,
        errors,
    } = useOrderForm(initialState, orderValidate, handleUpdate);

    React.useEffect(() => {
        if (!customer && Object.keys(fetchedCustomer).length > 0) {
            addCustomer(fetchedCustomer);
        }
    });

    const updateOrder = () => {
        if (!isProcessing) {
            handleSubmit();
        }
    };

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
    id: idType,
    order: orderType,
};

const UpdateOrderView = ({ match }) => {
    const id = match.params.id;
    const { order, error } = useGetOrderById(id);
    const { services, error: serviceError } = useGetServices();
    const { categories, error: categoryError } = useGetCategories();

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
