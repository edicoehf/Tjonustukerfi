import React from "react";
import useGetNextStatesById from "../../../hooks/useGetNextStatesById";
import { Step, Stepper, StepLabel } from "@material-ui/core/";

const ItemStates = ({ id }) => {
    const { states, error } = useGetNextStatesById(id);
    const nextStates = states.nextAvailableStates
        ? states.nextAvailableStates.sort((a, b) => a.id - b.id)
        : [];

    return (
        <div className="item-states">
            {!error ? (
                <>
                    {nextStates.length === 0 ? (
                        <Stepper activeStep={0} alternativeLabel>
                            <Step>
                                <StepLabel>
                                    {states.currentState &&
                                        states.currentState.name}
                                </StepLabel>
                            </Step>
                            {nextStates.map((state) => (
                                <Step key={state.id}>
                                    <StepLabel>{state.name}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                    ) : (
                        <p>Vara er í lokastöðu</p>
                    )}
                </>
            ) : (
                <p className="error">Gat ekki sótt næstu stöður vöru</p>
            )}
        </div>
    );
};

export default ItemStates;
