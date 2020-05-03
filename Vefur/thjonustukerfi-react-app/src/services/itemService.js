import { handleErrors, handleData } from "./serviceHandlers";

const endpoint = "http://localhost:5000/api/items/";

const getItemById = (id) => {
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

const getItemByBarcode = (barcode) => {
    return fetch(endpoint + "search?barcode=" + barcode, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const getNextStatesById = (id) => {
    return fetch(endpoint + "nextstate?itemId=" + id, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const updateItemState = ({ item, state, location }) => {
    return fetch(endpoint + "statechange", {
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

const updateItemById = (item) => {
    const id = item.id;
    delete item.id;
    return fetch(endpoint + id, {
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

const deleteItemById = (id) => {
    return fetch(endpoint + id, {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const getItemHistoryById = (id) => {
    return fetch(`http://localhost:5000/api/info/${id}/itemhistory`, {
        method: "GET",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .then(handleData)
        .catch((error) => Promise.reject(error));
};

const getItemLocations = () => {
    return fetch(`http://localhost:5000/api/info/itemlocations`, {
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
};
