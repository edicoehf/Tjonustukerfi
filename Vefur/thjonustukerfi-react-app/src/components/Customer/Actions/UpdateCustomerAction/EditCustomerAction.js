import React from "react";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import { Button } from "react-bootstrap";

const UpdateCustomerAction = ({ id }) => {
    return (
        <div className="delete-customer">
            <Link to="/new-customer" onClick={() => setCustomer(customer)}>
                <Button variant="warning">Update</Button>
            </Link>
            {error ? (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            ) : (
                <></>
            )}
        </div>
    );
};

export default UpdateCustomerAction;
