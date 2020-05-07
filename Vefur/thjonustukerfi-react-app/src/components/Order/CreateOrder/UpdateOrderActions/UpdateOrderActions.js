import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import { Button } from "@material-ui/core";
import { createOrderType, cancelOrderType } from "../../../../types/index";
import "./UpdateOrderActions.css";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";

const UpdateOrderActions = ({ updateOrder, cancelUpdate, isLoading }) => {
    const [OpenUpdateDialog, setOpenUpdateDialog] = React.useState(false);
    const [openCancelDialog, setOpenCancelDialog] = React.useState(false);

    const handleUpdate = () => {
        handleUpdateClose();
        updateOrder();
    };

    const handleUpdateClose = () => {
        setOpenUpdateDialog(false);
    };

    const handleUpdateOpen = () => {
        setOpenUpdateDialog(true);
    };

    const handleCancel = () => {
        handleCancelClose();
        cancelUpdate();
    };

    const handleCancelClose = () => {
        setOpenCancelDialog(false);
    };

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
    updateOrder: createOrderType,
    cancelOrder: cancelOrderType,
};

export default UpdateOrderActions;
