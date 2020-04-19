import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import { Link } from "react-router-dom";
import "./ItemDetails.css";

const ItemDetails = ({ id }) => {
    const { item, error } = useGetItemById(id);

    return (
        <div className="item-details">
            {!error ? (
                <>
                    <div className="item-title">Vara: {item.id}</div>
                    <div className="item-category">Tegund: {item.category}</div>
                    <div className="item-service">Þjónusta: {item.service}</div>
                    <div className="item-currentstate">Staða: {item.state}</div>
                    <div className="item-order">
                        <Link to={`/customer/${item.orderId}`}>
                            Pöntun: {item.orderId}
                        </Link>
                    </div>
                </>
            ) : (
                <p className="error">Gat ekki sótt upplýsingar um vöru</p>
            )}
        </div>
    );
};

export default ItemDetails;
