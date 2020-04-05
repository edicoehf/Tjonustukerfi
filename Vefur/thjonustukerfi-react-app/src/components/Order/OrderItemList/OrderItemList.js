import React from "react";
import OrderItem from "../OrderItem/OrderItem";
import PropTypes from "prop-types";
import { Table } from "react-bootstrap";

const OrderItemList = ({ items }) => {
    return (
        <Table className="order-item-list">
            <thead>
                <tr>
                    <th>Tegund</th>
                    <th>Þjónusta</th>
                    <th>Strikamerki</th>
                </tr>
            </thead>
            <tbody>
                {items.map((item) => (
                    <OrderItem
                        key={item.id}
                        type={item.type}
                        service={item.service}
                        barcode={item.barcode}
                    />
                ))}
            </tbody>
        </Table>
    );
};

OrderItemList.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            type: PropTypes.string.isRequired,
            service: PropTypes.string.isRequired,
            barcode: PropTypes.string.isRequired,
        })
    ),
};

export default OrderItemList;
