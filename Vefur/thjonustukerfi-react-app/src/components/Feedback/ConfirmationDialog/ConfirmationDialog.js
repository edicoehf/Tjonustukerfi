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
} from "../../../types/index";

/**
 * Display a confirmation dialog with custom text and handlers
 *
 * @component
 * @category Feedback
 */
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
    /**
     * Displayed title in dialog
     */
    title: titleType,
    /**
     * Displayed description in dialog
     */
    description: descriptionType,
    /**
     * Callback function for accept
     */
    handleAccept: handleAcceptType,
    /**
     * Callback function for close/decline
     */
    handleClose: handleCloseType,
    /**
     * Is dialog displayed/open
     */
    open: openType,
    /**
     * Custom text for decline button
     */
    declineText: declineTextType,
    /**
     * Custom text for confirm button
     */
    confirmText: confirmTextType,
};

export default ConfirmationDialog;
