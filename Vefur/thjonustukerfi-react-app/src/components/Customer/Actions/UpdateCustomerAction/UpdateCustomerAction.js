import React from "react";
import { Button } from "@material-ui/core";
import EditIcon from "@material-ui/icons/Edit";
import { Link } from "react-router-dom";
import { idType } from "../../../../types/index";
import "./UpdateCustomerAction.css";

/**
 * Button which opens the page used for updating customers info.
 * /update-customer
 */
const UpdateCustomerAction = ({ id }) => {
    return (
        <div className="update-customer">
            <Link to={`/update-customer/${id}`}>
                <Button
                    className="update-button"
                    size="medium"
                    variant="contained"
                >
                    <EditIcon className="update-icon" size="small" />
                    <b>Breyta</b>
                </Button>
            </Link>
        </div>
    );
};

UpdateCustomerAction.propTypes = {
    /** Customer ID */
    id: idType,
};

export default UpdateCustomerAction;
