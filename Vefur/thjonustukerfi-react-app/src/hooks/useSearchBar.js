import React from "react";

// Uses search bar and filters by object's name ...
const useSearchBar = (filterList, initKey) => {
    const [searchTerm, setSearchTerm] = React.useState("");
    const [searchResults, setSearchResults] = React.useState([]);
    const key = initKey || "name";
    const handleChange = (event) => {
        setSearchTerm(event.target.value);
    };
    React.useEffect(() => {
        const results = filterList.filter((filterListItem) =>
            filterListItem[key].toLowerCase().includes(searchTerm.toLowerCase())
        );
        setSearchResults(results);
    }, [searchTerm, filterList, key]);

    return { searchResults, handleChange, searchTerm };
};
export default useSearchBar;
