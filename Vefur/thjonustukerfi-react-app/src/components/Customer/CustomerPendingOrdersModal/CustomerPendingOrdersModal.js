import React from "react";
import {
    Dialog,
    DialogContent,
    DialogContentText,
    DialogActions,
    Button,
} from "@material-ui/core";
import { nameType, handleCloseType, openType } from "../../../types/index";

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
    customerName: nameType,
    handleClose: handleCloseType,
    open: openType,
};

export default CustomerPendingOrdersModal;
