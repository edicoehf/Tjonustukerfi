import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

// Define endpoint
const api_endpoint = `${endpoint}/api/info/services/`;

/**
 * Get all services from the API
 *
 * @returns List of services
 *
 * @category Info
 * @subcategory Services
 */
const getServices = () => {
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

export default {
    getServices,
};
