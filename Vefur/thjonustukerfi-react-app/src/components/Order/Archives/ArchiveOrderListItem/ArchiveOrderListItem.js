import React from "react";
import { TableRow, TableCell, ListItemText } from "@material-ui/core";
import moment from "moment";
import "moment/locale/is";
import ArchiveOrderItems from "../ArchiveOrderItems/ArchiveOrderItems";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";

const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("l");
};

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

export default ArchiveOrderListItem;
