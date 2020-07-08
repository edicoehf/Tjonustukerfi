import React from "react";

/**
 * Hook that handles using a searchbar, filters list according to searchbar input
 *
 * @param filterList - List that should be filtered
 * @param initKey - By which key in the objects should the list be filtered by
 *
 * @category Input
 * @subcategory Hooks
 */
const useSearchBar = (filterList, initKey) => {
    // The input in the searchbar
    const [searchTerm, setSearchTerm] = React.useState("");
    // The filtered list
    const [searchResults, setSearchResults] = React.useState([]);
    // Use 'name' key if nothing is provided
    const key = initKey || "name";
    var keys = [];
    if (key.includes(",")) {
        keys = key.split(",");
    }
    // Watch for changes in the input
    const handleChange = (event) => {
        setSearchTerm(event.target.value);
    };
    React.useEffect(() => {
        // Filter by searchterm
        var results = [];
        if (keys.length === 2) {
            results = filterList.filter((filterListItem) =>
            filterListItem[keys[0]].toLowerCase().includes(searchTerm.toLowerCase()) || filterListItem[keys[1]].toLowerCase().includes(searchTerm.toLowerCase())
            );
        } else {
            results = filterList.filter((filterListItem) =>
            filterListItem[key].toLowerCase().includes(searchTerm.toLowerCase())
            );
        }
        // Set the results
        setSearchResults(results);
    }, [searchTerm, filterList, key]);

    return { searchResults, handleChange, searchTerm };
};
export default useSearchBar;
