import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

const api_endpoint = `${endpoint}/api/info/categories`;

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
