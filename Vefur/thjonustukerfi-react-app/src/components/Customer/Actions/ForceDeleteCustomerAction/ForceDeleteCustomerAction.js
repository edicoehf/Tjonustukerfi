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

const ForceDeleteCustomerAction = ({ open, handleDelete, handleClose }) => {
    return (
        <Modal open={open}>
            <div className="modal-container">
                <div className="modal-text">
                    <h3>
                        Þessi viðskiptavinur er með pantanir í kerfinu, ertu
                        viss um að þú viljir eyða?
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
    opne: openType,
    handleDelete: handleDeleteType,
    handleClose: handleCloseType,
};

export default ForceDeleteCustomerAction;
