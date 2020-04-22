import React from "react";
import { Step, Stepper, StepLabel } from "@material-ui/core/";

const ItemStates = ({ id }) => {
    // Hook sem sækir þær stöður sem varan hefur verið í
    // {states, error} = useGetItemStatesById(id);

    return (
        <div className="item-states">
            {!error ? (
                <>
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
                </>
            ) : (
                <p className="error">Gat ekki sótt stöður vöru</p>
            )}
        </div>
    );
};

export default ItemStates;
