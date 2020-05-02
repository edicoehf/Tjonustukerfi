import React from "react";
import {
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    TableContainer,
    Divider,
} from "@material-ui/core";
import OrderListItem from "../OrderListItem/OrderListItem";
import "./OrderList.css";
import { ordersType, isLoadingType } from "../../../types";

const OrderList = ({ orders, isLoading, error }) => {
    return (
        <div>
            {!error ? (
                isLoading ? (
                    <p> Sæki Pantanir </p>
                ) : (
                    <TableContainer
                        component={Paper}
                        elevation={3}
                        className="order-list"
                    >
                        <Table>
                            <TableHead>
                                <TableRow className="order-row">
                                    <TableCell className="order-cell-id">
                                        Pöntunarnúmer
                                    </TableCell>
                                    <TableCell
                                        align="right"
                                        className="order-cell-customer"
                                    >
                                        Viðskiptavinur
                                    </TableCell>
                                    <TableCell
                                        align="right"
                                        className="order-cell-date"
                                    >
                                        Dagsetning
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {orders.map((order, i) => (
                                    <OrderListItem
                                        key={i}
                                        order={order}
                                        border={i !== 0}
                                    />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                )
            ) : (
                <p className="error"> Villa kom upp: Gat ekki sótt pantanir</p>
            )}
        </div>
    );
};

OrderList.propTypes = {
    orders: ordersType,
    isLoading: isLoadingType,
};

export default OrderList;
