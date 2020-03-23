import React from "react";
import useDeleteCustomerById from "../../../hooks/useDeleteCustomerById";

const DeleteCustomer = id => {
    const { error, handleDelete, isDeleting } = useDeleteCustomerById(id);
    return (
        <div className="delete-customer">
            <button
                type="button"
                className="deletebtn"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                Eyða viðskiptavin
            </button>
            {!error ? (
                <p className="delete-error">Gat ekki eytt viðskiptavin</p>
            ) : (
                <></>
            )}
        </div>
    );
};

export default DeleteCustomer;
