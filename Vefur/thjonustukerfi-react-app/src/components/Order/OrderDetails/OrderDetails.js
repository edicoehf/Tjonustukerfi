import React from "react";
import useGetOrderById from "../../../hooks/useGetOrderById";
import useGetServices from "../../../hooks/useGetServices";

// Temp comp for testing
const OrderDetails = ({ match }) => {
    const id = match.params.id;
    const { order } = useGetOrderById(id);
    const { services } = useGetServices();
    console.log(order);
    console.log(services);
    return <div></div>;
};

export default OrderDetails;
