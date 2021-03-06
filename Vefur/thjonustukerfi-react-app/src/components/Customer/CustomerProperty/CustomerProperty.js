import React from "react";
import { TableRow, TableCell, TextField } from "@material-ui/core";
import { titleType, nameType, valueType } from "../../../types";

/**
 * A table row for properties of a customer, e.g. name or phone
 * Only rendered if value is present, so it can be used for all properties, but only rendered if it has value.
 * This is because some of the customer properties are optional.
 *
 * @component
 * @category Customer
 */

const CustomerProperty = ({ title, name, value, editable, editHandler, enterHandler }) => {
    return (
        <>
            {value || editable ? (
                <TableRow
                    key={name}
                    title={name}
                    className={`customer-details-row ${
                        title !== "Nafn" ? " with-border" : ""
                    }`}
                >
                    <TableCell className="customer-details-title-cell">
                        {title}:
                    </TableCell>
                    <TableCell className="customer-details-content-cell">
                    {editable && value !== null ? (
                            <TextField
                                value={value}
                                name={name}
                                onChange={(ev) => {
                                    editHandler(ev.target.value);
                                   // ev.preventDefault();
                                }}
                                onKeyPress={(ev) => {
                                    if (ev.key === 'Enter') {
                                        enterHandler(ev.target.value);
                                        ev.preventDefault();
                                    }
                              }}/>
                        ) : (
                            value
                        )}
                    </TableCell>
                </TableRow>
            ) : (
                <></>
            )}
        </>
    );
};

/*
                        {editable ? (<input type="text" />) : ({value})}

 */

CustomerProperty.propTypes = {
    /** Title of the property, e.g. "Name" */
    title: titleType,
    /** The name of the property that should be displayed incase customer doesnt have the property, e.g. "Name" */
    name: nameType,
    /** Value of the property, e.g. "John Doe" */
    value: valueType,
};

export default CustomerProperty;
