import React from "react";
import AddItems from "../AddItems/AddItems";

const CreateOrderView = () => {
    const addItems = (items, cb) => {
        console.log(items);
        cb();
    };

    return (
        <div className="create-order-view">
            <AddItems addItems={addItems} />
        </div>
    );
};

export default CreateOrderView;
