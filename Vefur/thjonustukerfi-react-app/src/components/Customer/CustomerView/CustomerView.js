import React from "react";
import CustomerDetails from "../CustomerDetails/CustomerDetails";
import "./CustomerView.css";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";
import CustomerOrderListModal from "../CustomerOrderListModal/CustomerOrderListModal";

/**
 * A page which displays all details on a customer and available actions (delete, update..)
 *
 * @component
 * @category Customer
 */

const CustomerView = ({ match }) => {
    // Get the Customer ID from the url
    const id = match.params.id;
    // Should order modal be displayed
    const [
        isCustomerOrderModalOpen,
        setCustomerOrderModalOpen,
    ] = React.useState(false);
    // Close the order modal
    const handleClose = () => {
        setCustomerOrderModalOpen(false);
    };
    // Open the order modal
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
