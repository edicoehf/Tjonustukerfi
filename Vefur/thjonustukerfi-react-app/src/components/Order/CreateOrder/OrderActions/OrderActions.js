import React from "react";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckIcon from "@material-ui/icons/Check";

const OrderActions = ({}) => {
    return (
        <div className="order-actions">
            <Button
                className="cancel-btn"
                variant="contained"
                color="secondary"
                size="large"
                startIcon={<CancelIcon />}
                onClick={handleSubmit}
            >
                Hætta við
            </Button>
            <Button
                className="confirm-btn"
                variant="contained"
                color="primary"
                size="large"
                startIcon={<CheckIcon />}
                onClick={handleSubmit}
            >
                Senda inn pöntun
            </Button>
        </div>
    );
};

export default OrderActions;
