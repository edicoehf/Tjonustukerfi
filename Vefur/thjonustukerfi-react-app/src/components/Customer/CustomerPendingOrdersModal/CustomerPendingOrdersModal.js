import React from "react";
import {
    Dialog,
    DialogContent,
    DialogContentText,
    DialogActions,
    Button,
} from "@material-ui/core";
import { nameType, handleCloseType, openType } from "../../../types/index";

/**
 * A modal which notifies the user that a customer has orders ready to be picked up.
 */

const CustomerPendingOrdersModal = ({ customerName, open, handleClose }) => {
    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogContent>
                <DialogContentText>
                    {`${customerName} á ósóttar pantanir`}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose} color="primary">
                    Loka
                </Button>
            </DialogActions>
        </Dialog>
    );
};

CustomerPendingOrdersModal.propTypes = {
    /** The name of the customer */
    customerName: nameType,
    /** CB function that closes modal */
    handleClose: handleCloseType,
    /** Should modal be displayed */
    open: openType,
};

export default CustomerPendingOrdersModal;
