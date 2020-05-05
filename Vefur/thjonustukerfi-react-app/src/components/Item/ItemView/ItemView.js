import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import StateSelection from "../StateSelection/StateSelection";
import ItemStates from "../ItemStates/ItemStates";
import ItemActions from "../Actions/ItemActions/ItemActions";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import PrintItemView from "../PrintItemView/PrintItemView";
import "./ItemView.css";

const ItemView = ({ match }) => {
    const id = match.params.id;
    const [detailsUpdate, setDetailsUpdate] = React.useState(false);
    const [statesUpdate, setStatesUpdate] = React.useState(false);
    const [detailsLoading, setDetailsLoading] = React.useState(false);
    const [nextStatesLoading, setNextStatesLoading] = React.useState(false);
    const [prevStatesLoading, setPrevStatesLoading] = React.useState(false);
    const [isLoading, setIsLoading] = React.useState(true);
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

    React.useEffect(() => {
        if (!detailsLoading && !nextStatesLoading && !prevStatesLoading) {
            setIsLoading(false);
        } else {
            setIsLoading(true);
        }
    }, [detailsLoading, nextStatesLoading, prevStatesLoading]);

    return (
        <div className="item-view">
            <h1>Upplýsingar um vöru</h1>
            <ProgressComponent isLoading={isLoading} />
            <ItemDetails
                id={id}
                updated={detailsUpdate}
                receivedUpdate={detailsReceivedUpdate}
                componentLoading={setDetailsLoading}
            />
            <StateSelection
                id={id}
                hasUpdated={hasUpdated}
                componentLoading={setNextStatesLoading}
                itemLoaded={itemLoaded}
            />
            <ItemStates
                id={id}
                updated={statesUpdate}
                receivedUpdate={statesReceivedUpdate}
                componentLoading={setPrevStatesLoading}
            />
            <div className="button-area">
                <ItemActions id={id} />{" "}
                {isPrintingReady && (
                    <PrintItemView
                        id={id}
                        width={ticketWidth}
                        height={ticketHeight}
                    />
                )}
            </div>
        </div>
    );
};

export default ItemView;
