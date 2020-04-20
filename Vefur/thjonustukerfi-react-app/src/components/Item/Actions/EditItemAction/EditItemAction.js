import React from "react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";

const EditItemAction = ({ id }) => {
    return (
        <div className="edit-item">
            <Link to={`/edit-item/${id}`}>
                <Button color="amber">Breyta</Button>
            </Link>
        </div>
    );
};

export default EditItemAction;
