import React from "react";
import { Button } from "react-bootstrap";
import { idType } from "../../../../types/index";

const UpdateOrderAction = ({ id }) => {
    return (
        <div className="update-order">
            <Button variant="warning" href={`/update-order/${id}`}>
                Breyta
            </Button>
        </div>
    );
};

UpdateOrderAction.propTypes = {
    id: idType,
};

export default UpdateOrderAction;
