import { createContext } from "react";

const initialState = {
    name: "",
    ssn: "",
    telephone: "",
    email: "",
    postalCode: "",
    address: ""
};

export const CustomerContext = createContext(initialState);
