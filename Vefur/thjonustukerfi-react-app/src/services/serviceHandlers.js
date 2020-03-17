const handleErrors = resp => {
    if (!resp.ok) {
        throw Error(resp);
    }
    return resp;
};

module.exports = {
    handleErrors
};
