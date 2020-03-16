import React from 'react';
import CustomerDetails from '../CustomerDetails/CustomerDetails';

const CustomerView = ({ match }) => {
  const id = match.params.id;

  return (
    <CustomerDetails id={id} />
  )
};

export default CustomerView;