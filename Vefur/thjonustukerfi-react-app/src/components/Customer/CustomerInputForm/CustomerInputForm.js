import React from "react";
import Form from "../../Form/Form";
import Input from "../../Input/Input";
import validateForm from "../CustomerValidate/CustomerValidate";
import useForm from "../../../hooks/useForm";
import "./CustomerInputForm.css";
import {
    existingCustomerType,
    isProcessingType,
    submitHandlerType,
} from "../../../types/index";

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
        <div className="body">
            <Form onSubmit={handleSubmit}>
                <Input
                    type="text"
                    name="name"
                    value={values.name}
                    htmlId="name"
                    label="Nafn"
                    errorMessage={errors.name}
                    onInput={handleChange}
                />
                <Input
                    type="text"
                    name="ssn"
                    value={values.ssn}
                    htmlId="ssn"
                    label="Kennitala"
                    errorMessage={errors.ssn}
                    onInput={handleChange}
                />
                <Input
                    type="text"
                    name="phone"
                    value={values.phone}
                    htmlId="phone"
                    label="Símanúmer"
                    errorMessage={errors.phone}
                    onInput={handleChange}
                />
                <Input
                    type="text"
                    name="email"
                    value={values.email}
                    htmlId="email"
                    label="Netfang"
                    errorMessage={errors.email}
                    onInput={handleChange}
                />
                <Input
                    type="text"
                    name="address"
                    value={values.address}
                    htmlId="address"
                    label="Heimilisfang"
                    errorMessage={errors.address}
                    onInput={handleChange}
                />
                <Input
                    type="text"
                    name="postalCode"
                    value={values.postalCode}
                    htmlId="postalCode"
                    label="Póstnúmer"
                    errorMessage={errors.postalCode}
                    onInput={handleChange}
                />
                <input
                    disabled={isProcessing}
                    type="submit"
                    value={
                        isExistingCustomer
                            ? "Uppfæra viðskiptavin"
                            : "Skrá nýjan viðskiptavin"
                    }
                    className="btn submit-btn"
                />
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
