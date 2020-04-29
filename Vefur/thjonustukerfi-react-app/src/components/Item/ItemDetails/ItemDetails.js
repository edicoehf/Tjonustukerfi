import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import { Link } from "react-router-dom";
import "./ItemDetails.css";

const ItemDetails = ({ id, updated, receivedUpdate }) => {
    const { item, error, fetchItem, isLoading } = useGetItemById(id);
    const { category, service, orderId, state, json, barcode, details } = item;

    if (updated) {
        receivedUpdate();
        fetchItem();
    }
    // ÞETTA ÆTTI AÐ VERA TAFLA?
    return (
        <>
            {" "}
            {!isLoading ? (
                <div className="item-details">
                    {!error ? (
                        <>
                            <div className="item-title">Vara: {id}</div>
                            <div className="item-category">
                                Tegund:{" "}
                                {json.otherCategory
                                    ? json.otherCategory
                                    : category}
                            </div>
                            <div className="item-service">
                                Þjónusta:{" "}
                                {json.otherService
                                    ? json.otherService
                                    : service}
                            </div>
                            <div className="item-currentstate">
                                Staða: {state}
                            </div>
                            <div className="item-fillet">
                                Flökun: {json.filleted ? "Flakað" : "Óflakað"}
                            </div>
                            <div className="item-sliced">
                                Pökkun: {json.sliced ? "Bitar" : "Heilt Flak"}
                            </div>
                            <div className="item-barcode">
                                Strikamerki: {barcode}
                            </div>
                            <div className="item-details">Annað: {details}</div>
                            <div className="item-order">
                                <Link to={`/order/${orderId}`}>
                                    Pöntun: {orderId}
                                </Link>
                            </div>
                        </>
                    ) : (
                        <p className="error">
                            Gat ekki sótt upplýsingar um vöru
                        </p>
                    )}
                </div>
            ) : (
                <div>Sæki upplýsingar</div>
            )}
        </>
    );
};

export default ItemDetails;
