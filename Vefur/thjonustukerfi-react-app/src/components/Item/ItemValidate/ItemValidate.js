const itemValidate = (values) => {
    const { category, service, amount, sliced, filleted, other } = values;
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
    if (sliced === "") {
        errors.sliced = "Tilgreining á pökkun vantar";
    }
    if (filleted === "") {
        errors.filleted = "Tilgreining á flökun vantar";
    }
    if (other.length > 250) {
        errors.other = "Annað má aðeins vera 250 stafir";
    }
    return errors;
};

export default itemValidate;
