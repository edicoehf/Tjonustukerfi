import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

const api_endpoint = `${endpoint}/api/customers/`;

/**
 * Fetch all customers from API
 *
 * @returns List of all customers
 *
 * @category Customer
 * @subcategory Services
 */
const getAllCustomers = () => {
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
 * Fetch all customers from API
 *
 * @param id - Customer ID
 *
 * @category Customer
 * @subcategory Services
 */
const deleteCustomerById = (id) => {
    return fetch(api_endpoint + id, {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const forceDeleteCustomerById = (id) => {
    return fetch(api_endpoint + id + "/confirm", {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const createCustomer = (customer) => {
    return fetch(api_endpoint, {
        method: "POST",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const getCustomerById = (id) => {
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

const updateCustomer = (customer) => {
    return fetch(api_endpoint + customer.id, {
        method: "PATCH",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const getOrdersByCustomerId = (customerId) => {
    return fetch(api_endpoint + customerId + "/orders", {
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
    getAllCustomers,
    deleteCustomerById,
    createCustomer,
    getCustomerById,
    updateCustomer,
    forceDeleteCustomerById,
    getOrdersByCustomerId,
};
