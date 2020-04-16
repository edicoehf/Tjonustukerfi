import React from "react";
import { Button } from "@material-ui/core";
import AddBoxIcon from "@material-ui/icons/AddBox";
import PickCustomerModal from "../PickCustomerModal/PickCustomerModal";
import "./AddCustomer.css";
import CustomerDetails from "../../../Customer/CustomerDetails/CustomerDetails";

const AddCustomer = ({ customer, addCustomer }) => {
    const [modalOpen, setModalOpen] = React.useState(false);
    const handleOpen = () => {
        setModalOpen(true);
    };
    const handleClose = () => {
        setModalOpen(false);
    };

    return (
        <>
            <div className="add-customer">
                <h3>Viðskiptavinur</h3>
                {!customer ? (
                    <Button
                        className="pck-btn"
                        variant="contained"
                        color="primary"
                        size="large"
                        startIcon={<AddBoxIcon />}
                        onClick={handleOpen}
                    >
                        Velja viðskiptavin
                    </Button>
                ) : (
                    <CustomerDetails id={customer.id} />
                )}
            </div>
            <PickCustomerModal
                open={modalOpen}
                handleClose={handleClose}
                addCustomer={addCustomer}
            />
        </>
    );
};

export default AddCustomer;
