import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import "./ItemView.css";

const ItemView = ({ match }) => {
    const id = match.params.id;

    return (
        <div className="item-view">
            <h1>Upplýsingar um vöru</h1>
            <ItemDetails id={id} />
        </div>
    );
};

export default ItemView;
