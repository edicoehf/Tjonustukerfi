import React from "react";
import useGetNextStatesById from "../../../hooks/useGetNextStatesById";
import { Step, Stepper, StepLabel } from "@material-ui/core/";

const ItemStates = ({ id }) => {
    const { states, error } = useGetNextStatesById(id);
    const nextStates = states.nextAvailableStates
        ? states.nextAvailableStates.sort((a, b) => a.id - b.id)
        : [];

    console.log(nextStates);
    return (
        <div className="item-states">
            <Stepper activeStep={0} alternativeLabel>
                <Step>
                    <StepLabel>
                        {states.currentState && states.currentState.name}
                    </StepLabel>
                </Step>
                {nextStates.map((state) => (
                    <Step key={state.id}>
                        <StepLabel>{state.name}</StepLabel>
                    </Step>
                ))}
            </Stepper>
        </div>
    );
};

export default ItemStates;
