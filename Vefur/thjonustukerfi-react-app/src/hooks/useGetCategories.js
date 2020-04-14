import React from "react";
import categoryService from "../services/categoryService";

const useGetCategories = () => {
    const [categories, setCategories] = React.useState({});
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        categoryService
            .getCategories()
            .then((categories) => {
                setCategories(categories);
                setError(null);
            })
            .catch((error) => setError(error));
    }, []);
    return { categories, error };
};

export default useGetCategories;
