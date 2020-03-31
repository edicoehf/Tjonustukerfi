import React from "react";
import "./UpdateCustomer.css";
import CustomerInputForm from "../CustomerInputForm/CustomerInputForm";
import useUpdateCustomer from "../../../hooks/useUpdateCustomer";
import useGetCustomerById from "../../../hooks/useGetCustomerById";

const UpdateCustomer = ({ match }) => {
    const id = match.params.id;
    const { customer, error } = useGetCustomerById(id);
    const { updateError, handleUpdate, isProcessing } = useUpdateCustomer();

    return (
        <div className="body">
            <div className="header">
                <h1>Breyta viðskiptavin</h1>
            </div>
            {!error && Object.keys(customer).length > 0 ? (
                <>
                    <div className="body">
                        <CustomerInputForm
                            processing={isProcessing}
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
