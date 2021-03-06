import React from "react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";
import EditIcon from "@material-ui/icons/Edit";
import "./EditItemAction.css";
import { idType } from "../../../../types";

/**
 * Button that sends user to the page to update item information
 *
 * @component
 * @category Item
 */

const EditItemAction = ({ id }) => {
    return (
        <div className="edit-item-action">
            <Link to={`/update-item/${id}`}>
                <Button
                    className="edit-item-button"
                    variant="contained"
                    size="medium"
                >
                    <EditIcon className="update-icon" size="small" />
                    <b>Breyta</b>
                </Button>
            </Link>
        </div>
    );
};

EditItemAction.propTypes = {
    /** Item ID */
    id: idType,
};

export default EditItemAction;
