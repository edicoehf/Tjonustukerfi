import React from 'react';
import PropTypes from "prop-types";
import useCustomerService from '../../../hooks/useCustomerService';
import CustomerProperty from '../CustomerProperty/CustomerProperty';
import './CustomerDetails.css';

const CustomerDetails = ({ id }) => {
  const { customer, error } = useCustomerService(id);

  return (
    <div className='customer-details'>
      {!error ? ( 
        <table className="customer-properties">
          <CustomerProperty title='Nafn' value={customer.name} />
          <CustomerProperty title='Kennitala' value={customer.ssn} />
          <CustomerProperty title='Sími' value={customer.telephone} />
          <CustomerProperty title='Netfang' value={customer.email} />
          <CustomerProperty title='Heimilisfang' value={customer.address} />
          <CustomerProperty title='Póstnúmer' value={customer.postalCode} />
        </table>
      ) : (
        <p className="error">Villa kom upp: Gat ekki sótt viðskiptavin</p>
      )}
    </div>
  )
};

CustomerDetails.propTypes = {
  id: PropTypes.string.isRequired
}

export default CustomerDetails;