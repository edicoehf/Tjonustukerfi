import React from "react";
import CustomerProperty from "../CustomerProperty/CustomerProperty";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import useUpdateCustomerEmail from "../../../hooks/useUpdateCustomerEmail";
import { TableContainer, Table, Paper, TableBody } from "@material-ui/core";
import "./CustomerDetails.css";
import { idType } from "../../../types/index";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import CustomerPendingOrdersModal from "../CustomerPendingOrdersModal/CustomerPendingOrdersModal";
import SuccessToaster from "../../Feedback/SuccessToaster/SuccessToaster";

/**
 * A table containing all details of the customer with the given ID
 *
 * @component
 * @category Customer
 */

const CustomerDetails = ({ id }) => {

    const [emailUpdateSuccess, setEmailUpdateSuccess] = React.useState(false);

    // The creation has been marked successful, time for a new one (reset)
    const receivedEmailUpdateSuccess = () => {
        setEmailUpdateSuccess(false);
    };
    const handleEmailUpdateSuccess = () => {
        console.log("Email successfully updated");
        setEmailUpdateSuccess(true);
    };

    // Fetch details about customer
    const { customer, error, isProcessing, setCustomer } = useGetCustomerById(id);
    const { updateEmailError, handleUpdateEmail, isProcessingEmail } = useUpdateCustomerEmail(
        handleEmailUpdateSuccess
    );
    // To display notification modal regarding ready orders
    const [readyOrdersModalOpen, setReadyOrdersModalOpen] = React.useState(
        true
    );
    // Set modal close
    const handleClose = () => {
        setReadyOrdersModalOpen(false);
    };

    const onEnterEmail = (newEmail) => {
        customer.email = newEmail;
        const emailObj = {
            id: customer.id,
            email: newEmail
        };
        //console.log(emailObj);
        handleUpdateEmail(emailObj);
    }

    const onChangeEmail = (newEmail) => {
        const customerCopy = JSON.parse(JSON.stringify(customer));
        customerCopy.email = newEmail;
        setCustomer(customerCopy);
    }

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
                                    editable={false}
                                />
                                <CustomerProperty
                                    title="Kennitala"
                                    name="ssn"
                                    value={customer.ssn}
                                    editable={false}
                                />
                                <CustomerProperty
                                    title="Sími"
                                    name="phone"
                                    value={customer.phone}
                                    editable={false}
                                />
                                <CustomerProperty
                                    title="Netfang"
                                    name="email"
                                    value={customer.email}
                                    editable={true}
                                    enterHandler={onEnterEmail}
                                    editHandler={onChangeEmail}
                                />
                                <CustomerProperty
                                    title="Heimilisfang"
                                    name="address"
                                    value={customer.address}
                                    editable={false}
                                />
                                <CustomerProperty
                                    title="Póstnúmer"
                                    name="postalcode"
                                    value={customer.postalCode}
                                    editable={false}
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
            <SuccessToaster
                success={emailUpdateSuccess}
                receivedSuccess={receivedEmailUpdateSuccess}
                message="Tölvupóstur hefur verið uppfærður"
            />

        </div>
    );
};

CustomerDetails.propTypes = {
    /** Customer ID */
    id: idType,
};

export default CustomerDetails;
