import React from "react";
import { Button, Snackbar } from "@material-ui/core";
import MuiAlert from "@material-ui/lab/Alert";

const SuccessToaster = ({ success, receivedSuccess, message, cb, cbText }) => {
    const [open, setOpen] = React.useState(false);

    if (success && !open) {
        setOpen(true);
        receivedSuccess();
    }

    const Alert = (props) => {
        return <MuiAlert elevation={6} variant="filled" {...props} />;
    };

    const handleClose = (event, reason) => {
        if (reason === "clickaway") {
            return;
        }

        setOpen(false);
    };

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

export default SuccessToaster;
