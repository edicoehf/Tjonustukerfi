const orderValidate = (customer, items) => {
    const errors = {};
    if (
        customer === null ||
        !customer.hasOwnProperty("id") ||
        customer.id === null
    ) {
        errors.customer = "Viðskiptavin vantar";
    }
    if (items.length === 0) {
        errors.items = "Vörur vantar";
    }
    return errors;
};

export default orderValidate;
