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

const updateItemState = ({ item, state }) => {
    return fetch(endpoint + "statechangebyid", {
        method: "PATCH",
        headers: {
            crossDomain: true,
            "Content-Type": "application/json",
        },
        body: JSON.stringify([
            {
                itemId: item,
                barcode: null,
                stateChangeTo: state,
            },
        ]),
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const updateItemById = (item) => {
    return fetch(endpoint + item.id, {
        method: "PATCH",
        body: JSON.stringify({
            categoryId: item.categoryId,
            serviceId: item.serviceId,
        }),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

const deleteItemById = (id) => {
    return fetch(endpoint + item.id, {
        method: "DELETE",
        headers: {
            crossDomain: true,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

export default {
    getItemById,
    getNextStatesById,
    updateItemById,
    updateItemState,
    deleteItemById,
};
