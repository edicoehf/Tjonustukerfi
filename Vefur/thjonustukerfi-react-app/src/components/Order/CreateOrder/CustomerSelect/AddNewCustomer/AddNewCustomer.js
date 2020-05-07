import React from "react";
import CustomerInputForm from "../../../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../../../hooks/useCreateCustomer";
import { handleCreateType, isProcessingType } from "../../../../../types/index";
import useGetCustomerCallback from "../../../../../hooks/useGetCustomerCallback";

const AddNewCustomer = ({ addCustomer }) => {
    const [success, setSuccess] = React.useState(false);
    const handleSuccess = () => {
        setSuccess(true);
    };

    const { error, handleCreate, isProcessing, customerId } = useCreateCustomer(
        handleSuccess
    );

    const { fetchCustomer } = useGetCustomerCallback(customerId, addCustomer);

    if (success) {
        setSuccess(false);
        fetchCustomer();
    }

    return (
        <div className="add-new-customer">
            <h2>Nýr viðskiptavinur</h2>
            <CustomerInputForm
                isProcessing={isProcessing}
                submitHandler={handleCreate}
                compact={true}
            />
            {error && <p className="error">Gat ekki bætt við viðskiptavin</p>}
        </div>
    );
};

AddNewCustomer.propTypes = {
    handleCreate: handleCreateType,
    isProcessing: isProcessingType,
};

export default AddNewCustomer;
