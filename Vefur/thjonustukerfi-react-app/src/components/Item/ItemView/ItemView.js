import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import StateSelection from "../StateSelection/StateSelection";
import ItemStates from "../ItemStates/ItemStates";
import ItemActions from "../Actions/ItemActions/ItemActions";
import "./ItemView.css";
import PrintItemView from "../PrintItemView/PrintItemView";

const ItemView = ({ match }) => {
    const id = match.params.id;
    const [detailsUpdate, setDetailsUpdate] = React.useState(false);
    const [statesUpdate, setStatesUpdate] = React.useState(false);
    const [isPrintingReady, setPrintingReady] = React.useState(false);
    const ticketWidth = "15cm";
    const ticketHeight = "10cm";

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
    const itemLoaded = () => {
        setPrintingReady(true);
    };

    return (
        <div className="item-view">
            <h1>Upplýsingar um vöru</h1>
            <ItemDetails
                id={id}
                updated={detailsUpdate}
                receivedUpdate={detailsReceivedUpdate}
                itemLoaded={itemLoaded}
            />
            <StateSelection id={id} hasUpdated={hasUpdated} />
            <ItemStates
                id={id}
                updated={statesUpdate}
                receivedUpdate={statesReceivedUpdate}
            />
            <div className="button-area">
                <ItemActions id={id} />{" "}
                {isPrintingReady ? (
                    <PrintItemView
                        id={id}
                        width={ticketWidth}
                        height={ticketHeight}
                    />
                ) : (
                    <>Hallo</>
                )}
            </div>
        </div>
    );
};

export default ItemView;
