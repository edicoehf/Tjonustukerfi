import { handleErrors, handleData } from "./serviceHandlers";

const endpoint = "http://localhost:5000/api/orders/";

const getOrderById = (id) => {
    return fetch(endpoint + id, {
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
    return fetch(endpoint, {
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
    return fetch(endpoint + id, {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const getAllOrders = () => {
    return fetch(endpoint, {
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
    return fetch(endpoint + id, {
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
    return fetch(endpoint + id + "/complete", {
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
    return fetch("http://localhost:5000/api/info/orderarchives", {
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
