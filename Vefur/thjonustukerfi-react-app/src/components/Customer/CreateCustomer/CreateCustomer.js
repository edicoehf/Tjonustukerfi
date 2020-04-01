import React from "react";
import "./CreateCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../hooks/useCreateCustomer";

const CreateCustomer = () => {
    const { error, handleCreate, isProcessing } = useCreateCustomer();

    return (
        <div className="body">
            <div className="header">
                <h1>Nýr viðskiptavinur</h1>
            </div>
            <div className="body">
                <CustomerInputForm
                    processing={isProcessing}
                    submitHandler={handleCreate}
                />
            </div>
            {error && <div>Gat ekki bætt við viðskiptavin</div>}
        </div>
    );
};

export default CreateCustomer;
