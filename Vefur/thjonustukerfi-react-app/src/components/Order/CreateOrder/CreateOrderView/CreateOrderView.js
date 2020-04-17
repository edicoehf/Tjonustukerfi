import React from "react";
import AddItems from "../AddItems/AddItems";
import ViewItems from "../ViewItems/ViewItems";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";

const CreateOrderView = () => {
    const [items, setItems] = React.useState([]);

    const { services } = useGetServices();
    const { categories } = useGetCategories();

    const addItems = (newItem, cb) => {
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
        const ids = items.map((item) => item.id);
        newItem.id = ids.reduce((acc, curr) => Math.max(acc, curr), 0) + 1;
        setItems([...items, newItem]);
        cb();
    };

    const removeItem = (itemToRemove) => {
        setItems(items.filter((item) => itemToRemove.id !== item.id));
    };

    return (
        <div className="create-order-view">
            <AddItems
                addItems={addItems}
                categories={categories}
                services={services}
            />
            <ViewItems items={items} remove={removeItem} />
        </div>
    );
};

export default CreateOrderView;
