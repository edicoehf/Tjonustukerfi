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
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

const OrderDetails = ({ id, update, receivedUpdate }) => {
    const { order, error, fetchOrder, isLoading } = useGetOrderById(id);

    React.useEffect(() => {
        if (update && receivedUpdate) {
            receivedUpdate();
            fetchOrder();
        }
    }, [update, receivedUpdate, fetchOrder]);

    // Icelandic human readable format, e.g. 4. sep, 2020 08:
    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("LLL");
    };

    return (
        <div className="order-details">
            {isLoading ? (
                <ProgressComponent isLoading={isLoading} />
            ) : !error ? (
                <>
                    <TableContainer component={Paper} elevation={3}>
                        <h3 className="order-title">Pöntun {order.id}</h3>
                        <Table className="order-info">
                            <TableBody>
                                <TableRow>
                                    <TableCell className="order-barcode">
                                        <b>Strikamerki:</b> {order.barcode}
                                    </TableCell>
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
                                <TableRow>
                                    <TableCell className="order-date">
                                        <b>Dagsetning:</b>{" "}
                                        {dateFormat(order.dateCreated)}
                                    </TableCell>
                                    <TableCell>
                                        {order.dateCompleted && (
                                            <>
                                                <b>Sótt:</b>{" "}
                                                {dateFormat(
                                                    order.dateCompleted
                                                )}
                                            </>
                                        )}
                                    </TableCell>
                                </TableRow>
                            </TableBody>
                        </Table>
                        <OrderItemList items={order.items} />
                    </TableContainer>
                    {order.items.length === 0 && (
                        <p className="error">
                            Þessi pöntun inniheldur engar vörur
                        </p>
                    )}
                </>
            ) : (
                <p className="error">Gat ekki sótt pöntun</p>
            )}
        </div>
    );
};

OrderDetails.propTypes = {
    order: orderType,
};

export default OrderDetails;
