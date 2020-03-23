const handleErrors = resp => {
    console.log(resp);
    if (!resp.ok) {
        throw Error(resp);
    }
    return resp;
};

const handleData = data => {
    if (!data) {
        return {};
    }
    return data.json();
};

module.exports = {
    handleErrors,
    handleData
};
