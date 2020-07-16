import React from "react";
import useDebounce from './useDebouncedSearch';

/**
 * Hook that handles using a searchbar, filters list according to searchbar input
 *
 * @param filterList - List that should be filtered
 * @param initKey - By which key in the objects should the list be filtered by
 *
 * @category Input
 * @subcategory Hooks
 */
const useSearchBar = (filterList, initKey, limit) => {
    // The input in the searchbar
    const [searchTerm, setSearchTerm] = React.useState("");
    // The filtered list:
    const [searchResults, setSearchResults] = React.useState([]);
    // Use 'name' key if nothing is provided
    const key = initKey || "name";
    // Watch for changes in the input
    const handleChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const debouncedSearchTerm = useDebounce(searchTerm, 500);

    React.useEffect(() => {
        //console.log("About to execute search bar effect");
        //console.log("Length of filterList: " + filterList.length);
        //console.log("Debounced search term is: " + debouncedSearchTerm);
        var keys = [];
        if (key.includes(",")) {
            keys = key.split(",");
        }
            // Filter by searchterm
        var results = [];
        if (keys.length === 2) {
            // The second item may not be a string...
            results = filterList.filter((filterListItem) =>
                filterListItem[keys[0]].toLowerCase().includes(debouncedSearchTerm.toLowerCase()) || filterListItem[keys[1]].toString().toLowerCase().includes(debouncedSearchTerm.toLowerCase())
            );
        } else {
            results = filterList.filter((filterListItem) =>
                filterListItem[key].toLowerCase().includes(debouncedSearchTerm.toLowerCase())
            );
        }

        if (limit) {
            results = results.slice(0, 20);
            setSearchResults(results);
        } else {
            setSearchResults(results);
        }
        // Set the results
    }, [key, debouncedSearchTerm, filterList, limit]);
    // For the moment, we must NOT include filterList in the dependency array. For some reason, the effect seems to run m
    //}, [key, debouncedSearchTerm]);

    return { searchResults, handleChange, searchTerm };
};
export default useSearchBar;
