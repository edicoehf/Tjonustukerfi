const handleErrors = resp => {
    if (!resp.ok) {
        throw Error(resp);
    }
    return resp.json();
};

const handleData = data => {
    if (!data) {
        return {};
    }
    return data;
};

module.exports = {
    handleErrors,
    handleData
};
