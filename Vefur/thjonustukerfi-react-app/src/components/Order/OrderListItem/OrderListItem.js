import React from "react";
import { TableRow, TableCell, ListItemText } from "@material-ui/core";
import { orderType, borderType } from "../../../types/index";
import { useHistory } from "react-router-dom";
import moment from "moment";
import "moment/locale/is";

/**
 * Row in orderlist table.
 * Displays Customer name and email along with order creation date
 * Sends user to details page for order on click
 *
 * @component
 * @category Order
 */

const OrderListItem = ({ order, border }) => {
    // Get history
    const history = useHistory();
    // Send user to details page for order
    const handleRedirect = () => {
        history.push(`/order/${order.id}`);
    };

    // Parse datetime in (icelandic) human readable format
    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("LL");
    };

    return (
        <>
            <TableRow
                hover={true}
                onClick={handleRedirect}
                className={`order-row order-row-body ${
                    border === true ? "with-border" : ""
                }`}
            >
                <TableCell className="order-cell-id">{order.id}</TableCell>
                <TableCell align="right" className="order-cell-customer">
                    <ListItemText
                        primary={order.customer}
                        secondary={order.customerEmail}
                    />
                </TableCell>
                <TableCell align="right" className="order-cell-date">
                    {dateFormat(order.dateCreated)}
                </TableCell>
            </TableRow>
        </>
    );
};

OrderListItem.propTypes = {
    /** Order to represent */
    order: orderType,
    /** Should row have a top-border */
    border: borderType,
};

export default OrderListItem;
