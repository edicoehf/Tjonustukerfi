import React from "react";
import { Modal, Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import CancelIcon from "@material-ui/icons/Cancel";

import "./ForceDeleteCustomerAction.css";
import {
    openType,
    handleDeleteType,
    handleCloseType,
} from "../../../../types/index";

/**
 * Fullscreen modal for confirmation of deleting a customer
 * which has active orders in the system.
 *
 * @component
 * @category Customer
 */
const ForceDeleteCustomerAction = ({ open, handleDelete, handleClose }) => {
    return (
        <Modal
            open={open}
            onClose={handleClose}
            onBackdropClick={handleClose}
            className="force-modal"
        >
            <div className="modal-container" onClick={handleClose}>
                <div className="modal-text">
                    <h3>
                        Þessi viðskiptavinur er með pantanir í kerfinu.
                        <br />
                        Staðfestu að eyði skuli viðskiptavini og öllum þeim
                        pöntunum.
                    </h3>
                </div>
                <div className="modal-buttons">
                    <Button
                        variant="outlined"
                        color="primary"
                        onClick={handleClose}
                    >
                        <CancelIcon />
                        <h6 className="modal-button-text">Hætta við</h6>
                    </Button>
                    <Button
                        variant="outlined"
                        color="secondary"
                        onClick={handleDelete}
                    >
                        <DeleteIcon />
                        <h6 className="modal-button-text">Eyða</h6>
                    </Button>
                </div>
            </div>
        </Modal>
    );
};

ForceDeleteCustomerAction.propTypes = {
    /**
     * Boolean controlling if modal is displayed or not
     */
    open: openType,
    /**
     * Callback function triggered on modal confirm
     */
    handleDelete: handleDeleteType,
    /**
     * Callback function triggered on modal close
     */
    handleClose: handleCloseType,
};

export default ForceDeleteCustomerAction;
