import React from "react";
import EditItemAction from "../EditItemAction/EditItemAction";
import "./ItemActions.css";
import DeleteItemAction from "../DeleteItemAction/DeleteItemAction";

const ItemActions = ({ id }) => {
    return (
        <div className="item-actions">
            <DeleteItemAction id={id} />
            <EditItemAction id={id} />
        </div>
    );
};

export default ItemActions;
