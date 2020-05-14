import React from "react";
import EditItemAction from "../EditItemAction/EditItemAction";
import "./ItemActions.css";
import DeleteItemAction from "../DeleteItemAction/DeleteItemAction";
import { idType } from "../../../../types";

/**
 * Row of the available actions (buttons) for an item
 *
 * @component
 * @category Item
 */

const ItemActions = ({ id }) => {
    return (
        <div className="item-actions">
            <DeleteItemAction id={id} />
            <EditItemAction id={id} />
        </div>
    );
};

ItemActions.propTypes = {
    /** Item ID */
    id: idType,
};

export default ItemActions;
