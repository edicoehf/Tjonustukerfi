import React from 'react';
import PropTypes from "prop-types";
import useCustomerService from '../../../hooks/useCustomerService';
import CustomerProperty from '../CustomerProperty/CustomerProperty';
import './CustomerDetails.css';

const CustomerDetails = ({ id }) => {
  const { customer, error } = useCustomerService(id);
  console.log(customer);
  return (
    <div className='customer-details'>
      <h1>Upplýsingar um viðskiptavin</h1>
      {!error ? ( 
        <table className="customer-properties">
          <CustomerProperty title='Nafn' value={customer.name} />
          <CustomerProperty title='Kennitala' value={customer.ssn} />
          <CustomerProperty title='Sími' value={customer.telephone} />
          <CustomerProperty title='Netfang' value={customer.email} />
          <CustomerProperty title='Póstnúmer' value={customer.postalCode} />
          <CustomerProperty title='Heimilisfang' value={customer.address} />
        </table>
      ) : (
        <p className="error">{error}</p>
      )}
    </div>
  )
};

CustomerDetails.propTypes = {
  id: PropTypes.string.isRequired
}

export default CustomerDetails;