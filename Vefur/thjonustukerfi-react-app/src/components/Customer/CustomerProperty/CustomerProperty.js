import React from 'react';
import PropTypes from "prop-types";

const CustomerProperty = ({ title, value }) => {
  return (
    <>
      {value ? (
        <p className="customer-property">{title}: {value}</p>  
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