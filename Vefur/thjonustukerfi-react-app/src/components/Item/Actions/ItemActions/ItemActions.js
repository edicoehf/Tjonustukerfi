import React from "react";
import EditItemAction from "../EditItemAction/EditItemAction";
import "./ItemActions.css";

const ItemActions = ({ id }) => {
    return (
        <div className="item-actions">
            <EditItemAction id={id} />
        </div>
    );
};

export default ItemActions;
