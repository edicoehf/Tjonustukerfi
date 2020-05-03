import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import StateSelection from "../StateSelection/StateSelection";
import ItemStates from "../ItemStates/ItemStates";
import ItemActions from "../Actions/ItemActions/ItemActions";
import "./ItemView.css";

const ItemView = ({ match }) => {
    const id = match.params.id;
    const [detailsUpdate, setDetailsUpdate] = React.useState(false);
    const [statesUpdate, setStatesUpdate] = React.useState(false);

    const hasUpdated = () => {
        setDetailsUpdate(true);
        setStatesUpdate(true);
    };

    const detailsReceivedUpdate = () => {
        setDetailsUpdate(false);
    };

    const statesReceivedUpdate = () => {
        setStatesUpdate(false);
    };

    return (
        <div className="item-view">
            <h1>Upplýsingar um vöru</h1>
            <ItemDetails
                id={id}
                updated={detailsUpdate}
                receivedUpdate={detailsReceivedUpdate}
            />
            <StateSelection id={id} hasUpdated={hasUpdated} />
            <ItemStates
                id={id}
                updated={statesUpdate}
                receivedUpdate={statesReceivedUpdate}
            />
            <ItemActions id={id} />
        </div>
    );
};

export default ItemView;
