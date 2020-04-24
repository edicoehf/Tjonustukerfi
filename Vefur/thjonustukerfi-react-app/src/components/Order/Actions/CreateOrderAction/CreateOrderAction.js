import React from "react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";
import AddCircleIcon from "@material-ui/icons/AddCircle";
import "./CreateOrderAction.css";

const CreateOrderActions = () => {
    return (
        <div className="create-order">
            <Link to={`/new-order`}>
                <Button
                    className="create-order-button"
                    size="large"
                    variant="contained"
                >
                    <AddCircleIcon className="create-order-icon" />
                    <b>Bæta Við Pöntun</b>
                </Button>
            </Link>
        </div>
    );
};

export default CreateOrderActions;
