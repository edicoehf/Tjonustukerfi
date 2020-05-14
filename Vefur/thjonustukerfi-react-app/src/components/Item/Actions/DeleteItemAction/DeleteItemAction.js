import React from "react";
import { Button } from "@material-ui/core";
import useDeleteItemById from "../../../../hooks/useDeleteItemById";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import DeleteIcon from "@material-ui/icons/Delete";
import "./DeleteItemAction.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { useHistory } from "react-router-dom";
import { idType } from "../../../../types";

/**
 * Button that deletes a given item from the system
 *
 * @component
 * @category Item
 */
const DeleteItemAction = ({ id }) => {
    // Get history
    const history = useHistory();
    // Send user to order details page if orderId is known, else orders page
    const redirect = (orderId) => {
        if (orderId) {
            history.push(`/order/${orderId}`);
        } else {
            history.push("/orders/");
        }
    };

    // Use delete item hook, send redirect function as cb to be called on success
    const { error, handleDelete, isDeleting } = useDeleteItemById(id, redirect);

    // Is confirmation dialog open
    const [open, setOpen] = React.useState(false);

    // Open confirmation dialog
    const handleOpen = () => {
        setOpen(true);
    };

    // Close confirmation dialog
    const handleClose = () => {
        setOpen(false);
    };

    // Close dialog and delete item, for confirm button
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

DeleteItemAction.propTypes = {
    /** Item ID */
    id: idType,
};

export default DeleteItemAction;
