import React from "react";

// Uses search bar and filters by object's name ...
const useSearchBar = filterList => {
    const [searchTerm, setSearchTerm] = React.useState("");
    const [searchResults, setSearchResults] = React.useState([]);
    const handleChange = event => {
        setSearchTerm(event.target.value);
    };
    React.useEffect(() => {
        const results = filterList.filter(filterListItem =>
            filterListItem.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setSearchResults(results);
    }, [searchTerm, filterList]);

    return { searchResults, handleChange, searchTerm };
};
export default useSearchBar;
