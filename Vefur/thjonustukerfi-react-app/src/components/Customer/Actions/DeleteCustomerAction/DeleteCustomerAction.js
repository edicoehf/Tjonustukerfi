import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
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
                className="delete-button"
                size="medium"
                color="secondary"
                variant="contained"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                <DeleteIcon className="delete-icon" size="small" />
                <b>Eyða</b>
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
