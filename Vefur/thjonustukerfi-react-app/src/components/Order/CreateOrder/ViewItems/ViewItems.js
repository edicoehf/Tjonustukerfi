import React from "react";
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    IconButton,
} from "@material-ui/core";
import ClearIcon from "@material-ui/icons/Clear";
import "./ViewItems.css";
import { itemsType, removeType } from "../../../../types";

/**
 * Display table of the items that have been added to the order, with the option of removing an item
 *
 * @component
 * @category Order
 */
const ViewItems = ({ items, remove }) => {
    return (
        <div className="view-items">
            <h3>Vörur</h3>
            <TableContainer component={Paper} elevation={3}>
                <Table stickyHeader aria-label="simple table">
                    <TableHead>
                        <TableRow className="item-row">
                            <TableCell className="item-cell-category">
                                Tegund
                            </TableCell>
                            <TableCell className="item-cell-service">
                                Þjónusta
                            </TableCell>
                            <TableCell className="item-cell-filleted">
                                Flökun
                            </TableCell>
                            <TableCell className="item-cell-sliced">
                                Pökkun
                            </TableCell>
                            <TableCell className="item-cell-amount">
                                Fjöldi
                            </TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody className="order-body">
                        {items.map((item, i) => (
                            <React.Fragment key={i}>
                                <TableRow
                                    key={i}
                                    className={`item-row ${
                                        i !== 0 ? "with-border" : ""
                                    }`}
                                >
                                    <TableCell className="item-cell-category">
                                        {item.otherCategory
                                            ? item.otherCategory
                                            : item.categoryName}
                                    </TableCell>
                                    <TableCell className="item-cell-services">
                                        {item.otherService
                                            ? item.otherService
                                            : item.serviceName}
                                    </TableCell>
                                    <TableCell className="item-cell-filleted">
                                        {item.filleted === "filleted"
                                            ? "Já"
                                            : "Nei"}
                                    </TableCell>
                                    <TableCell className="item-cell-sliced">
                                        {item.sliced === "sliced"
                                            ? "Bitar"
                                            : "Heilt Flak"}
                                    </TableCell>
                                    <TableCell className="item-cell-amount">
                                        {item.amount}
                                    </TableCell>
                                    <TableCell className="item-cell-remove">
                                        <IconButton
                                            color="secondary"
                                            component="span"
                                            onClick={() => remove(item)}
                                        >
                                            <ClearIcon />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                                {item.details !== "" && (
                                    <TableRow className="item-row-details">
                                        <TableCell className="item-cell-details-title">
                                            <b>Annað:</b>
                                        </TableCell>
                                        <TableCell className="item-cell-details-content">
                                            {item.details}
                                        </TableCell>
                                    </TableRow>
                                )}
                            </React.Fragment>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    );
};

ViewItems.propTypes = {
    items: itemsType,
    remove: removeType,
};

export default ViewItems;
