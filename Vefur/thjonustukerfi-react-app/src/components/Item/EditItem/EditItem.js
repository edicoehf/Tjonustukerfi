import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import ItemForm from "../ItemForm/ItemForm";
import useGetServices from "../../../hooks/useGetServices";
import useGetCategories from "../../../hooks/useGetCategories";
import useUpdateItem from "../../../hooks/useUpdateItem";
import "./EditItem.css";

const EditItem = ({ match, history }) => {
    const id = match.params.id;
    const { services } = useGetServices();
    const { categories } = useGetCategories();
    const { item, error } = useGetItemById(id);
    const redirect = () => {
        history.push(`/item/${id}`);
    };
    const { updateError, handleUpdate, isProcessing } = useUpdateItem(redirect);

    const editItem = (item) => {
        if (!isProcessing) {
            handleUpdate({
                id: item.id,
                categoryId: parseInt(item.category),
                serviceId: parseInt(item.service),
                filleted: item.filleted === "filleted" ? true : false,
                sliced: item.sliced === "sliced" ? true : false,
                otherCategory: item.otherCategory || "",
                otherService: item.otherService || "",
                details: item.details || "",
            });
        }
    };

    const loaded = (obj) => {
        return Object.keys(obj).length > 0;
    };

    return (
        <div className="edit-item">
            <h3>Breyta vöru</h3>
            {loaded(item) && loaded(services) && loaded(categories) && (
                <ItemForm
                    existingItem={item}
                    categories={categories}
                    services={services}
                    submitHandler={editItem}
                />
            )}
            {error && <p className="error">Vara fannst ekki</p>}
            {updateError && <p className="error">Gat ekki uppfært vöru</p>}
        </div>
    );
};

export default EditItem;
