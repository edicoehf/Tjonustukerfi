import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import { Button } from "@material-ui/core";
import {
    createOrderType,
    cancelOrderType,
    isLoadingType,
} from "../../../../types/index";
import "./UpdateOrderActions.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";

/**
 * Actions available when updating an order.
 * 1. Cancel and return to order details.
 * 2. Update order and return to order details.
 *
 * @component
 * @category Order
 */
const UpdateOrderActions = ({ updateOrder, cancelUpdate, isLoading }) => {
    // Is confirmation dialog for updating the order open
    const [OpenUpdateDialog, setOpenUpdateDialog] = React.useState(false);
    // Is confirmation dialog for canceling the changes open
    const [openCancelDialog, setOpenCancelDialog] = React.useState(false);

    // Update the order and close the dialog
    const handleUpdate = () => {
        handleUpdateClose();
        updateOrder();
    };

    // Close the update order dialog
    const handleUpdateClose = () => {
        setOpenUpdateDialog(false);
    };

    // Open the update order confirmation dialog
    const handleUpdateOpen = () => {
        setOpenUpdateDialog(true);
    };

    // Cancel changes to the order and close dialog
    const handleCancel = () => {
        handleCancelClose();
        cancelUpdate();
    };

    // Close the cancel changes dialog
    const handleCancelClose = () => {
        setOpenCancelDialog(false);
    };

    // Open cancel changes dialog
    const handleCancelOpen = () => {
        setOpenCancelDialog(true);
    };

    return (
        <div className="update-order-actions">
            <Button
                className="cancel-btn"
                variant="contained"
                color="secondary"
                size="large"
                disabled={isLoading}
                startIcon={<CancelIcon />}
                onClick={handleCancelOpen}
            >
                Hætta við
            </Button>
            <ProgressButton isLoading={isLoading}>
                <Button
                    className="confirm-btn"
                    variant="contained"
                    color="primary"
                    size="large"
                    disabled={isLoading}
                    startIcon={<CheckIcon />}
                    onClick={handleUpdateOpen}
                >
                    Uppfæra pöntun
                </Button>
            </ProgressButton>
            <ConfirmationDialog
                title="Uppfæra pöntun"
                description="Vinsamlegast staðfestu breytingar á pöntun"
                handleAccept={handleUpdate}
                handleClose={handleUpdateClose}
                open={OpenUpdateDialog}
            />
            <ConfirmationDialog
                title="Hætta við uppfærslu"
                description="Viltu hætta við breytingar á pöntun?"
                handleAccept={handleCancel}
                handleClose={handleCancelClose}
                open={openCancelDialog}
            />
        </div>
    );
};

UpdateOrderActions.propTypes = {
    /** CB that updates order */
    updateOrder: createOrderType,
    /** CB that cancels changes */
    cancelOrder: cancelOrderType,
    /** Is the data still loading */
    isLoading: isLoadingType,
};

export default UpdateOrderActions;
