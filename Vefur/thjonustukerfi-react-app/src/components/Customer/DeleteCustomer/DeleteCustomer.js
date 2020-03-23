import React from "react";
import useDeleteCustomerById from "../../../hooks/useDeleteCustomerById";

const DeleteCustomer = id => {
    const { error, handleDelete } = useDeleteCustomerById(id);
    return (
        <div className="delete-customer" onClick={handleDelete}>
            <button type="button" className="deletebtn">
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
