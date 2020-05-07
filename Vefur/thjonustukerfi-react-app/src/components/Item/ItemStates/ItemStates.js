import React from "react";
import { Step, Stepper, StepLabel, Typography } from "@material-ui/core/";
import useGetItemHistoryById from "../../../hooks/useGetItemHistory";
import moment from "moment";
import "moment/locale/is";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("l");
};

const ItemStates = ({ id, updated, receivedUpdate, componentLoading }) => {
    const {
        itemHistory,
        error,
        fetchItemHistory,
        isLoading,
    } = useGetItemHistoryById(id);

    if (updated) {
        fetchItemHistory();
        receivedUpdate();
    }

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

export default ItemStates;
