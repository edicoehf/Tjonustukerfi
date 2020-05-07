import React from "react";
import "./UpdateCustomer.css";
import CustomerInputForm from "../CustomerInputForm/CustomerInputForm";
import useUpdateCustomer from "../../../hooks/useUpdateCustomer";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
import { useHistory } from "react-router-dom";

const UpdateCustomer = ({ match }) => {
    const id = match.params.id;

    const [success, setSuccess] = React.useState(false);

    const handleSuccess = () => {
        setSuccess(true);
    };

    const history = useHistory();
    if (success) {
        setSuccess(false);
        history.push(`/customer/${id}`);
    }

    const { customer, error } = useGetCustomerById(id);
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
