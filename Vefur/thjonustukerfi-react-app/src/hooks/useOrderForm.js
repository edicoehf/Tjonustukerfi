import React from "react";

const useOrderForm = (initialState, validate, submitHandler) => {
    const [items, setItems] = React.useState(initialState.items);
    const [customer, setCustomer] = React.useState(initialState.customer);
    const [errors, setErrors] = React.useState({});
    const [isSubmitting, setSubmitting] = React.useState(false);

    React.useEffect(() => {
        if (isSubmitting) {
            const noErrors = Object.keys(errors).length === 0;
            if (noErrors) {
                const order = constructOrder();
                submitHandler(order);
                setSubmitting(false);
            } else {
                setSubmitting(false);
            }
        }
    }, []);

    const addItems = (newItem, cb) => {
        const ids = items.map((item) => item.id);
        newItem.id = ids.reduce((acc, curr) => Math.max(acc, curr), 0) + 1;
        setItems([...items, newItem]);
        if (cb) {
            cb();
        }
    };

    const removeItem = (itemToRemove) => {
        setItems(items.filter((item) => itemToRemove.id !== item.id));
    };

    const addCustomer = (customer) => {
        setCustomer(customer);
    };

    const handleSubmit = () => {
        const validationErrors = validate(items, customer);
        setErrors(validationErrors);
        setSubmitting(true);
    };

    const resetFields = () => {
        setItems([]);
        setCustomer([]);
        setErrors({});
    };

    const constructOrder = () => {
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
        return order;
    };

    return {
        addItems,
        removeItem,
        addCustomer,
        handleSubmit,
        resetFields,
        items,
        customer,
        errors,
    };
};

export default useOrderForm;
