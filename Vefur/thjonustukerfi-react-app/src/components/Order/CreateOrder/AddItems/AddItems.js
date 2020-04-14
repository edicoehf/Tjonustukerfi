import React from "react";
import useGetServices from "../../../../hooks/useGetServices";

const { services, serviceError: error } = useGetServices();

const AddItems = () => {
    return (
        <div className="add-items">
            <div></div>
        </div>
    );
};

export default AddItems;
