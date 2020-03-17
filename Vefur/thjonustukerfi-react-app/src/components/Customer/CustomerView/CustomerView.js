import React from 'react';
import CustomerDetails from '../CustomerDetails/CustomerDetails';
import './CustomerView.css'

const CustomerView = ({ match }) => {
  const id = match.params.id;

  return (
    <div className="customer-view">
      <h1>Upplýsingar um viðskiptavin</h1>
      <CustomerDetails id={id} />
    </div>
  )
};

export default CustomerView;