import React from "react";
import { TableRow, TableCell } from "@material-ui/core";
import { orderType } from "../../../types/index";
import { useHistory } from "react-router-dom";
import moment from "moment";
import "moment/locale/is";
import "./OrderListItem.css";

const OrderListItem = ({ order }) => {
    const history = useHistory();

    const handleRedirect = () => {
        history.push(`/order/${order.id}`);
    };

    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("LL");
    };

    return (
        <>
            <TableRow
                hover={true}
                onClick={handleRedirect}
                className="order-row order-row-body"
            >
                <TableCell className="order-cell-id">{order.id}</TableCell>
                <TableCell align="right" className="order-cell-customer">
                    {order.customer}
                </TableCell>
                <TableCell align="right" className="order-cell-date">
                    {dateFormat(order.dateCreated)}
                </TableCell>
            </TableRow>
        </>
    );
};

OrderListItem.propTypes = {
    order: orderType,
};

export default OrderListItem;
