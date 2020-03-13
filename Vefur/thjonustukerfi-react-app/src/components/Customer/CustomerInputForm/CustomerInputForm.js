import React from "react";
import Form from "../../Form/Form";
import Input from "../../Input/Input";
import validateForm from "../CustomerValidate/CustomerValidate";
import useForm from "../../../hooks/useForm/useForm";
import createCustomer from "../../../services/customerService";

import "./CustomerInputForm.css";

const initialState = {
    name: "",
    ssn: "",
    telephone: "",
    email: "",
    postalCode: "",
    address: ""
};

const CustomerInputForm = () => {
    const submitHandler = async values => {
        createCustomer(values);
    };

    // isSubmitting, resetFields
    const { handleSubmit, handleChangeText, values, errors } = useForm(
        initialState,
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
                    onInput={handleChangeText}
                />
                <Input
                    type="text"
                    name="ssn"
                    value={values.ssn}
                    htmlId="ssn"
                    label="Kennitala"
                    errorMessage={errors.ssn}
                    onInput={handleChangeText}
                />
                <Input
                    type="text"
                    name="telephone"
                    value={values.telephone}
                    htmlId="telephone"
                    label="Símanúmer"
                    errorMessage={errors.telephone}
                    onInput={handleChangeText}
                />
                <Input
                    type="text"
                    name="email"
                    value={values.email}
                    htmlId="email"
                    label="Netfang"
                    errorMessage={errors.email}
                    onInput={handleChangeText}
                />
                <Input
                    type="text"
                    name="address"
                    value={values.address}
                    htmlId="address"
                    label="Heimilisfang"
                    errorMessage={errors.address}
                    onInput={handleChangeText}
                />
                <Input
                    type="text"
                    name="postalCode"
                    value={values.postalCode}
                    htmlId="postalCode"
                    label="Póstnúmer"
                    errorMessage={errors.postalCode}
                    onInput={handleChangeText}
                />
                <input
                    type="submit"
                    value="Skrá nýjan viðskiptavin"
                    className="btn btn-dark"
                />
            </Form>
        </div>
    );
};

export default CustomerInputForm;
