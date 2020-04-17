import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";
import ConfirmationDialog from "../../../ConfirmationDialog/ConfirmationDialog";

const OrderActions = ({ createOrder, cancelOrder }) => {
    [openSendDialog, setOpenSendDialog] = React.useState(false);
    [openCancelDialog, setOpenCancelDialog] = React.useState(false);

    const handleSend = () => {
        createOrder();
        handleSendClose();
    };

    const handleSendClose = () => {
        setOpenSendDialog(false);
    };

    const handleSendOpen = () => {
        setOpenSendDialog(true);
    };

    const handleCancel = () => {
        cancelOrder();
        handleCancelClose();
    };

    const handleCancelClose = () => {
        setOpenCancelDialog(false);
    };

    const handleCancelOpen = () => {
        setOpenCancelDialog(true);
    };

    return (
        <div className="order-actions">
            <Button
                className="cancel-btn"
                variant="contained"
                color="secondary"
                size="large"
                startIcon={<CancelIcon />}
                onClick={handleSubmit}
            >
                Hætta við
            </Button>
            <Button
                className="confirm-btn"
                variant="contained"
                color="primary"
                size="large"
                startIcon={<CheckIcon />}
                onClick={handleOpen}
            >
                Senda inn pöntun
            </Button>
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

export default OrderActions;
