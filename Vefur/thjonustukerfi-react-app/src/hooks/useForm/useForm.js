import React from 'react';

const useForm = (initialState, validate, submitHandler) => {
    const [values, setValues] = React.useState(initialState);
	const [errors, setErrors] = React.useState({});
	const [isSubmitting, setSubmitting] = React.useState(false);

	React.useEffect(() => {
		if (isSubmitting) {
			const noErrors = Object.keys(errors).length === 0;
			if (noErrors) {
				submitHandler(values);
				setSubmitting(false);
			} else {
				setSubmitting(false);
			}
		}
	}, [errors]);

	const handleChangeText = e => {
		setValues({
			...values,
			[e.target.name]: e.target.value
		});
	};

	const handleSubmit = e => {
		e.preventDefault();
		const validationErrors = validate(values);
		setSubmitting(true);
		setErrors(validationErrors);
	};

	const resetFields = () => {
		setValues(initialState);
		setErrors({});
	};

	return { handleSubmit, handleChangeText, values, errors, isSubmitting, resetFields };
};

export default useForm;