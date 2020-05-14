import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import ItemForm from "../ItemForm/ItemForm";
import useGetServices from "../../../hooks/useGetServices";
import useGetCategories from "../../../hooks/useGetCategories";
import useUpdateItem from "../../../hooks/useUpdateItem";
import "./EditItem.css";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

/**
 * Page where the information of an item can be edited
 *
 * @component
 * @category Item
 */

const EditItem = ({ match, history }) => {
    // Get id from url
    const id = match.params.id;

    // Fetch services
    const { services } = useGetServices();
    // Fetch categories
    const { categories } = useGetCategories();
    // Fetch Item
    const { item, error } = useGetItemById(id);

    // Send user to details page for item
    const redirect = () => {
        history.push(`/item/${id}`);
    };

    // Use update item hook, send redirect function as cb to be called on success
    const { updateError, handleUpdate, isProcessing } = useUpdateItem(redirect);

    // Call update function with correctly structured data
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

    // Check if object has loaded
    // Itemform can have unexpected behaviour if not all items are loaded prior mounting
    const loaded = (obj) => {
        return Object.keys(obj).length > 0;
    };

    return (
        <div className="edit-item">
            <h3>Breyta vöru</h3>
            {loaded(item) && loaded(services) && loaded(categories) ? (
                <ItemForm
                    existingItem={item}
                    categories={categories}
                    services={services}
                    submitHandler={editItem}
                />
            ) : (
                <>{!error && <ProgressComponent isLoading={true} />}</>
            )}
            {error && <p className="error">Vara fannst ekki</p>}
            {updateError && <p className="error">Gat ekki uppfært vöru</p>}
        </div>
    );
};

export default EditItem;
