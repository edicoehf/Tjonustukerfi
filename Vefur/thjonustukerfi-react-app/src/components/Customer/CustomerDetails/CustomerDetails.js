import React from "react";
import CustomerProperty from "../CustomerProperty/CustomerProperty";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import { TableContainer, Table, Paper, TableBody } from "@material-ui/core";
import "./CustomerDetails.css";
import { idType } from "../../../types/index";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import CustomerPendingOrdersModal from "../CustomerPendingOrdersModal/CustomerPendingOrdersModal";

/**
 * A table containing all details of the customer with the given ID
 *
 * @component
 * @category Customer
 */

const CustomerDetails = ({ id }) => {
    // Fetch details about customer
    const { customer, error, isProcessing } = useGetCustomerById(id);
    // To display notification modal regarding ready orders
    const [readyOrdersModalOpen, setReadyOrdersModalOpen] = React.useState(
        true
    );
    // Set modal close
    const handleClose = () => {
        setReadyOrdersModalOpen(false);
    };

    return (
        <div className="customer-details">
            <ProgressComponent isLoading={isProcessing} />
            {!error ? (
                <>
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
                    {customer.hasReadyOrders && (
                        <CustomerPendingOrdersModal
                            customerName={customer.name}
                            open={readyOrdersModalOpen}
                            handleClose={handleClose}
                        />
                    )}
                </>
            ) : (
                <p className="error">Gat ekki sótt viðskiptavin</p>
            )}
        </div>
    );
};

CustomerDetails.propTypes = {
    /** Customer ID */
    id: idType,
};

export default CustomerDetails;
