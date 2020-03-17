import validator from "validator";
const validateForm = values => {
    //  name, ssn, email, telephone, postalCode, address
    const { name, email } = values;
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
    // if (ssn === '') { errors.ssn = 'Kennitölu vantar' }
    // if (telephone === '') { errors.telephone = 'Símanúmer vantar; }
    // if (postalCode === '') { errors.postalCode = 'Póstnúmer vantar'; }
    // if (address === '') { errors.address = 'Heimilisfang vantar'; }

    return errors;
};

export default validateForm;
