import React from "react";
const useItemStepper = (values, services, categories) => {
    const [activeStep, setActiveStep] = React.useState(0);
    const [isForwardError, setShowForwardError] = React.useState(false);

    const handleStepChange = () => {
        if (activeStep === 0 && values.category) {
            if (
                values.category === categories.length.toString() &&
                values.otherCategory === ""
            ) {
                setShowForwardError(true);
            } else {
                setActiveStep(1);
                setShowForwardError(false);
            }
        } else if (activeStep === 1 && values.service) {
            if (
                values.service === services.length.toString() &&
                values.otherService === ""
            ) {
                setShowForwardError(true);
            } else {
                setActiveStep(2);
                setShowForwardError(false);
            }
        } else {
            setShowForwardError(true);
        }
    };

    const handleStepReset = () => {
        setActiveStep(0);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };
    return {
        activeStep,
        isForwardError,
        handleStepChange,
        handleStepReset,
        handleBack,
    };
};

export default useItemStepper;
