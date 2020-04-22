import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import StateSelection from "../StateSelection/StateSelection";
import ItemActions from "../Actions/ItemActions/ItemActions";
import "./ItemView.css";

const ItemView = ({ match }) => {
    const id = match.params.id;
    const [update, setUpdate] = React.useState(false);

    const hasUpdated = () => {
        setUpdate(true);
    };

    const receivedUpdate = () => {
        setUpdate(false);
    };

    return (
        <div className="item-view">
            <h1>Upplýsingar um vöru</h1>
            <ItemDetails
                id={id}
                updated={update}
                receivedUpdate={receivedUpdate}
            />
            <StateSelection id={id} hasUpdated={hasUpdated} />
            <ItemActions id={id} />
        </div>
    );
};

export default ItemView;
