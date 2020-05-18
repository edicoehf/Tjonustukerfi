import React from "react";

/**
 * Hook that handles the use of forms
 *
 * @param initialState - The initial values for the form
 * @param validate - Validation function for validating input
 * @param submitHandler - Function that handles the submission of the form values
 * @returns handleSubmit, handleChange, resetFields, values, errors
 *
 * @category Input
 * @subcategory Hooks
 */
const useForm = (initialState, validate, submitHandler) => {
    // Values in the input fields
    const [values, setValues] = React.useState(initialState);
    // Errors that occurred in validation
    const [errors, setErrors] = React.useState({});
    // Should the form being submitted
    const [isSubmitting, setSubmitting] = React.useState(false);

    React.useEffect(() => {
        // Submit if the form should be submitted
        if (isSubmitting) {
            // Only if error free
            const noErrors = Object.keys(errors).length === 0;
            if (noErrors) {
                submitHandler(values);
                setSubmitting(false);
            } else {
                setSubmitting(false);
            }
        }
    }, [errors, values, submitHandler, isSubmitting]);

    // Exported function for updating values when input changes
    const handleChange = (e) => {
        setValues({
            ...values,
            [e.target.name]: e.target.value,
        });
    };

    // Exported function for submitting form
    // Checks for errors and submits if error free
    const handleSubmit = (e) => {
        e.preventDefault();
        const validationErrors = validate(values);
        setErrors(validationErrors);
        setSubmitting(true);
    };

    // Exported function for resetting the input fields
    const resetFields = () => {
        setValues(initialState);
    };

    return {
        handleSubmit,
        handleChange,
        resetFields,
        values,
        errors,
    };
};

export default useForm;
