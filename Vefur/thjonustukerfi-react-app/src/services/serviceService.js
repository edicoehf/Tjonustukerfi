import { handleErrors, handleData } from "./serviceHandlers";

const endpoint = "http://localhost:5000/api/info/services";

const getServices = () => {
    return fetch(endpoint, {
        method: "GET",
        headers: {
            crossDomain: true
        }
    })
        .then(handleErrors)
        .then(handleData)
        .catch(error => Promise.reject(error));
};

export default {
    getServices
};
