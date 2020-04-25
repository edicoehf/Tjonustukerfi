import React from "react";
import { Button } from "@material-ui/core";
import CheckIcon from "@material-ui/icons/Check";
import "./CheckoutOrderAction.css";
import useCheckoutOrderById from "../../../../hooks/useCheckoutOrderById";
import ConfirmationDialog from "../../../ConfirmationDialog/ConfirmationDialog";

const CheckoutOrderAction = ({ id }) => {
    const { error, handleCheckout, isCheckingOut } = useCheckoutOrderById(id);
    const [open, setOpen] = React.useState(false);

    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAccept = () => {
        handleClose();
        if (!isCheckingOut) {
            handleCheckout();
        }
    };

    return (
        <div className="checkout-order">
            <Button
                className="checkout-order-button"
                size="medium"
                color="primary"
                variant="contained"
                disabled={isCheckingOut}
                onClick={handleOpen}
            >
                <CheckIcon className="checkout-icon" size="small" />
                <b>Skrá sótt</b>
            </Button>
            <ConfirmationDialog
                title="Skrá pöntun sótta"
                description="Staðfestu að skrá eigi pöntun sótta"
                handleClose={handleClose}
                handleAccept={handleAccept}
                open={open}
            />
            {error && (
                <p className="checkout-error">Gat ekki skráð pöntun sótta</p>
            )}
        </div>
    );
};

export default CheckoutOrderAction;
