import React from "react";
import useGetOrderById from "../../../hooks/useGetOrderById";

// Temp comp for testing
const OrderDetails = ({ match }) => {
    const id = match.params.id;
    const { order, error } = useGetOrderById(id);
    console.log(order);
    return <div></div>;
};

export default OrderDetails;
