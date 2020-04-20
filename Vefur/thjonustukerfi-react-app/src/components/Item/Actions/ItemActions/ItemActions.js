import React from "react";
import EditItemAction from "../EditItemAction/EditItemAction";

const ItemActions = ({ id }) => {
    return (
        <div className="item-actions">
            <EditItemAction id={id} />
        </div>
    );
};

export default ItemActions;
