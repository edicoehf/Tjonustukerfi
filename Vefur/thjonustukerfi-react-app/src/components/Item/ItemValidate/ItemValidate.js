const itemValidate = (values) => {
    const { category, service, amount, sliced, filleted, details } = values;
    const errors = {};
    if (category === null) {
        errors.category = "Tegund vantar";
    }
    if (service === null) {
        errors.service = "Þjónustu vantar";
    }
    if (amount === null) {
        errors.amount = "Fjölda vantar";
    } else if (amount < 1) {
        errors.amount = "Fjöldi verður að vera stærri en 0";
    }
    if (sliced === null) {
        errors.sliced = "Tilgreining á pökkun vantar";
    }
    if (filleted === null) {
        errors.filleted = "Tilgreining á flökun vantar";
    }
    if (details.length > 250) {
        errors.details = "Annað má aðeins vera 250 stafir";
    }
    return errors;
};

export default itemValidate;
