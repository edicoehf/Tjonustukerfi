import validator from "validator";

/**
 * Functions that validates customer information
 * @param {object} values - Customer values
 * @returns {object} Object with errors messages for failed tests
 *
 * @category Customer
 * @subcategory Validation
 */

const validateForm = (values) => {
    //  name, ssn, email, telephone, postalCode, address
    const { name, email, ssn } = values;
    const errors = {};
    if (name === "") {
        errors.name = "Nafn vantar";
    }
    if (name.length >= 100) {
        errors.name = "Nafn verður að vera minna en 100 stafir";
    }
    if (!validator.isEmail(email)) {
        errors.email = "Ógilt netfang";
    }
    if (email === "") {
        errors.email = "Netfang vantar";
    }
    //const re = /^(0?[1-9]|[12][0-9]|3[01])(1[0-2]|0?[1-9])[0-9]{2}(-?)([0-9]{3})[890]$/;
    //if (ssn !== "" && re.exec(ssn)) {
    //    errors.ssn = "Ógild kennitala";
    //}
    // if (ssn === '') { errors.ssn = 'Kennitölu vantar' }
    // if (telephone === '') { errors.telephone = 'Símanúmer vantar; }
    // if (postalCode === '') { errors.postalCode = 'Póstnúmer vantar'; }
    // if (address === '') { errors.address = 'Heimilisfang vantar'; }

    return errors;
};

export default validateForm;
