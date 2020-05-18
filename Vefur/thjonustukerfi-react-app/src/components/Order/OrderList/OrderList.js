import React from "react";
import {
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    TableContainer,
    FormControlLabel,
    Switch,
    Toolbar,
} from "@material-ui/core";
import OrderListItem from "../OrderListItem/OrderListItem";
import "./OrderList.css";
import { ordersType, isLoadingType, errorType } from "../../../types";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import useFilterOrders from "../../../hooks/useFilterOrders";

/**
 * List orders as table
 *
 * @component
 * @category Order
 */

const OrderList = ({ orders, isLoading, error }) => {
    // Sort orders by id, so newest come first
    orders = orders.sort((a, b) => (a.id < b.id ? 1 : b.id < a.id ? -1 : 0));
    // Use filter orders, when used it toggles whether orders that have been picked up are shown or not
    const { filtered, filterToggle, shouldFilter } = useFilterOrders(orders);

    return (
        <div>
            {isLoading ? (
                <ProgressComponent isLoading={isLoading} />
            ) : !error ? (
                <TableContainer
                    component={Paper}
                    elevation={3}
                    className="order-list"
                >
                    <Toolbar className="order-list-toolbar">
                        <FormControlLabel
                            className="filter-toggle"
                            control={
                                <Switch
                                    checked={!shouldFilter}
                                    onChange={filterToggle}
                                />
                            }
                            label="Birta sóttar pantanir"
                        />
                    </Toolbar>
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
                            {filtered.map((order, i) => (
                                <OrderListItem
                                    key={i}
                                    order={order}
                                    border={i !== 0}
                                />
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            ) : (
                <p className="error">Gat ekki sótt pantanir</p>
            )}
        </div>
    );
};

OrderList.propTypes = {
    /** List of orders */
    orders: ordersType,
    /** Is data still loading */
    isLoading: isLoadingType,
    /** Error that occurred */
    error: errorType,
};

export default OrderList;
