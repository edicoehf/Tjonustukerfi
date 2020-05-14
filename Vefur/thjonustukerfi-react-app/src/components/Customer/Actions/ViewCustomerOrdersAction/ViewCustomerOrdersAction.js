import React from "react";
import { Button } from "@material-ui/core";
import VisibilityIcon from "@material-ui/icons/Visibility";
import "./ViewCustomerOrdersAction.css";
import { handleOpenType } from "../../../../types";

/**
 * A button which triggers callback function from props.
 * Used for opening the modal which displays a customers orderlist
 *
 * @component
 * @category Customer
 */

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

ViewCustomerOrdersAction.propTypes = {
    /** CB function triggered when button is clicked */
    handleOpen: handleOpenType,
};

export default ViewCustomerOrdersAction;
