import React from "react";
import CustomerDetails from "../CustomerDetails/CustomerDetails";
import "./CustomerView.css";
import CustomerActions from "../Actions/CustomerActions/CustomerActions";

const CustomerView = ({ match }) => {
    const id = match.params.id;

    return (
        <div className="customer-view">
            <h1 className="customer-detail-header">
                Upplýsingar um viðskiptavin
            </h1>
            <CustomerDetails id={id} />
            <div className="customer-detail-action">
                <CustomerActions id={id} />
            </div>
        </div>
    );
};

export default CustomerView;
