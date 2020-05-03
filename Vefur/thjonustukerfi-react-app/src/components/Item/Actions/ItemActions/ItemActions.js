import React from "react";
import EditItemAction from "../EditItemAction/EditItemAction";
import "./ItemActions.css";
import DeleteItemAction from "../DeleteItemAction/DeleteItemAction";
import PrintItemAction from "../PrintItemAction/PrintItemAction";

const ItemActions = ({ id, handlePrint }) => {
    return (
        <div className="item-actions">
            <DeleteItemAction id={id} />
            <EditItemAction id={id} />
            <PrintItemAction id={id} handlePrint={handlePrint} />
        </div>
    );
};

export default ItemActions;
