import React from "react";
import "moment/locale/is";
import {
    TableContainer,
    Table,
    Paper,
    TableRow,
    TableCell,
    TableBody,
    TableHead,
} from "@material-ui/core";
import "./ArchiveOrderItems.css";
import { archivedOrderItemsType } from "../../../../types";

/**
 * Table showing the items of an archived order
 *
 * @component
 * @category Order
 */
const ArchiveOrderItems = ({ order }) => {
    const [items, setItems] = React.useState([]);

    React.useEffect(() => {
        if (order) {
            let nItems = [];
            order.forEach((item) => {
                const index = nItems.findIndex(
                    (i) =>
                        i.category === item.category &&
                        i.service === item.service
                );
                if (index > 0) {
                    nItems[index].amount++;
                } else {
                    nItems.push({
                        category: item.category,
                        service: item.service,
                        amount: 1,
                    });
                }
                setItems(nItems);
            });
        }
    }, [order]);

    return (
        <TableContainer
            component={Paper}
            elevation={1}
            className="order-archives-item-list"
        >
            <Table className="order-info">
                <TableHead>
                    <TableRow className="order-info-row">
                        <TableCell align="left" className="order-cell-customer">
                            Tegund
                        </TableCell>
                        <TableCell align="left" className="order-cell-date">
                            Þjónusta
                        </TableCell>
                        <TableCell align="left" className="order-cell-date">
                            Fjöldi
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((item, i) => (
                        <TableRow
                            className={`order-info-row ${
                                i !== 0 ? "with-border" : ""
                            }`}
                            key={i}
                        >
                            <TableCell
                                align="left"
                                className="order-info-cell-category"
                            >
                                {item.category}
                            </TableCell>
                            <TableCell
                                align="left"
                                className="order-info-cell-service"
                            >
                                {item.service}
                            </TableCell>
                            <TableCell
                                align="left"
                                className="order-info-cell-amount"
                            >
                                {item.amount}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

ArchiveOrderItems.propTypes = {
    /** Items to show */
    order: archivedOrderItemsType,
};

export default ArchiveOrderItems;
