import React from "react";
import Form from "../../Form/Form";
import Input from "../../Input/Input";
import validateForm from "../CustomerValidate/CustomerValidate";
import useForm from "../../../hooks/useForm";
import "./CustomerInputForm.css";

const initialState = {
    name: "",
    ssn: "",
    telephone: "",
    email: "",
    postalCode: "",
    address: "",
};

const CustomerInputForm = ({ existingCustomer, submitHandler, processing }) => {
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
                    name="telephone"
                    value={values.telephone}
                    htmlId="telephone"
                    label="Símanúmer"
                    errorMessage={errors.telephone}
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
                    disabled={processing}
                    type="submit"
                    value={
                        isExistingCustomer
                            ? "Uppfæra viðskiptavin"
                            : "Skrá nýjan viðskiptavin"
                    }
                    className="btn btn-dark"
                />
            </Form>
        </div>
    );
};

export default CustomerInputForm;
