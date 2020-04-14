const itemValidate = (values) => {
    const { category, service, amount } = values;
    const errors = {};
    if (category === null) {
        errors.category = "Tegund vantar";
    }
    if (service === null) {
        errors.service = "Þjónustu vantar";
    }
    if (amount < 1) {
        errors.amount = "Fjöldi verður að vera stærri en 0";
    }
    return errors;
};

export default itemValidate;
