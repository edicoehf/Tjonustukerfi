import React from "react";
import { Button } from "react-bootstrap";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import "./DeleteCustomerAction.css";

const DeleteCustomerAction = ({ id }) => {
    const { error, handleDelete, isDeleting } = useDeleteCustomerById(id);

    return (
        <div className="delete-customer">
            <Button
                variant="danger"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                Eyða
            </Button>
            {error ? (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            ) : (
                <></>
            )}
        </div>
    );
};

export default DeleteCustomerAction;
