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

const TabPanel = ({ value, index, children }) => {
    return <>{value === index && children}</>;
};

const PickCustomerModal = ({ open, handleClose, addCustomer }) => {
    const [tab, setTab] = React.useState(0);

    const handleChangeTab = (event, newValue) => {
        setTab(newValue);
    };

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
    open: openType,
    handleClose: handleCloseType,
    addCustomer: addCustomerType,
};

export default PickCustomerModal;
