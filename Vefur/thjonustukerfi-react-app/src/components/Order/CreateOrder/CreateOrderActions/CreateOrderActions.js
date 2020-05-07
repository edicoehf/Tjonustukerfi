import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import { Button } from "@material-ui/core";
import { createOrderType, cancelOrderType } from "../../../../types/index";
import "./CreateOrderActions.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";

const CreateOrderActions = ({ createOrder, cancelOrder, isProcessing }) => {
    const [openSendDialog, setOpenSendDialog] = React.useState(false);
    const [openCancelDialog, setOpenCancelDialog] = React.useState(false);

    const handleSend = () => {
        handleSendClose();
        createOrder();
    };

    const handleSendClose = () => {
        setOpenSendDialog(false);
    };

    const handleSendOpen = () => {
        setOpenSendDialog(true);
    };

    const handleCancel = () => {
        handleCancelClose();
        cancelOrder();
    };

    const handleCancelClose = () => {
        setOpenCancelDialog(false);
    };

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
    createOrder: createOrderType,
    cancelOrder: cancelOrderType,
};

export default CreateOrderActions;
