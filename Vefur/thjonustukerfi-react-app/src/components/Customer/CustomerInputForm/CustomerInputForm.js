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
import { Button } from "@material-ui/core";

const initialState = {
    name: "",
    ssn: "",
    phone: "",
    email: "",
    postalCode: "",
    address: "",
};

const CustomerInputForm = ({
    existingCustomer,
    submitHandler,
    isProcessing,
    compact,
}) => {
    const isExistingCustomer =
        existingCustomer && Object.keys(existingCustomer).length > 0;
    const state = isExistingCustomer ? existingCustomer : initialState;

    // isSubmitting, resetFields
    const { handleSubmit, handleChange, values, errors } = useForm(
        state,
        validateForm,
        submitHandler
    );

    return (
        <div className={`customer-input-form ${compact ? "compact" : ""}`}>
            <Form onSubmit={handleSubmit}>
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
                    name="email"
                    value={values.email}
                    htmlId="email"
                    className="email-input"
                    label="Netfang *"
                    errorMessage={errors.email}
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
                <Button
                    className="input-submit"
                    variant="contained"
                    color="primary"
                    size="large"
                    disabled={isProcessing}
                    type="submit"
                >
                    {isExistingCustomer
                        ? "Uppfæra viðskiptavin"
                        : "Skrá nýjan viðskiptavin"}
                </Button>
            </Form>
        </div>
    );
};

CustomerInputForm.propTypes = {
    existingCustomer: existingCustomerType,
    isProcessing: isProcessingType,
    submitHandler: submitHandlerType,
};

export default CustomerInputForm;
