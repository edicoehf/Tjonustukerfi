import React from "react";
import "./CreateCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../hooks/useCreateCustomer";
import { useHistory } from "react-router-dom";

const CreateCustomer = () => {
    const [success, setSuccess] = React.useState(false);

    const handleSuccess = () => {
        setSuccess(true);
    };

    const { error, handleCreate, isProcessing, customerId } = useCreateCustomer(
        handleSuccess
    );

    const history = useHistory();
    if (success) {
        setSuccess(false);
        history.push(`/customer/${customerId}`);
    }

    return (
        <div className="create-customer">
            <h1>Nýr viðskiptavinur</h1>
            <CustomerInputForm
                isProcessing={isProcessing}
                submitHandler={handleCreate}
            />

            {error && (
                <div className="error">Gat ekki bætt við viðskiptavin</div>
            )}
        </div>
    );
};

export default CreateCustomer;
