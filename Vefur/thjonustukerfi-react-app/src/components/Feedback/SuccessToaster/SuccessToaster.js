import React from "react";
import { Button, Snackbar } from "@material-ui/core";
import MuiAlert from "@material-ui/lab/Alert";
import { descriptionType, cbType, successType } from "../../../types";

/**
 * Show a success toaster
 *
 * @component
 * @category Feedback
 */

const SuccessToaster = ({ success, receivedSuccess, message, cb, cbText }) => {
    // Is toaster displayed
    const [open, setOpen] = React.useState(false);

    // Set toaster open and call CB on success (if not already open)
    React.useEffect(() => {
        if (success && !open) {
            setOpen(true);
            receivedSuccess();
        }
    }, [success, open, receivedSuccess]);

    // Alert factor of the toaster
    const Alert = (props) => {
        return <MuiAlert elevation={6} variant="filled" {...props} />;
    };

    // Close toaster
    const handleClose = (event, reason) => {
        if (reason === "clickaway") {
            return;
        }

        setOpen(false);
    };

    // Optional button that calls given CB
    const action = cb ? (
        <Button size="small" onClick={cb}>
            {cbText}
        </Button>
    ) : (
        <></>
    );
    return (
        <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
            <Alert onClose={handleClose} severity="success" action={action}>
                {message}
            </Alert>
        </Snackbar>
    );
};

SuccessToaster.protoTypes = {
    /**  Was operation successful */
    success: successType,
    /** CB to let parent know it has received that the operation was successful */
    receivedSuccess: cbType,
    /** Message to display in toaster */
    message: descriptionType,
    /** Optional callback function for button */
    cb: cbType,
    /** Text in button for optional cb */
    cbText: descriptionType,
};

export default SuccessToaster;
