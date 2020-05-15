import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

// Define endpoint
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
 * Delete a customer from API
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

/**
 * Force delete a customer from API - even if it has active orders
 *
 * @param id - Customer ID
 *
 * @category Customer
 * @subcategory Services
 */
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

/**
 * Create a new customer in API
 *
 * @param customer - Customer object
 * @returns Customer ID
 *
 * @category Customer
 * @subcategory Services
 */
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

/**
 * Gets a customer by ID from the API
 *
 * @param id - Customer ID
 * @returns Customer
 *
 * @category Customer
 * @subcategory Services
 */
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

/**
 * Updates an existing customer in the API
 *
 * @param customer - Customer Object
 *
 * @category Customer
 * @subcategory Services
 */
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

/**
 * Gets all orders related to the customer with given ID from API
 *
 * @param id - Customer ID
 * @returns List of orders
 *
 * @category Customer
 * @subcategory Services
 */
const getOrdersByCustomerId = (id) => {
    return fetch(api_endpoint + id + "/orders", {
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
