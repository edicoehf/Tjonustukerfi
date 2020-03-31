import React from "react";
import useDeleteCustomerById from "../../../hooks/useDeleteCustomerById";
import { Button } from "react-bootstrap";
import "./DeleteCustomer.css";

const DeleteCustomer = ({ id }) => {
    const { error, handleDelete, isDeleting } = useDeleteCustomerById(id);

    return (
        <div className="delete-customer">
            <Button
                variant="danger"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                Eyða viðskiptavin
            </Button>
            {error ? (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            ) : (
                <></>
            )}
        </div>
    );
};

export default DeleteCustomer;
