const orderValidate = (customer, items) => {
    const errors = {};
    if (
        customer === null ||
        !customer.hasOwnProperty("id") ||
        customer.id === null
    ) {
        errors.category = "Viðskiptavin vantar";
    }
    if (items.length === 0) {
        errors.service = "Vörur vantar";
    }
    return errors;
};

export default orderValidate;
