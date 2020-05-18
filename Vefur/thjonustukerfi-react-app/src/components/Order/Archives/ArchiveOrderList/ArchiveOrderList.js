import React from "react";
import {
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    TableContainer,
} from "@material-ui/core";
import ArchiveOrderListItem from "../ArchiveOrderListItem/ArchiveOrderListItem";
import ProgressComponent from "../../../Feedback/ProgressComponent/ProgressComponent";
import "./ArchiveOrderList.css";
import {
    archivedOrdersType,
    errorType,
    isLoadingType,
} from "../../../../types";

/**
 * Curtain List of archived orders, order expands and shows items of the order onclick
 *
 * @component
 * @category Order
 */
const ArchiveOrderList = ({ orders, isLoading, error }) => {
    // sort orders by id, so newest first
    orders = orders.sort((a, b) => (a.id < b.id ? 1 : b.id < a.id ? -1 : 0));

    // Which order is expanded
    const [expanded, setExpanded] = React.useState(null);

    // Expand an order, or shrink if the expanded one is clicked
    const expand = (key) => {
        if (expanded === key) {
            setExpanded(null);
        } else {
            setExpanded(key);
        }
    };

    return (
        <div>
            {isLoading ? (
                <ProgressComponent isLoading={isLoading} />
            ) : !error ? (
                <TableContainer
                    component={Paper}
                    elevation={3}
                    className="order-archives-list"
                >
                    <Table>
                        <TableHead>
                            <TableRow className="order-row header-row">
                                <TableCell
                                    align="left"
                                    className="order-archives-cell-customer"
                                >
                                    Viðskiptavinur
                                </TableCell>
                                <TableCell
                                    align="left"
                                    className="order-archives-cell-date"
                                >
                                    Skráð
                                </TableCell>
                                <TableCell
                                    align="left"
                                    className="order-archives-cell-date"
                                >
                                    Sótt
                                </TableCell>
                                <TableCell
                                    align="right"
                                    className="order-archives-cell-expanded"
                                >
                                    {" "}
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {orders.map((order, i) => (
                                <ArchiveOrderListItem
                                    key={order.id}
                                    order={order}
                                    border={i !== 0}
                                    expand={expand}
                                    expanded={order.id === expanded}
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

ArchiveOrderList.propTypes = {
    /** List of archived orders */
    orders: archivedOrdersType,
    /** Are the orders still loading */
    isLoading: isLoadingType,
    /** The error that occured while fetching */
    error: errorType,
};

export default ArchiveOrderList;
