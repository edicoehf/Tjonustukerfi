import React from "react";
import { Button } from "react-bootstrap";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import "./DeleteCustomerAction.css";
import ForceDeleteCustomerAction from "../ForceDeleteCustomerAction/ForceDeleteCustomerAction";

const DeleteCustomerAction = ({ id }) => {
    const {
        error,
        handleDelete,
        isDeleting,
        modalIsOpen,
        handleClose,
        handleForceDelete,
    } = useDeleteCustomerById(id);

    return (
        <div className="delete-customer">
            <Button
                variant="danger"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                Eyða
            </Button>
            <ForceDeleteCustomerAction
                isOpen={modalIsOpen}
                handleDelete={handleForceDelete}
                handleClose={handleClose}
            />
            {error && (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            )}
        </div>
    );
};

export default DeleteCustomerAction;
