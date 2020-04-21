import React from "react";
import CustomerInputForm from "../../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../../hooks/useCreateCustomer";
import { handleCreateType, isProcessingType } from "../../../../types/index";

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

AddNewCustomer.propTypes = {
    handleCreate: handleCreateType,
    isProcessing: isProcessingType,
};

export default AddNewCustomer;
