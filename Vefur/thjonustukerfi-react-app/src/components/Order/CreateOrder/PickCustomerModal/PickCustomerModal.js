import React from "react";
import Modal from "@material-ui/core/Modal";
import Backdrop from "@material-ui/core/Backdrop";
import Fade from "@material-ui/core/Fade";
import CustomerSelectView from "../CustomerSelect/CustomerSelectView/CustomerSelectView";
import "./PickCustomerModal.css";

const PickCustomerModal = ({ open, handleClose }) => {
    return (
        <Modal
            className="pick-customer-modal"
            open={open}
            onClose={handleClose}
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{
                timeout: 500,
            }}
        >
            <Fade in={open}>
                <div className="fade-modal">
                    <h2 id="customer-modal-title">Viðskiptavinir</h2>
                    <CustomerSelectView />
                </div>
            </Fade>
        </Modal>
    );
};

export default PickCustomerModal;
