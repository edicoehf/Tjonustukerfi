import React from "react";
import CustomerInputForm from "../../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../../hooks/useCreateCustomer";

const AddNewCustomer = () => {
    const { error, handleCreate, isProcessing } = useCreateCustomer();

    return (
        <div className="add-new-customer">
            <h2>Nýr viðskiptavinur</h2>
            <CustomerInputForm
                processing={isProcessing}
                submitHandler={handleCreate}
            />
            {error && <div>Gat ekki bætt við viðskiptavin</div>}
        </div>
    );
};

export default AddNewCustomer;
