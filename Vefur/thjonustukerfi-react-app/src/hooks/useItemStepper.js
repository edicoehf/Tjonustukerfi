import React from "react";

/**
 * Hook that handles using the item stepper
 *
 * @param values - Selected values
 * @param services - List of services
 * @param categories - List of categories
 * @returns activeStep, isForwardError, handleStepChange, handleStepReset, handleBack
 *
 * @category Input
 * @subcategory Hooks
 */
const useItemStepper = (values, services, categories) => {
    // The active step
    const [activeStep, setActiveStep] = React.useState(0);
    // Was there an error moving forward in the stepper
    const [isForwardError, setShowForwardError] = React.useState(false);

    // Handle moving between steps and validating
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

    // Move back to first step
    const handleStepReset = () => {
        setActiveStep(0);
    };

    // Move one step back
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
