import React from "react";
import CustomerListItem from "../CustomerListItem/CustomerListItem";
import { ListGroup } from "react-bootstrap";
import "./CustomerList.css";

const CustomerList = ({ customers, error, isLoading }) => {
    return (
        <div>
            {!error ? (
                isLoading ? (
                    <p> Sæki viðskiptavini </p>
                ) : (
                    <ListGroup className="list">
                        <ListGroup.Item className="item" variant="dark">
                            <h5>Nafn</h5>
                        </ListGroup.Item>
                        <ListGroup.Item
                            className="item actions-item"
                            variant="dark"
                        >
                            <h5 className="actions">Aðgerðir</h5>
                        </ListGroup.Item>
                        {customers.map(item => (
                            <CustomerListItem customer={item} key={item.id} />
                        ))}
                    </ListGroup>
                )
            ) : (
                <p className="error">
                    {" "}
                    Villa kom upp: Gat ekki sótt viðskiptavin
                </p>
            )}
        </div>
    );
};
export default CustomerList;