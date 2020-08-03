import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

// define endpoint
const api_endpoint = `${endpoint}/api/orders/`;

/**
 * Get order with the corresponding ID from the API
 *
 * @param id - Order ID
 * @returns Order
 *
 * @category Order
 * @subcategory Services
 */
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

/**
 * Create a new order in the API
 *
 * @param order - Order object
 * @returns Order ID
 *
 * @category Order
 * @subcategory Services
 */
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

/**
 * Delete order with the corresponding ID from the API
 *
 * @param id - Order ID
 *
 * @category Order
 * @subcategory Services
 */
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

/**
 * Get all orders from the API
 *
 * @returns List of orders
 *
 * @category Order
 * @subcategory Services
 */
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


/**
 * Get all orders from the API
 *
 * @returns List of orders
 *
 * @category Order
 * @subcategory Services
 */
const getAllRawOrders = () => {
    return fetch(api_endpoint + "raw", {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

/**
 * Update order in the API
 *
 * @param order - Order object
 * @param id - Order ID
 *
 * @category Order
 * @subcategory Services
 */
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

/**
 * Check out order with the corresponding ID in the API
 *
 * @param id - Order ID
 *
 * @category Order
 * @subcategory Services
 */
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

/**
 * Get all archived orders from the API
 *
 * @returns Order
 *
 * @category Order
 * @subcategory Services
 */
const getAllArchivedOrders = () => {
    return fetch(`${endpoint}/api/info/orderarchives`, {
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
    getAllRawOrders,
    deleteOrderById,
    updateOrderById,
    checkoutOrderById,
    getAllArchivedOrders,
};
