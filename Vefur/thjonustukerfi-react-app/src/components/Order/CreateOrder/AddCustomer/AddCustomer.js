import React from "react";
import { Button } from "@material-ui/core";
import AddBoxIcon from "@material-ui/icons/AddBox";
import PickCustomerModal from "../PickCustomerModal/PickCustomerModal";

const AddCustomer = ({ customer, addCustomer }) => {
    const [modalOpen, setModalOpen] = React.useState(false);
    const handleOpen = () => {
        setModalOpen(true);
    };
    const handleClose = () => {
        setModalOpen(false);
    };

    return (
        <div className="add-customer">
            <h3>Viðskiptavinur</h3>
            {!customer ? (
                <Button
                    className="sbm-btn"
                    variant="contained"
                    color="primary"
                    size="large"
                    startIcon={<AddBoxIcon />}
                    onClick={handleOpen}
                >
                    Velja viðskiptavin
                </Button>
            ) : (
                <></>
            )}
            <PickCustomerModal open={modalOpen} handleClose={handleClose} />
        </div>
    );
};

export default AddCustomer;
