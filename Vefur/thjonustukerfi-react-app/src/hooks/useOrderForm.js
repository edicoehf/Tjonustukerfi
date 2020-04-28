import React from "react";

const useOrderForm = (initialState, validate, submitHandler) => {
    const [items, setItems] = React.useState(initialState.items);
    const [customer, setCustomer] = React.useState(initialState.customer);
    const [errors, setErrors] = React.useState({});
    const [isSubmitting, setSubmitting] = React.useState(false);

    React.useEffect(() => {
        const constructOrder = () => {
            let order = {
                customerId: null,
                items: [],
            };
            order.customerId = customer.id;
            items.forEach((item) => {
                for (var i = 0; i < item.amount; i++) {
                    order.items.push({
                        categoryId: parseInt(item.category),
                        serviceId: parseInt(item.service),
                        sliced: item.sliced === "sliced" ? true : false,
                        filleted: item.filleted === "filleted" ? true : false,
                        other: item.other,
                    });
                }
            });
            return order;
        };

        if (isSubmitting) {
            const noErrors = Object.keys(errors).length === 0;
            if (noErrors) {
                const order = constructOrder();
                submitHandler(order, resetFields);
                setSubmitting(false);
            } else {
                setSubmitting(false);
            }
        }
    }, [errors, isSubmitting, submitHandler, items, customer]);

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
        console.log(customer);
        console.log(items);
        const validationErrors = validate(customer, items);
        setErrors(validationErrors);
        setSubmitting(true);
    };

    const resetFields = () => {
        setItems([]);
        setCustomer(null);
        setErrors({});
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
