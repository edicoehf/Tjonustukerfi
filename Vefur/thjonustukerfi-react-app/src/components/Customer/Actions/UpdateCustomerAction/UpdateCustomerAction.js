import React from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { idType } from "../../../../types/index";

const UpdateCustomerAction = ({ id }) => {
    return (
        <div className="update-customer">
            <Link to={`/update-customer/${id}`}>
                <Button variant="warning">Breyta</Button>
            </Link>
        </div>
    );
};

UpdateCustomerAction.propTypes = {
    id: idType,
};

export default UpdateCustomerAction;
