import React from "react";
import PropTypes from "prop-types";
import "./CustomerProperty.css";
import { TableRow, TableCell } from "@material-ui/core";

const CustomerProperty = ({ title, name, value }) => {
    return (
        <>
            {value ? (
                <TableRow key={name} title={name}>
                    <TableCell>{title}:</TableCell>
                    <TableCell>{value}</TableCell>
                </TableRow>
            ) : (
                <></>
            )}
        </>
    );
};

CustomerProperty.propTypes = {
    title: PropTypes.string.isRequired,
    value: PropTypes.string,
};

export default CustomerProperty;
