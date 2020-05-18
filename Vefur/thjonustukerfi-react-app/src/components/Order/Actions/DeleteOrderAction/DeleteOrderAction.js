import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import useDeleteOrderById from "../../../../hooks/useDeleteOrderById";
import { idType } from "../../../../types/index";
import "./DeleteOrderAction.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { useHistory } from "react-router-dom";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";

/**
 * Button that deletes order from system
 *
 * @component
 * @category Order
 */
const DeleteOrderAction = ({ id }) => {
    // Get history
    const history = useHistory();
    // Send user to the list of orders
    const redirect = () => {
        history.push("/orders");
    };

    // Use delete order hook, send redirection function as cb to be called on success
    const { error, handleDelete, isDeleting } = useDeleteOrderById(
        id,
        redirect
    );

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

    // Close dialog and delete order
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
    /** Order ID */
    id: idType,
};

export default DeleteOrderAction;
