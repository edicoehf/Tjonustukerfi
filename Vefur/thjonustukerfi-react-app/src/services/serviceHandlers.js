/**
 * Throws error if response isn't ok
 *
 * @param resp - Response from API
 * @returns Response
 */
const handleErrors = (resp) => {
    if (!resp.ok) {
        throw Error(resp);
    }
    return resp;
};

/**
 * Parses data to json
 *
 * @param data - Data from API
 * @returns JSON data
 */
const handleData = (data) => {
    if (!data) {
        return {};
    }
    return data.json();
};

module.exports = {
    handleErrors,
    handleData,
};
