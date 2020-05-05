import React from "react";
import { Button } from "@material-ui/core";
import VisibilityIcon from "@material-ui/icons/Visibility";
import "./ViewCustomerOrdersAction.css";

const ViewCustomerOrdersAction = ({ handleOpen }) => {
    return (
        <Button
            className="view-button"
            variant="contained"
            color="primary"
            onClick={handleOpen}
        >
            <VisibilityIcon className="view-icon" size="small" />
            <b>Pantanir</b>
        </Button>
    );
};

export default ViewCustomerOrdersAction;
