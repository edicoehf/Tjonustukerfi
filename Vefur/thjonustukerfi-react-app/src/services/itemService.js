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

const updateItemState = ({ id, state }) => {
    return fetch(endpoint + "statechangebyid", {
        method: "PATCH",
        headers: {
            crossDomain: true,
        },
        body: {
            itemId: id,
            stateChangeTo: state,
        },
    })
        .then(handleErrors)
        .catch((error) => Promise.reject(error));
};

export default {
    getItemById,
    getNextStatesById,
    updateItemState,
};
