import React from "react";
import { Modal, Backdrop, Fade, AppBar, Tabs, Tab } from "@material-ui/core";
import CustomerSelectView from "../CustomerSelect/CustomerSelectView/CustomerSelectView";
import "./PickCustomerModal.css";
import AddNewCustomer from "../CustomerSelect/AddNewCustomer/AddNewCustomer";
import {
    openType,
    handleCloseType,
    addCustomerType,
} from "../../../../types/index";

// Display components as tabs
const TabPanel = ({ value, index, children }) => {
    return <>{value === index && children}</>;
};

/**
 * Modal used for picking a customer for an order,
 * has 2 tabs, select customer and create customer
 *
 * @component
 * @category Order
 */
const PickCustomerModal = ({ open, handleClose, addCustomer }) => {
    // which tab is displayed
    const [tab, setTab] = React.useState(0);

    // Change which tab to display
    const handleChangeTab = (event, newValue) => {
        setTab(newValue);
    };

    // Add a customer to the order and close modal
    const addCustomerAndClose = (customer) => {
        addCustomer(customer);
        handleClose();
    };

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
                    <AppBar position="static">
                        <Tabs
                            onChange={handleChangeTab}
                            aria-label="simple tabs example"
                            value={tab}
                            centered
                        >
                            <Tab label="Leita að viðskiptavin" />
                            <Tab label="Nýr viðskiptavinur" />
                        </Tabs>
                    </AppBar>
                    <TabPanel index={0} value={tab}>
                        <div className="tab">
                            <h2 id="customer-modal-title">Viðskiptavinir</h2>
                            <CustomerSelectView
                                addCustomer={addCustomerAndClose}
                            />
                        </div>
                    </TabPanel>
                    <TabPanel index={1} value={tab}>
                        <div className="tab">
                            <AddNewCustomer addCustomer={addCustomerAndClose} />
                        </div>
                    </TabPanel>
                </div>
            </Fade>
        </Modal>
    );
};

PickCustomerModal.propTypes = {
    /** Is the modal open */
    open: openType,
    /** CB that closes modal */
    handleClose: handleCloseType,
    /** CB that adds customer to an order */
    addCustomer: addCustomerType,
};

export default PickCustomerModal;
