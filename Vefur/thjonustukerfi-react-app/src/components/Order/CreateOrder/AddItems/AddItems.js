import React from "react";
import "./AddItems.css";
import ItemForm from "../../../Item/ItemForm/ItemForm";
import {
    addItemsType,
    categoriesType,
    servicesType,
} from "../../../../types/index";

const AddItems = ({ addItems, categories, services }) => {
    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <ItemForm
                categories={categories}
                services={services}
                submitHandler={addItems}
            />
        </div>
    );
};

AddItems.propTypes = {
    addItems: addItemsType,
    categories: categoriesType,
    services: servicesType,
};

export default AddItems;
