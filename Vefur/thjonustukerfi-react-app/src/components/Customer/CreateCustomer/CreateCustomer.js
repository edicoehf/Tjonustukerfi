import React from "react";
import "./CreateCustomer.css";
import CustomerInputForm from "../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../hooks/useCreateCustomer";
import { useHistory } from "react-router-dom";

/**
 * A page for creating a new customer
 */

const CreateCustomer = () => {
    // Was the customer successfully created
    const [success, setSuccess] = React.useState(false);

    // CB to be triggered if customer was successfully created
    const handleSuccess = () => {
        setSuccess(true);
    };

    // Use Create Customer hook, handleSuccess is sent as CB to be called on success
    const { error, handleCreate, isProcessing, customerId } = useCreateCustomer(
        handleSuccess
    );

    // Get history
    const history = useHistory();

    React.useEffect(() => {
        if (success) {
            setSuccess(false);
            history.push(`/customer/${customerId}`);
        }
    }, [success, customerId, history]);

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
