import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import ItemForm from "../ItemForm/ItemForm";
import useGetServices from "../../../hooks/useGetServices";
import useGetCategories from "../../../hooks/useGetCategories";
import useUpdateItem from "../../../hooks/useUpdateItem";

const EditItem = ({ match }) => {
    const id = match.params.id;
    const { services } = useGetServices();
    const { categories } = useGetCategories();
    const { item, error, fetchItem } = useGetItemById(id);
    const { updateError, handleUpdate, isProcessing } = useUpdateItem();

    const editItem = (item, cb) => {
        if (!isProcessing) {
            handleUpdate(item);
            cb();
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
        </div>
    );
};

export default EditItem;
