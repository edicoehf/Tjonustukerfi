import React from "react";
import "./UpdateCustomer.css";
import CustomerInputForm from "../CustomerInputForm/CustomerInputForm";
import useUpdateCustomer from "../../../hooks/useUpdateCustomer";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import { useHistory } from "react-router-dom";

/**
 * A page which is used to update customers information
 *
 * @component
 * @category Customer
 */

const UpdateCustomer = ({ match }) => {
    // Get the Customer ID from url
    const id = match.params.id;

    // Was customer successfully updated
    const [success, setSuccess] = React.useState(false);
    // CB that sets customer update successful
    const handleSuccess = () => {
        setSuccess(true);
    };

    // Get history
    const history = useHistory();
    // Open details page of customer when successfully updated
    React.useEffect(() => {
        if (success) {
            setSuccess(false);
            history.push(`/customer/${id}`);
        }
    }, [success, history, id]);

    // Fetch customer info
    const { customer, error } = useGetCustomerById(id);
    // Use update customer hook, send handleSuccess as CB to be called on success
    const { updateError, handleUpdate, isProcessing } = useUpdateCustomer(
        handleSuccess
    );

    return (
        <div className="body">
            <div className="header">
                <h1>Breyta viðskiptavin</h1>
            </div>
            {!error && Object.keys(customer).length > 0 ? (
                <>
                    <div className="body">
                        <CustomerInputForm
                            isProcessing={isProcessing}
                            existingCustomer={customer}
                            submitHandler={handleUpdate}
                        />
                    </div>
                    {updateError && (
                        <div className="error">
                            Gat ekki breytt viðskiptavin
                        </div>
                    )}
                </>
            ) : (
                <div className="error">Viðskiptavinur fannst ekki</div>
            )}
        </div>
    );
};

export default UpdateCustomer;
