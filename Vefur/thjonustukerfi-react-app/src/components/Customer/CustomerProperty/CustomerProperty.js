import React from 'react';
import PropTypes from "prop-types";
import './CustomerProperty.css';

const CustomerProperty = ({ title, name, value }) => {
  return (
    <>
      {value ? (
        <tr className="customer-property" title={name}> 
          <td className="customer-property-title">{title}:</td>
          <td className="customer-property-value">{value}</td>
        </tr>  
      ) : (
        <></>
      )}
    </>
  )
}

CustomerProperty.propTypes = {
  title: PropTypes.string.isRequired,
  value: PropTypes.string
}

export default CustomerProperty;