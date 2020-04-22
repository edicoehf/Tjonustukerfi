import React from "react";
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    Button,
} from "@material-ui/core";
import {
    titleType,
    descriptionType,
    handleAcceptType,
    declineTextType,
    confirmTextType,
    handleCloseType,
    openType,
} from "../../types/index";

const ConfirmationDialog = ({
    title,
    description,
    handleAccept,
    handleClose,
    open,
    declineText,
    confirmText,
}) => {
    return (
        <Dialog open={open} onClose={handleClose}>
            {title && (
                <DialogTitle id="alert-dialog-title">{title}</DialogTitle>
            )}
            {description && (
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {description}
                    </DialogContentText>
                </DialogContent>
            )}
            <DialogActions>
                <Button onClick={handleClose} color="primary">
                    {declineText ? declineText : "Til baka"}
                </Button>
                <Button onClick={handleAccept} color="primary" autoFocus>
                    {confirmText ? confirmText : "Staðfesta"}
                </Button>
            </DialogActions>
        </Dialog>
    );
};

ConfirmationDialog.propTypes = {
    title: titleType,
    description: descriptionType,
    handleAccept: handleAcceptType,
    handleClose: handleCloseType,
    open: openType,
    declineText: declineTextType,
    confirmText: confirmTextType,
};

export default ConfirmationDialog;
