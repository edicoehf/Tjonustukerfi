import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";
import ConfirmationDialog from "../../../ConfirmationDialog/ConfirmationDialog";

const OrderActions = ({ createOrder }) => {
    [openDialog, setOpenDialog] = React.useState(false);

    const handleAccept = () => {
        createOrder();
        handleClose();
    };

    const handleClose = () => {
        setOpenDialog(false);
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
                onClick={handleSubmit}
            >
                Senda inn pöntun
            </Button>
            <ConfirmationDialog
                title="Senda inn pöntun"
                description="Vinsamlegast staðfestu skráningu á nýrri pöntun"
                handleAccept={handleAccept}
                handleClose={handleClose}
                open={openDialog}
            />
        </div>
    );
};

export default OrderActions;
