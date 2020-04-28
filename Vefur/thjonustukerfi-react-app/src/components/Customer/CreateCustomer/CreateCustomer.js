import React from "react";
import "./CreateCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../hooks/useCreateCustomer";

const CreateCustomer = () => {
    const { error, handleCreate, isProcessing } = useCreateCustomer();

    return (
        <div className="create-customer">
            <h1>Nýr viðskiptavinur</h1>
            <CustomerInputForm
                processing={isProcessing}
                submitHandler={handleCreate}
            />
            {error && <div>Gat ekki bætt við viðskiptavin</div>}
        </div>
    );
};

export default CreateCustomer;
