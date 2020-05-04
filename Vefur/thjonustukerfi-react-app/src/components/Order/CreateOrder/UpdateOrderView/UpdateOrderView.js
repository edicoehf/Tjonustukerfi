import React from "react";
import useUpdateOrder from "../../../../hooks/useUpdateOrder";
import orderValidate from "../OrderValidate/OrderValidate";
import useOrderForm from "../../../../hooks/useOrderForm";
import OrderForm from "../OrderForm/OrderForm";
import UpdateOrderActions from "../UpdateOrderActions/UpdateOrderActions";
import useGetOrderById from "../../../../hooks/useGetOrderById";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import {
    orderType,
    categoriesType,
    servicesType,
    idType,
} from "../../../../types";
import useGetCustomerById from "../../../../hooks/useGetCustomerById";
import { Redirect } from "react-router-dom";
import "./UpdateOrderView.css";

const UpdateOrder = ({ order, categories, services }) => {
    const { customer: fetchedCustomer } = useGetCustomerById(order.customerId);

    const [cancel, setCancel] = React.useState(false);
    const handleCancel = () => {
        setCancel(true);
    };

    // const getServiceId = (service) => {
    //     return services[services.findIndex((s) => s.name === service)].id;
    // };

    // const getCategoryId = (category) => {
    //     return categories[categories.findIndex((c) => c.name === category)].id;
    // };

    console.log(order);
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
        error: sendError,
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
            />
        </>
    );
};

UpdateOrder.prototype = {
    id: idType,
    order: orderType,
    categories: categoriesType,
    services: servicesType,
};

const UpdateOrderView = ({ match }) => {
    const id = match.params.id;
    const { order, error } = useGetOrderById(id);
    const { services } = useGetServices();
    const { categories } = useGetCategories();

    const loaded = (obj) => {
        return Object.keys(obj).length > 0;
    };

    return (
        <div className="update-order-view">
            {!error ? (
                <>
                    <h1>Uppfæra pöntun</h1>
                    {loaded(order) &&
                        loaded(services) &&
                        loaded(categories) && (
                            <UpdateOrder
                                order={order}
                                services={services}
                                categories={categories}
                            />
                        )}
                </>
            ) : (
                <p className="error">Gat ekki sótt pöntun</p>
            )}
        </div>
    );
};

export default UpdateOrderView;
