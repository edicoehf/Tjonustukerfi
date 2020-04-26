import React from "react";
import { idType } from "../../../../types/index";
import EditIcon from "@material-ui/icons/Edit";
import { Link, Button } from "@material-ui/core";
import "./UpdateOrderAction.css";

const UpdateOrderAction = ({ id }) => {
    return (
        <div className="update-order">
            <Link to={`/update-order/${id}`}>
                <Button
                    className="update-order-button"
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

UpdateOrderAction.propTypes = {
    id: idType,
};

export default UpdateOrderAction;
