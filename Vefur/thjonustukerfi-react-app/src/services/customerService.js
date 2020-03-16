const endpoint = "http://localhost:5000/api/customers/";

const createCustomer = customer => {
    console.log("POST");
    return fetch(endpoint, {
        method: "POST",
        body: JSON.stringify(customer),
        headers: {
            "Content-Type": "application/json",
            crossDomain: true
        }
    });
};

export default createCustomer;
