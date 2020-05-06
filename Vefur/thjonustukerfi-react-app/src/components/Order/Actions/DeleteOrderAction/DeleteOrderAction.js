import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import useDeleteOrderById from "../../../../hooks/useDeleteOrderById";
import {
    idType,
    handleDeleteType,
    isDeletingType,
} from "../../../../types/index";
import "./DeleteOrderAction.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { useHistory } from "react-router-dom";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";

const DeleteOrderAction = ({ id }) => {
    const history = useHistory();

    const redirect = () => {
        history.push("/orders");
    };

    const { error, handleDelete, isDeleting } = useDeleteOrderById(
        id,
        redirect
    );

    const [open, setOpen] = React.useState(false);

    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAccept = () => {
        handleClose();
        if (!isDeleting) {
            handleDelete();
        }
    };

    return (
        <div className="delete-order">
            <ProgressButton isLoading={isDeleting}>
                <Button
                    className="delete-order-button"
                    size="medium"
                    color="secondary"
                    variant="contained"
                    disabled={isDeleting}
                    onClick={handleOpen}
                >
                    <DeleteIcon className="delete-order-icon" size="small" />
                    <b>Eyða Pöntun</b>
                </Button>
            </ProgressButton>
            <ConfirmationDialog
                title="Eyða pöntun"
                description="Staðfestu að eyða eigi pöntun"
                handleClose={handleClose}
                handleAccept={handleAccept}
                open={open}
            />
            {error && <p className="delete-error">Gat ekki eytt pöntun</p>}
        </div>
    );
};

DeleteOrderAction.propTypes = {
    id: idType,
    handleDelete: handleDeleteType,
    isDeleting: isDeletingType,
};

export default DeleteOrderAction;
