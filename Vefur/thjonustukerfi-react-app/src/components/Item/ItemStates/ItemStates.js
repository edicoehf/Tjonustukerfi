import React from "react";
import { Step, Stepper, StepLabel, Typography } from "@material-ui/core/";
import useGetItemHistoryById from "../../../hooks/useGetItemHistory";
import moment from "moment";
import "moment/locale/is";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import { idType, cbType, updatedType } from "../../../types";

// Format date in (icelandic) human readable format
const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("l");
};

/**
 * Show the states that an item has been in as a step history.
 * Timeline of the item.
 *
 * @component
 * @category Item
 */

const ItemStates = ({ id, updated, receivedUpdate, componentLoading }) => {
    // Fetch the states that the item has been in
    const {
        itemHistory,
        error,
        fetchItemHistory,
        isLoading,
    } = useGetItemHistoryById(id);

    // Refetch item if it has been updated, inform parent roger
    React.useEffect(() => {
        if (updated) {
            fetchItemHistory();
            receivedUpdate();
        }
    }, [updated, fetchItemHistory, receivedUpdate]);

    // Tell parent component whether its loading or not, but only if parent provides such function
    React.useEffect(() => {
        if (componentLoading !== undefined) {
            componentLoading(isLoading);
        }
    }, [isLoading, componentLoading]);

    return (
        <div className="item-states">
            {isLoading ? (
                <ProgressComponent isLoading={componentLoading === undefined} />
            ) : (
                <>
                    {!error ? (
                        <Stepper
                            activeStep={itemHistory.length - 1}
                            alternativeLabel
                            elevation={3}
                        >
                            {itemHistory.map((state) => (
                                <Step key={state.stateId}>
                                    <StepLabel
                                        optional={
                                            <Typography
                                                variant="caption"
                                                color="textSecondary"
                                            >
                                                {dateFormat(state.timeOfChange)}
                                            </Typography>
                                        }
                                    >
                                        {state.state}
                                    </StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                    ) : (
                        <p className="error">
                            Gat ekki sótt fyrrum stöður vöru
                        </p>
                    )}
                </>
            )}
        </div>
    );
};

ItemStates.propTypes = {
    /** Item ID */
    id: idType,
    /** Has item been updated */
    updated: updatedType,
    /** CB to let parent know that child knows it Item has updated,
     * if this is provided then the component will not display a spinner while loading but instead leaves that responsibility with the parent.
     * Used for scenarios where parent displays multiple api dependant components */
    receivedUpdate: cbType,
    /** CB to let parent know if item is still being fetched
     * @param {bool} isLoading - Is component still loading
     */
    componentLoading: cbType,
};

export default ItemStates;
