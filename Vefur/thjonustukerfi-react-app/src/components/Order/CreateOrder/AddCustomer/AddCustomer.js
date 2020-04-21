import React from "react";
import { Button } from "@material-ui/core";
import AddBoxIcon from "@material-ui/icons/AddBox";
import EditIcon from "@material-ui/icons/Edit";
import PickCustomerModal from "../PickCustomerModal/PickCustomerModal";
import "./AddCustomer.css";
import CustomerDetails from "../../../Customer/CustomerDetails/CustomerDetails";
import { customerType, addCustomerType } from "../../../../types/index";

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
                    <>
                        <CustomerDetails id={customer.id} />
                        <Button
                            className="edt-btn"
                            variant="contained"
                            color="primary"
                            size="large"
                            startIcon={<EditIcon />}
                            onClick={handleOpen}
                        >
                            Velja annan viðskiptavin
                        </Button>
                    </>
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

AddCustomer.propTypes = {
    customer: customerType,
    addCustomer: addCustomerType,
};

export default AddCustomer;
