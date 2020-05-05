import React from "react";
import categoryService from "../services/categoryService";

const useGetCategories = () => {
    const [categories, setCategories] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(false);

    React.useEffect(() => {
        setIsLoading(true);
        categoryService
            .getCategories()
            .then((categories) => {
                setCategories(categories);
                setError(null);
            })
            .catch((error) => setError(error))
            .finally(() => {
                setIsLoading(false);
            });
    }, []);
    return { categories, error, isLoading };
};

export default useGetCategories;
