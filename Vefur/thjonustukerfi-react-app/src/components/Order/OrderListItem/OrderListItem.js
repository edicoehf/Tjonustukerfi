import React from "react";
import { TableRow, TableCell } from "@material-ui/core";
import { orderType } from "../../../types/index";
import { Redirect } from "react-router-dom";
import moment from "moment";
import "moment/locale/is";
import "./OrderListItem.css";

const OrderListItem = ({ order }) => {
    const [redirect, setRedirect] = React.useState(false);

    const handleRedirect = () => {
        setRedirect(true);
    };

    const renderRedirect = () => {
        if (redirect) {
            return <Redirect to={`/order/${order.id}`} />;
        }
    };

    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("LL");
    };

    return (
        <>
            {renderRedirect()}
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
