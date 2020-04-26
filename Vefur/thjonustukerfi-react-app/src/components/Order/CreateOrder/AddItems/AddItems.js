import React from "react";
import "./AddItems.css";
import ItemForm from "../../../Item/ItemForm/ItemForm";
import {
    addItemsType,
    categoriesType,
    servicesType,
} from "../../../../types/index";

const AddItems = ({ addItems, categories, services }) => {
    const handleAdd = (newItem, cb) => {
        newItem.serviceName =
            services[
                services.findIndex((s) => s.id.toString() === newItem.service)
            ].name;
        newItem.categoryName =
            categories[
                categories.findIndex(
                    (c) => c.id.toString() === newItem.category
                )
            ].name;

        addItems(newItem, cb);
    };

    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <ItemForm
                categories={categories}
                services={services}
                submitHandler={handleAdd}
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
