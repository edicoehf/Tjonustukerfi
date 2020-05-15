import { handleErrors, handleData } from "./serviceHandlers";
import endpoint from "./endpoint";

// Define endpoint
const api_endpoint = `${endpoint}/api/items/`;

/**
 * Gets an Item with the given ID from the API
 *
 * @param id - Item ID
 * @returns Item
 *
 * @category Item
 * @subcategory Services
 */
const getItemById = (id) => {
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
 * Gets an Item with the given barcode from the API
 *
 * @param barcode - Item barcode
 * @returns Item
 *
 * @category Item
 * @subcategory Services
 */
const getItemByBarcode = (barcode) => {
    return fetch(api_endpoint + "search?barcode=" + barcode, {
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
 * Gets an next available states for the item with the given ID from the API
 *
 * @param id - Item ID
 * @returns List of states
 *
 * @category Item
 * @subcategory Services
 */
const getNextStatesById = (id) => {
    return fetch(api_endpoint + "nextstate?itemId=" + id, {
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
 * Updates an items state in the API
 *
 * @param values - Item,State & Location
 *
 * @category Item
 * @subcategory Services
 */
const updateItemState = ({ item, state, location }) => {
    return fetch(api_endpoint + "statechange", {
        method: "PATCH",
        headers: {
            crossDomain: true,
            "Content-Type": "application/json",
        },
        body: JSON.stringify([
            {
                itemId: item,
                stateChangeTo: state,
                location: location,
            },
        ]),
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

/**
 * Updates an Item in the API
 *
 * @param item - Item Object
 *
 * @category Item
 * @subcategory Services
 */
const updateItemById = (item) => {
    const id = item.id;
    delete item.id;
    return fetch(api_endpoint + id, {
        method: "PATCH",
        body: JSON.stringify(item),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

/**
 * Delete an item from the API
 *
 * @param id - Item ID
 *
 * @category Item
 * @subcategory Services
 */
const deleteItemById = (id) => {
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
 * Gets an items history from the API
 *
 * @param id - Item ID
 * @returns List of states
 *
 * @category Item
 * @subcategory Services
 */
const getItemHistoryById = (id) => {
    return fetch(`${endpoint}/api/info/${id}/itemhistory`, {
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
 * Gets all locations from the API
 *
 * @returns List of locations
 *
 * @category Item
 * @subcategory Services
 */
const getItemLocations = () => {
    return fetch(`${endpoint}/api/info/itemlocations`, {
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
 * Gets item details for printing
 *
 * @param id - Item ID
 * @returns Item print details
 *
 * @category Item
 * @subcategory Services
 */
const getItemPrintDetails = (id) => {
    return fetch(`${endpoint}/api/items/printer/${id}`, {
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
 * Gets all states from the API
 *
 * @returns List of states
 *
 * @category Item
 * @subcategory Services
 */
const getAllStates = () => {
    return fetch(`${endpoint}/api/info/states`, {
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
    getItemById,
    getItemByBarcode,
    getNextStatesById,
    updateItemById,
    updateItemState,
    deleteItemById,
    getItemHistoryById,
    getItemLocations,
    getItemPrintDetails,
    getAllStates,
};
