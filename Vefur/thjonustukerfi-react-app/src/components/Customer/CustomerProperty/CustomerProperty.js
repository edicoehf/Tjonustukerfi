import React from "react";
import PropTypes from "prop-types";
import { TableRow, TableCell } from "@material-ui/core";

const CustomerProperty = ({ title, name, value }) => {
    return (
        <>
            {value ? (
                <TableRow
                    key={name}
                    title={name}
                    className="customer-details-row"
                >
                    <TableCell className="customer-details-title-cell">
                        {title}:
                    </TableCell>
                    <TableCell className="customer-details-content-cell">
                        {value}
                    </TableCell>
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
