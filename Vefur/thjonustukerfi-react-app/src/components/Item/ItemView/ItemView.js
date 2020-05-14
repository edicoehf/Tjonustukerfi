import React from "react";
import ItemDetails from "../ItemDetails/ItemDetails";
import StateSelection from "../StateSelection/StateSelection";
import ItemStates from "../ItemStates/ItemStates";
import ItemActions from "../Actions/ItemActions/ItemActions";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import PrintItemView from "../PrintItemView/PrintItemView";
import "./ItemView.css";

/**
 * Page which displays all information on an item and the available actions for it
 *
 * @component
 * @category Item
 */
const ItemView = ({ match }) => {
    // Get the id from url
    const id = match.params.id;
    // Details needs to be updated
    const [detailsUpdate, setDetailsUpdate] = React.useState(false);
    // States needs to be updated
    const [statesUpdate, setStatesUpdate] = React.useState(false);
    // Is details loading
    const [detailsLoading, setDetailsLoading] = React.useState(false);
    // Is the next state selection loading
    const [nextStatesLoading, setNextStatesLoading] = React.useState(false);
    // Is the state timeline loading
    const [prevStatesLoading, setPrevStatesLoading] = React.useState(false);
    // Is something loading
    const [isLoading, setIsLoading] = React.useState(true);
    // Size of printable ticket
    const ticketWidth = "15cm";
    const ticketHeight = "10cm";

    // Set that the item has updated, details and state need to update
    const hasUpdated = () => {
        setDetailsUpdate(true);
        setStatesUpdate(true);
    };

    // Details has updated
    const detailsReceivedUpdate = () => {
        setDetailsUpdate(false);
    };

    // States has updated
    const statesReceivedUpdate = () => {
        setStatesUpdate(false);
    };

    // Check if anything is loading, keep track of this so only one spinner can be used instead of multiple spinners
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
            />
            <ItemStates
                id={id}
                updated={statesUpdate}
                receivedUpdate={statesReceivedUpdate}
                componentLoading={setPrevStatesLoading}
            />
            <div className="button-area">
                <ItemActions id={id} />{" "}
                <PrintItemView
                    id={id}
                    width={ticketWidth}
                    height={ticketHeight}
                />
            </div>
        </div>
    );
};

export default ItemView;
