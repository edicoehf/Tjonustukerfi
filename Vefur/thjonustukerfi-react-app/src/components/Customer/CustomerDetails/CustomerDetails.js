import React from 'react';
import PropTypes from "prop-types";
import useCustomerService from '../../../hooks/useCustomerService';
import CustomerProperty from '../CustomerProperty/CustomerProperty';

const CustomerDetails = ({ id }) => {
  const { customer, error } = useCustomerService(id);

  return (
    <div className='customer-details'>
      {error ? ( 
        <>
        <CustomerProperty title='Nafn' value={customer.name} />
        <CustomerProperty title='Kennitala' value={customer.ssn} />
        <CustomerProperty title='Sími' value={customer.telephone} />
        <CustomerProperty title='Netfang' value={customer.email} />
        <CustomerProperty title='Póstnúmer' value={customer.postalCode} />
        <CustomerProperty title='Heimilisfang' value={customer.address} />
        </>
      ) : (
        <p className="error">{error}</p>
      )}
    </div>
  )
};

CustomerDetails.propTypes = {
  id: PropTypes.number.isRequired
}

export default CustomerDetails;