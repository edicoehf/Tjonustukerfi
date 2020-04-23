import React from "react";
import { Link } from "react-router-dom";
import OrderItemList from "../OrderItemList/OrderItemList";
import useGetOrderById from "../../../hooks/useGetOrderById";
import moment from "moment";
import "moment/locale/is";
import "./OrderDetails.css";
import { orderType } from "../../../types/index";
import {
    TableContainer,
    Table,
    Paper,
    TableRow,
    TableCell,
    TableBody,
} from "@material-ui/core";

const OrderDetails = ({ id }) => {
    const { order, error } = useGetOrderById(id);

    // Icelandic human readable format, e.g. 4. sep, 2020 08:
    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("lll");
    };
    return (
        <div className="order-details">
            {!error ? (
                <TableContainer component={Paper}>
                    <h3 className="order-title">Pöntun {order.id}</h3>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell className="order-barcode">
                                    <b>Strikamerki:</b> {order.barcode}
                                </TableCell>
                                <TableCell className="order-date">
                                    <b>Dagsetning:</b>{" "}
                                    {dateFormat(order.dateCreated)}
                                </TableCell>
                                {order.dateCompleted && (
                                    <TableCell>
                                        <b>Sótt:</b>{" "}
                                        {dateFormat(order.dateCompleted)}
                                    </TableCell>
                                )}
                                <TableCell>
                                    <b>Viðskiptavinur: </b>
                                    <Link
                                        className="customer-link"
                                        to={`/customer/${order.customerId}`}
                                    >
                                        {order.customer}
                                    </Link>
                                </TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                    <OrderItemList items={order.items} />
                </TableContainer>
            ) : (
                <p className="error">Villa kom upp: Gat ekki sótt pöntun</p>
            )}
        </div>
    );
};

OrderDetails.propTypes = {
    order: orderType,
};

export default OrderDetails;
