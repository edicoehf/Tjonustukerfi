/**
 * Functions that validates order information
 * @param {object} customer - Selected customer
 * @param {array} items - Items in order
 * @returns {object} Object with errors messages for failed tests
 *
 * @category Order
 * @subcategory Validation
 */
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
