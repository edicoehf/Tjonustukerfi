import React from "react";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import { Button } from "react-bootstrap";

const EditCustomerAction = ({ id }) => {
    return (
        <div className="delete-customer">
            <Link to="/new-customer" onClick={() => setCustomer(customer)}>
                <Button variant="warning">Edit</Button>
            </Link>
            {error ? (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            ) : (
                <></>
            )}
        </div>
    );
};

export default EditCustomerAction;
