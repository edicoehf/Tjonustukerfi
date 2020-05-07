import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

const api_endpoint = `${endpoint}/api/orders/`;

const getOrderById = (id) => {
    return fetch(api_endpoint + id, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const createOrder = (order) => {
    return fetch(api_endpoint, {
        method: "POST",
        body: JSON.stringify(order),
        headers: {
            crossDomain: true,
            "Content-Type": "application/json",
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const deleteOrderById = (id) => {
    return fetch(api_endpoint + id, {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const getAllOrders = () => {
    return fetch(api_endpoint, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const updateOrderById = (order, id) => {
    return fetch(api_endpoint + id, {
        method: "PATCH",
        body: JSON.stringify(order),
        headers: {
            crossDomain: true,
            "Content-Type": "application/json",
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const checkoutOrderById = (id) => {
    return fetch(api_endpoint + id + "/complete", {
        method: "PATCH",
        headers: {
            crossDomain: true,
            "Content-Type": "application/json",
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const getAllArchivedOrders = () => {
    return fetch(`${api_endpoint}/api/info/orderarchives`, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

export default {
    getOrderById,
    createOrder,
    getAllOrders,
    deleteOrderById,
    updateOrderById,
    checkoutOrderById,
    getAllArchivedOrders,
};
