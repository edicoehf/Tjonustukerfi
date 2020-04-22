import React from "react";
import { Button } from "react-bootstrap";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import "./DeleteCustomerAction.css";
import ForceDeleteCustomerAction from "../ForceDeleteCustomerAction/ForceDeleteCustomerAction";
import { idType } from "../../../../types/index";

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
                open={modalIsOpen}
                handleDelete={handleForceDelete}
                handleClose={handleClose}
            />
            {error && (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            )}
        </div>
    );
};

DeleteCustomerAction.propTypes = {
    id: idType,
};

export default DeleteCustomerAction;
