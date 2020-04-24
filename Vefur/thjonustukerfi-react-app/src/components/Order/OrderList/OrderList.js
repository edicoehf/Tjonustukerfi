import React from "react";
import { List, ListItem } from "@material-ui/core";
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
                    <List className="order-list">
                        <ListItem className="order-item" variant="dark">
                            <h5>Pantananúmer</h5>
                        </ListItem>
                        <ListItem className="order-item" variant="dark">
                            <h5>Eigandi Pöntunar</h5>
                        </ListItem>
                        <ListItem className="order-item" variant="dark">
                            <h5 className="item">Fjöldi Vara</h5>
                        </ListItem>
                        <ListItem className="order-item order-action-item">
                            <h5>Aðgerðir</h5>
                        </ListItem>
                        {orders.map((item) => (
                            <OrderListItem order={item} key={item.id} />
                        ))}
                    </List>
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
