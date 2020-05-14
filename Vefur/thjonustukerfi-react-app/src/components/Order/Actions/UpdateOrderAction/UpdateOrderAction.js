import React from "react";
import { idType } from "../../../../types/index";
import EditIcon from "@material-ui/icons/Edit";
import { Button } from "@material-ui/core";
import "./UpdateOrderAction.css";

/**
 * Button that sends user to the page to update order.
 *
 * @component
 * @category Order
 */
const UpdateOrderAction = ({ id }) => {
    return (
        <div className="update-order">
            <Button
                className="update-order-button"
                size="medium"
                variant="contained"
                href={`/update-order/${id}`}
            >
                <EditIcon className="update-icon" size="small" />
                <b>Breyta</b>
            </Button>
        </div>
    );
};

UpdateOrderAction.propTypes = {
    /** Order ID */
    id: idType,
};

export default UpdateOrderAction;
