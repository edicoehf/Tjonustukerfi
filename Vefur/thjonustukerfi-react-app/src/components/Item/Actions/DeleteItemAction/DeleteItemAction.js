import React from "react";
import { Button } from "@material-ui/core";
import useDeleteItemById from "../../../../hooks/useDeleteItemById";
import ConfirmationDialog from "../../../ConfirmationDialog/ConfirmationDialog";
import DeleteIcon from "@material-ui/icons/Delete";
import "./DeleteItemAction.css";

const DeleteItemAction = ({ id }) => {
    const { error, handleDelete, isDeleting } = useDeleteItemById(id);
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
            <Button
                className="delete-item-button"
                variant="contained"
                color="secondary"
                disable={isDeleting}
                onClick={handleOpen}
            >
                <DeleteIcon className="delete-icon" size="small" />
                <b>Eyða</b>
            </Button>
            <ConfirmationDialog
                title="Eyða vöru"
                description="Staðfestu að eyða skuli vöru úr pöntun"
                handleClose={handleClose}
                handleAccept={handleAccept}
                open={open}
            />
            {error && <p className="error">Gat ekki eytt vöru</p>}
        </div>
    );
};

export default DeleteItemAction;
