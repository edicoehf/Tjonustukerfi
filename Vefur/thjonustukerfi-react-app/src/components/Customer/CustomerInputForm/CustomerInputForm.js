import React from "react";
import Form from "../../Form/Form";
import validateForm from "../CustomerValidate/CustomerValidate";
import useForm from "../../../hooks/useForm";
import "./CustomerInputForm.css";
import {
    existingCustomerType,
    isProcessingType,
    submitHandlerType,
} from "../../../types/index";
import TextInput from "../../TextInput/TextInput";
import { Button, Paper } from "@material-ui/core";
import ProgressButton from "../../Feedback/ProgressButton/ProgressButton";

const initialState = {
    name: "",
    ssn: "",
    phone: "",
    email: "",
    postalCode: "",
    address: "",
    comment: ""
};

/**
 * Input form for creating and updating customers info
 *
 * @component
 * @category Customer
 */

const CustomerInputForm = ({
    existingCustomer,
    submitHandler,
    isProcessing,
    compact,
}) => {
    // Check if an existing customer was passed in props (and if it has loaded)
    const isExistingCustomer =
        existingCustomer && Object.keys(existingCustomer).length > 0;
    // Init state for Form hook with existing customer if provided, else blank
    const state = isExistingCustomer ? existingCustomer : initialState;

    // Use Form hook, send inital state from above, the customer validation function and the submithandler that was passed down
    const { handleSubmit, handleChange, values, errors } = useForm(
        state,
        validateForm,
        submitHandler
    );

    return (
        <div className={`customer-input-form ${compact ? "compact" : ""}`}>
            <Form onSubmit={handleSubmit}>
                <Paper elevation={3}>
                    <TextInput
                        name="name"
                        value={values.name}
                        htmlId="name"
                        className="name-input"
                        label="Nafn *"
                        errorMessage={errors.name}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="email"
                        value={values.email}
                        htmlId="email"
                        className="email-input"
                        label="Netfang *"
                        errorMessage={errors.email}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="ssn"
                        value={values.ssn}
                        htmlId="ssn"
                        className="ssn-input"
                        label="Kennitala"
                        errorMessage={errors.ssn}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="phone"
                        value={values.phone}
                        htmlId="phone"
                        className="phone-input"
                        label="Símanúmer"
                        errorMessage={errors.phone}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="address"
                        value={values.address}
                        htmlId="address"
                        className="address-input"
                        label="Heimilisfang"
                        errorMessage={errors.address}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="postalCode"
                        value={values.postalCode}
                        htmlId="postalCode"
                        className="postalcode-input"
                        label="Póstnúmer"
                        errorMessage={errors.postalCode}
                        onInput={handleChange}
                    />
                    <TextInput
                        name="comment"
                        value={values.comment}
                        htmlId="comment"
                        className="comment-input"
                        label="Athugasemdir"
                        errorMessage={errors.comment}
                        onInput={handleChange}
                    />

                </Paper>
                <ProgressButton isLoading={isProcessing}>
                    <Button
                        className="input-submit"
                        variant="contained"
                        color="primary"
                        disabled={isProcessing}
                        type="submit"
                    >
                        {isExistingCustomer
                            ? "Uppfæra viðskiptavin"
                            : "Skrá nýjan viðskiptavin"}
                    </Button>
                </ProgressButton>
            </Form>
        </div>
    );
};

CustomerInputForm.propTypes = {
    /** Customer information to prefill if updating, skip for new customer */
    existingCustomer: existingCustomerType,
    /** Status of submission, used to disable submit button while processing request */
    isProcessing: isProcessingType,
    /** The callback function called on submission */
    submitHandler: submitHandlerType,
};

export default CustomerInputForm;
