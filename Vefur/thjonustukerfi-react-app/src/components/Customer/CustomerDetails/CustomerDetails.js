import React from "react";
import CustomerProperty from "../CustomerProperty/CustomerProperty";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import { TableContainer, Table, Paper, TableBody } from "@material-ui/core";
import "./CustomerDetails.css";
import { idType } from "../../../types/index";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

const CustomerDetails = ({ id }) => {
    const { customer, error, isProcessing } = useGetCustomerById(id);
    console.log(isProcessing);
    return (
        <div className="customer-details">
            <ProgressComponent isLoading={isProcessing} />
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
                                name="phone"
                                value={customer.phone}
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
                <p className="error">Gat ekki sótt viðskiptavin</p>
            )}
        </div>
    );
};

CustomerDetails.propTypes = {
    id: idType,
};

export default CustomerDetails;
