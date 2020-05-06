import React from "react";
import CustomerDetails from "../CustomerDetails/CustomerDetails";
import "./CustomerView.css";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";
import CustomerOrderListModal from "../CustomerOrderListModal/CustomerOrderListModal";

const CustomerView = ({ match }) => {
    const id = match.params.id;
    const [
        isCustomerOrderModalOpen,
        setCustomerOrderModalOpen,
    ] = React.useState(false);

    const handleClose = () => {
        setCustomerOrderModalOpen(false);
    };

    const handleOpen = () => {
        setCustomerOrderModalOpen(true);
    };

    return (
        <div className="customer-view">
            <h1 className="customer-detail-header">
                Upplýsingar um viðskiptavin
            </h1>
            <CustomerDetails id={id} />
            <div className="customer-detail-action">
                <CustomerActions id={id} handleOpen={handleOpen} />
            </div>
            <CustomerOrderListModal
                customerId={id}
                open={isCustomerOrderModalOpen}
                handleClose={handleClose}
            />
        </div>
    );
};

export default CustomerView;
