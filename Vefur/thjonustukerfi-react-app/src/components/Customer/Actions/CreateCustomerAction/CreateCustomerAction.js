import React from "react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";
import AddCircleIcon from "@material-ui/icons/AddCircle";
import "./CreateCustomerAction.css";

/**
 * A button which opens the page used to create a new customer.
 *
 * @component
 * @category Customer
 */

const CreateCustomerAction = () => {
    return (
        <div className="create-customer">
            <Link to={`/new-customer`}>
                <Button
                    className="create-button"
                    size="large"
                    variant="contained"
                >
                    <AddCircleIcon className="create-icon" />
                    <b>Bæta Við Viðskiptavin</b>
                </Button>
            </Link>
        </div>
    );
};

export default CreateCustomerAction;
