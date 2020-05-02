import React from "react";
import CustomerProperty from "../CustomerProperty/CustomerProperty";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import { TableContainer, Table, Paper, TableBody } from "@material-ui/core";
import "./CustomerDetails.css";
import { idType } from "../../../types/index";

const CustomerDetails = ({ id }) => {
    const { customer, error } = useGetCustomerById(id);

    return (
        <div className="customer-details">
            {!error ? (
                <TableContainer component={Paper} elevation={3}>
                    <Table aria-label="caption table">
                        <TableBody>
                            <CustomerProperty
                                title="Nafn"
                                name="name"
                                value={customer.name}
                            />
                            <CustomerProperty
                                title="Kennitala"
                                name="ssn"
                                value={customer.ssn}
                            />
                            <CustomerProperty
                                title="Sími"
                                name="telephone"
                                value={customer.telephone}
                            />
                            <CustomerProperty
                                title="Netfang"
                                name="email"
                                value={customer.email}
                            />
                            <CustomerProperty
                                title="Heimilisfang"
                                name="address"
                                value={customer.address}
                            />
                            <CustomerProperty
                                title="Póstnúmer"
                                name="postalcode"
                                value={customer.postalCode}
                            />
                        </TableBody>
                    </Table>
                </TableContainer>
            ) : (
                <p className="error">
                    Villa kom upp: Gat ekki sótt viðskiptavin
                </p>
            )}
        </div>
    );
};

CustomerDetails.propTypes = {
    id: idType,
};

export default CustomerDetails;
