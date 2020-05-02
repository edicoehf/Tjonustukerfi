import React from "react";
import { Step, Stepper, StepLabel, Typography } from "@material-ui/core/";
import useGetItemHistoryById from "../../../hooks/useGetItemHistory";
import moment from "moment";
import "moment/locale/is";

const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("l");
};

const ItemStates = ({ id, updated, receivedUpdate }) => {
    const { itemHistory, error, fetchItemHistory } = useGetItemHistoryById(id);
    console.log(updated);
    if (updated) {
        console.log("UP");
        fetchItemHistory();
        receivedUpdate();
    }

    return (
        <div className="item-states">
            {!error ? (
                <>
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
                </>
            ) : (
                <p className="error">Gat ekki sótt fyrrum stöður vöru</p>
            )}
        </div>
    );
};

export default ItemStates;
