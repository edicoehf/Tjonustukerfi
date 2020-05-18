import React from "react";
import CustomerInputForm from "../../../../Customer/CustomerInputForm/CustomerInputForm";
import useCreateCustomer from "../../../../../hooks/useCreateCustomer";
import { cbType } from "../../../../../types/index";
import useGetCustomerCallback from "../../../../../hooks/useGetCustomerCallback";
import "./AddNewCustomer.css";
/**
 * Smaller version of create customer that can be used in the customer selection modal in create order.
 * The created customer is automatically set as the selected customer in the createorder process.
 *
 * @component
 * @category Order
 */

const AddNewCustomer = ({ addCustomer }) => {
    // Was the creation successful
    const [success, setSuccess] = React.useState(false);
    // Set the creation as success
    const handleSuccess = () => {
        setSuccess(true);
    };

    // Use create customer hook, send handle success in as cb to be called upon success
    const { error, handleCreate, isProcessing, customerId } = useCreateCustomer(
        handleSuccess
    );

    // Get a function that fetches customer on demand, and adds it as selected customer on success
    const { fetchCustomer } = useGetCustomerCallback(customerId, addCustomer);

    // if customer was successfully created, fetch him and set as selected customer
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
    /** CB function that adds a customer as selected */
    addCustomer: cbType,
};

export default AddNewCustomer;
