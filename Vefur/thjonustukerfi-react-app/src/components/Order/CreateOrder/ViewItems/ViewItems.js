import React from "react";
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Fab,
} from "@material-ui/core";
import RemoveCircleOutlineIcon from "@material-ui/icons/RemoveCircleOutline";
import "./ViewItems.css";
import { itemsType, removeType } from "../../../../types";

const ViewItems = ({ items, remove }) => {
    return (
        <div className="view-items">
            <h3>Vörur</h3>
            <TableContainer component={Paper}>
                <Table stickyHeader aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Tegund</TableCell>
                            <TableCell>Þjónusta</TableCell>
                            <TableCell>Fjöldi</TableCell>
                            <TableCell>Flökun</TableCell>
                            <TableCell>Pökkun</TableCell>
                            <TableCell>Annað</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody className="order-body">
                        {items.map((item, i) => (
                            <TableRow key={i} className="order-row">
                                <TableCell>{item.categoryName}</TableCell>
                                <TableCell>{item.serviceName}</TableCell>
                                <TableCell>{item.amount}</TableCell>
                                <TableCell>
                                    {item.filleted === "filleted"
                                        ? "Flakað"
                                        : "Óflakað"}
                                </TableCell>
                                <TableCell>
                                    {item.sliced === "sliced"
                                        ? "Bitar"
                                        : "Heilt Flak"}
                                </TableCell>
                                <TableCell>{item.details}</TableCell>
                                <TableCell align="right">
                                    <Fab
                                        className="dlt-btn"
                                        onClick={() => remove(item)}
                                        size="small"
                                    >
                                        <RemoveCircleOutlineIcon />
                                    </Fab>
                                </TableCell>
                            </TableRow>
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
