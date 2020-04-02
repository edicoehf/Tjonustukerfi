import { handleErrors, handleData } from "./serviceHandlers";

const endpoint = "http://localhost:5000/api/orders/";

const getOrderById = id => {
    return fetch(endpoint + id, {
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
    getOrderById
};
