import React from "react";
import { Button } from "@material-ui/core";
import PrintIcon from "@material-ui/icons/Print";
import "./PrintItemAction.css";

const PrintItemAction = ({ handlePrint }) => {
    return (
        <div className="print-item-action">
            <Button
                className="print-item-button"
                variant="contained"
                size="medium"
                onClick={handlePrint}
            >
                <PrintIcon className="print-icon" size="small" />
                <b>Prenta</b>
            </Button>
        </div>
    );
};

export default PrintItemAction;
