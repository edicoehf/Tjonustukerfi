import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import "./DeleteCustomerAction.css";
import ForceDeleteCustomerAction from "../ForceDeleteCustomerAction/ForceDeleteCustomerAction";
import { idType } from "../../../../types/index";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";

const DeleteCustomerAction = ({ id }) => {
    const {
        error,
        handleDelete,
        isDeleting,
        modalIsOpen,
        handleClose,
        handleForceDelete,
        isForceDeleting,
    } = useDeleteCustomerById(id);

    return (
        <div className="delete-customer">
            <ProgressButton isLoading={isDeleting || isForceDeleting}>
                <Button
                    className="delete-button"
                    size="medium"
                    color="secondary"
                    variant="contained"
                    disabled={isDeleting || isForceDeleting}
                    onClick={handleDelete}
                >
                    <DeleteIcon className="delete-icon" size="small" />
                    <b>Eyða</b>
                </Button>
            </ProgressButton>
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
