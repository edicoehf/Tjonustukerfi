import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import useDeleteCustomerById from "../../../../hooks/useDeleteCustomerById";
import "./DeleteCustomerAction.css";
import ForceDeleteCustomerAction from "../ForceDeleteCustomerAction/ForceDeleteCustomerAction";
import { idType } from "../../../../types/index";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { useHistory } from "react-router-dom";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";

const DeleteCustomerAction = ({ id }) => {
    const history = useHistory();

    const redirect = () => {
        history.push("/customers");
    };

    const {
        error,
        handleDelete,
        isDeleting,
        forceModalOpen,
        softModalOpen,
        handleClose,
        handleForceDelete,
        handleSoftDelete,
        isForceDeleting,
    } = useDeleteCustomerById(id, redirect);

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
                open={forceModalOpen}
                handleDelete={handleForceDelete}
                handleClose={handleClose}
            />
            <ConfirmationDialog
                title="Eyða viðskiptavin"
                description="Staðfestu að eyða eigi viðskiptavin"
                handleClose={handleClose}
                handleAccept={handleSoftDelete}
                open={softModalOpen}
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
