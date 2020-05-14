import React from "react";
import { Button } from "@material-ui/core";
import CheckIcon from "@material-ui/icons/Check";
import "./CheckoutOrderAction.css";
import useCheckoutOrderById from "../../../../hooks/useCheckoutOrderById";
import ConfirmationDialog from "../../../Feedback/ConfirmationDialog/ConfirmationDialog";
import ProgressButton from "../../../Feedback/ProgressButton/ProgressButton";
import { idType, cbType } from "../../../../types";

/**
 * Button that marks order as checkout out, has confirmation dialog.
 * This causes all items in the order to go the completed state
 *
 * @component
 * @category Order
 */
const CheckoutOrderAction = ({ id, hasUpdated }) => {
    // Use the checkout order hook, send hasUpdated as cb to be called on success
    // So parent knows that the item order has been updated
    const { error, handleCheckout, isCheckingOut } = useCheckoutOrderById(
        id,
        hasUpdated
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

    // Close dialog and checkout the order
    const handleAccept = () => {
        handleClose();
        if (!isCheckingOut) {
            handleCheckout();
        }
    };

    return (
        <div className="checkout-order">
            <ProgressButton isLoading={isCheckingOut}>
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
            </ProgressButton>
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

CheckoutOrderAction.propTypes = {
    /** Order ID */
    id: idType,
    /** CB that is called when item is checked out, used to let parent know of update */
    hasUpdated: cbType,
};

export default CheckoutOrderAction;
