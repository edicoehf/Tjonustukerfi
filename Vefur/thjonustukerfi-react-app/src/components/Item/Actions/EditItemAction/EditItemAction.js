import React from "react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";
import "./EditItemAction.css";

const EditItemAction = ({ id }) => {
    return (
        <div className="edit-item">
            <Link to={`/update-item/${id}`}>
                <Button className="edit-item-button" variant="contained">
                    Breyta
                </Button>
            </Link>
        </div>
    );
};

export default EditItemAction;
