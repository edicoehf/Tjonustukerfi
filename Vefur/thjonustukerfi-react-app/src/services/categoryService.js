import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

// Define endpoint
const api_endpoint = `${endpoint}/api/info/categories`;

/**
 * Fetches all categories from API
 *
 * @returns List of categories
 * @category Info
 * @subcategory Services
 */
const getCategories = () => {
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
    getCategories,
};
