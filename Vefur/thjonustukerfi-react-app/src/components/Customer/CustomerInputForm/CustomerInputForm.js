import React, { useContext } from "react";
import Form from "../../Form/Form";
import Input from "../../Input/Input";
import validateForm from "../CustomerValidate/CustomerValidate";
import useForm from "../../../hooks/useForm";
import customerService from "../../../services/customerService";
import "./CustomerInputForm.css";
import { CustomerContext } from "../../../context/customerContext";

const initialState = {
    name: "",
    ssn: "",
    telephone: "",
    email: "",
    postalCode: "",
    address: ""
};

const CustomerInputForm = () => {
    const { customer } = useContext(CustomerContext);
    const state = customer ? customer : initialState;
    const [submitError, setSubmitError] = React.useState(null);

    const submitHandler = async values => {
        if (Object.keys(customer).length > 0) {
            console.log("INSIDE UPDATE");
            customerService
                .updateCustomer(values)
                .catch(error => setSubmitError(error));
        } else {
            console.log("INSIDE POST");
            customerService
                .createCustomer(values)
                .catch(error => setSubmitError(error));
        }
    };

    // isSubmitting, resetFields
    const { handleSubmit, handleChangeText, values, errors } = useForm(
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
