import React from "react";
import "./AddItems.css";
import ItemForm from "../../../Item/ItemForm/ItemForm";
import {
    addItemsType,
    categoriesType,
    servicesType,
} from "../../../../types/index";

/**
 * Side panel for adding items to an order in createorder page
 *
 * @component
 * @category Order
 */
const AddItems = ({ addItems, categories, services }) => {
    // Adds item to an order
    const handleAdd = (newItem, cb) => {
        // Get the name of the service so it can be displayed in the page
        newItem.serviceName =
            services[
                services.findIndex((s) => s.id.toString() === newItem.service)
            ].name;
        // Get the name of the category so it can be displayed in the page
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
    /** CB that adds an item to the order */
    addItems: addItemsType,
    /** List of categories */
    categories: categoriesType,
    /** List of services */
    services: servicesType,
};

export default AddItems;
