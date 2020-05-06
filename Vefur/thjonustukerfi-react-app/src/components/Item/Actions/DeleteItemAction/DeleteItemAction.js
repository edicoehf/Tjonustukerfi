import React from "react";
import { Button } from "@material-ui/core";
import useDeleteItemById from "../../../../hooks/useDeleteItemById";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import DeleteIcon from "@material-ui/icons/Delete";
import "./DeleteItemAction.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { useHistory } from "react-router-dom";

const DeleteItemAction = ({ id }) => {
    const history = useHistory();

    const redirect = (orderId) => {
        if (orderId) {
            history.push(`/order/${orderId}`);
        } else {
            history.push("/orders/");
        }
    };

    const { error, handleDelete, isDeleting } = useDeleteItemById(id, redirect);
    const [open, setOpen] = React.useState(false);

    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAccept = () => {
        handleClose();
        handleDelete();
    };

    return (
        <div className="delete-item">
            <ProgressButton isLoading={isDeleting}>
                <Button
                    className="delete-item-button"
                    variant="contained"
                    color="secondary"
                    disabled={isDeleting}
                    onClick={handleOpen}
                >
                    <DeleteIcon className="delete-icon" size="small" />
                    <b>Eyða</b>
                </Button>
            </ProgressButton>
            <ConfirmationDialog
                title="Eyða vöru"
                description="Staðfestu að eyða eigi vöru úr pöntun"
                handleClose={handleClose}
                handleAccept={handleAccept}
                open={open}
            />
            {error && <p className="error">Gat ekki eytt vöru</p>}
        </div>
    );
};

export default DeleteItemAction;
