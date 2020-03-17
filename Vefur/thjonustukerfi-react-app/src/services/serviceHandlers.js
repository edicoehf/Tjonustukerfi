const handleErrors = resp => {
    if (!resp.ok) {
        throw Error(resp);
    }
    return resp.json();
};

module.exports = {
    handleErrors
};
