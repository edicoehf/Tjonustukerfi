import React from "react";
import { TableRow, TableCell, ListItemText } from "@material-ui/core";
import moment from "moment";
import "moment/locale/is";
import ArchiveOrderItems from "../ArchiveOrderItems/ArchiveOrderItems";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import {
    archivedOrderType,
    cbType,
    expandedType,
    borderType,
} from "../../../../types";

// Parse datetime in (icelandic) human readable format
const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("l");
};

/**
 * A row in the archivedorderlist table.
 * Shows customer name and email along with creation and pickup date
 *
 * @component
 * @category Order
 */
const ArchiveOrderListItem = ({ order, border, expand, expanded }) => {
    const toggleShow = () => {
        expand(order.id);
    };

    return (
        <TableRow
            className={`order-row order-row-body ${
                border === true ? "with-border" : ""
            } ${expanded ? "selected" : ""}`}
            onClick={toggleShow}
        >
            <TableCell align="left" className="order-archives-cell-customer">
                <ListItemText
                    primary={order.customer}
                    secondary={order.customerEmail}
                />
            </TableCell>
            <TableCell align="left" className="order-archives-cell-created">
                {dateFormat(order.dateCreated)}
            </TableCell>
            <TableCell align="left" className="order-archives-cell-completed">
                {dateFormat(order.dateCompleted)}
            </TableCell>
            <TableCell align="right" className="order-archives-cell-expanded">
                <ExpandMoreIcon />
            </TableCell>
            {expanded && (
                <TableCell className="order-archives-cell-info">
                    <ArchiveOrderItems order={order.items} />
                </TableCell>
            )}
        </TableRow>
    );
};

ArchiveOrderListItem.propTypes = {
    /** Archived order to represent */
    order: archivedOrderType,
    /** Should the row have a top border */
    border: borderType,
    /** CB to expand the row, called on row click */
    expand: cbType,
    /** Is the row expanded */
    expanded: expandedType,
};

export default ArchiveOrderListItem;
