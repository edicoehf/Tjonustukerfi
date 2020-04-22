import React from "react";
import { ListGroup } from "react-bootstrap";
import OrderListItem from "../OrderListItem/OrderListItem";
import "./OrderList.css";
import { ordersType, isLoadingType } from "../../../types";

const OrderList = ({ orders, isLoading, error }) => {
    return (
        <div>
            {!error ? (
                isLoading ? (
                    <p> Sæki Pantanir </p>
                ) : (
                    <ListGroup className="order-list">
                        <ListGroup.Item className="item" variant="dark">
                            <h5>Pantananúmer</h5>
                        </ListGroup.Item>
                        <ListGroup.Item className="item" variant="dark">
                            <h5>Eigandi Pöntunar</h5>
                        </ListGroup.Item>
                        <ListGroup.Item
                            className="item actions-item"
                            variant="dark"
                        >
                            <h5 className="item">Fjöldi Vara</h5>
                        </ListGroup.Item>
                        {orders.map((item) => (
                            <OrderListItem order={item} key={item.id} />
                        ))}
                    </ListGroup>
                )
            ) : (
                <p className="error"> Villa kom upp: Gat ekki sótt pantanir</p>
            )}
        </div>
    );
};

OrderList.propTypes = {
    orders: ordersType,
    isLoading: isLoadingType,
};

export default OrderList;
