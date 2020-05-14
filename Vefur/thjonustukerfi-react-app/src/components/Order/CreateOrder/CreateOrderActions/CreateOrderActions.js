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
import "./CreateOrderActions.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";

/**
 * Actions available in Create Order, Button for sending an order into the system and one for dissmissing (clear)
 *
 * @component
 * @category Order
 */
const CreateOrderActions = ({ createOrder, cancelOrder, isProcessing }) => {
    // Is confirmation dialog for sending order into the system open
    const [openSendDialog, setOpenSendDialog] = React.useState(false);
    // Is confirmation dialog for dissmissing order open
    const [openCancelDialog, setOpenCancelDialog] = React.useState(false);

    // Close confirmation dialog and send the order into the system (create)
    const handleSend = () => {
        handleSendClose();
        createOrder();
    };

    // Close confirmation dialog for creating order
    const handleSendClose = () => {
        setOpenSendDialog(false);
    };

    // Open create order dialog
    const handleSendOpen = () => {
        setOpenSendDialog(true);
    };

    // Clear the create order page and close confirmation dialog
    const handleCancel = () => {
        handleCancelClose();
        cancelOrder();
    };

    // Close dissimiss confirmation dialog
    const handleCancelClose = () => {
        setOpenCancelDialog(false);
    };

    // Open dissmiss confirmation dialog
    const handleCancelOpen = () => {
        setOpenCancelDialog(true);
    };

    return (
        <div className="create-order-actions">
            <Button
                className="cancel-btn"
                variant="contained"
                color="secondary"
                size="large"
                disabled={isProcessing}
                startIcon={<CancelIcon />}
                onClick={handleCancelOpen}
            >
                Hætta við
            </Button>
            <ProgressButton isLoading={isProcessing}>
                <Button
                    className="confirm-btn"
                    variant="contained"
                    color="primary"
                    size="large"
                    disabled={isProcessing}
                    startIcon={<CheckIcon />}
                    onClick={handleSendOpen}
                >
                    Senda inn pöntun
                </Button>
            </ProgressButton>
            <ConfirmationDialog
                title="Senda inn pöntun"
                description="Vinsamlegast staðfestu skráningu á nýrri pöntun"
                handleAccept={handleSend}
                handleClose={handleSendClose}
                open={openSendDialog}
            />
            <ConfirmationDialog
                title="Hætta við pöntun"
                description="Viltu hætta við núverandi pöntun?"
                handleAccept={handleCancel}
                handleClose={handleCancelClose}
                open={openCancelDialog}
            />
        </div>
    );
};

CreateOrderActions.propTypes = {
    /** CB that handles creating the order */
    createOrder: createOrderType,
    /** CB that resets the creaete order page */
    cancelOrder: cancelOrderType,
    /** Is data still loading */
    isProcessing: isLoadingType,
};

export default CreateOrderActions;
