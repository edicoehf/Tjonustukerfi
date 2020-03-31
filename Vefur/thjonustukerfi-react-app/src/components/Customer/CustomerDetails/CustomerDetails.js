import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import PropTypes from "prop-types";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import CustomerProperty from "../CustomerProperty/CustomerProperty";
import "./CustomerDetails.css";
import { CustomerContext } from "../../../context/customerContext";

const CustomerDetails = ({ id }) => {
    const { setCustomer } = useContext(CustomerContext);
    const { customer, error } = useGetCustomerById(id);

    return (
        <div className="customer-details">
            {!error ? (
                <table className="customer-properties">
                    <tbody>
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
                        <Link
                            to="/new-customer"
                            onClick={() => setCustomer(customer)}
                        >
                            <Button variant="warning">Edit</Button>
                        </Link>
                    </tbody>
                </table>
            ) : (
                <p className="error">
                    Villa kom upp: Gat ekki sótt viðskiptavin
                </p>
            )}
        </div>
    );
};

CustomerDetails.propTypes = {
    id: PropTypes.string.isRequired
};

export default CustomerDetails;
