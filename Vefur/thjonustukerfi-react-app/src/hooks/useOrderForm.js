import React from "react";

/**
 * Hook that handles the use of the order form
 *
 * @param initialState - The initial input values
 * @param validate - Function used to validatate input values
 * @param submitHandler - Function used to submit values
 * @returns addItems, removeItem, addCustomer, handleSubmit, resetFields, items, customer, errors
 *
 * @category Order
 * @subcategory Hooks
 */
const useOrderForm = (initialState, validate, submitHandler) => {
    // Items in the order
    const [items, setItems] = React.useState(initialState.items);
    // Customer for the order
    const [customer, setCustomer] = React.useState(initialState.customer);
    // Errors that occurred
    const [errors, setErrors] = React.useState({});
    // Should the form be submitted
    const [isSubmitting, setSubmitting] = React.useState(false);

    React.useEffect(() => {
        // Function that uses the form values to create an order object which the API accepts
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
                        otherCategory: item.otherCategory,
                        otherService: item.otherService,
                        sliced: item.sliced === "sliced" ? true : false,
                        filleted: item.filleted === "filleted" ? true : false,
                        details: item.details,
                    });
                }
            });
            return order;
        };

        // Submit if order should be submitted
        if (isSubmitting) {
            // Only if error free
            const noErrors = Object.keys(errors).length === 0;
            if (noErrors) {
                // Construct order for API
                const order = constructOrder();
                // Submit
                submitHandler(order, resetFields);
                setSubmitting(false);
            } else {
                setSubmitting(false);
            }
        }
    }, [errors, isSubmitting, submitHandler, items, customer]);

    // Add item to the order
    const addItems = (newItem, cb) => {
        const ids = items.map((item) => item.id);
        newItem.id = ids.reduce((acc, curr) => Math.max(acc, curr), 0) + 1;
        setItems([...items, newItem]);
        if (cb) {
            cb();
        }
    };

    // Remove item from the order
    const removeItem = (itemToRemove) => {
        setItems(items.filter((item) => itemToRemove.id !== item.id));
    };

    // Add customer to the order
    const addCustomer = (customer) => {
        setCustomer(customer);
    };

    // Exported function to trigger submission
    const handleSubmit = () => {
        const validationErrors = validate(customer, items);
        setErrors(validationErrors);
        setSubmitting(true);
    };

    // Reset all input fields
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
