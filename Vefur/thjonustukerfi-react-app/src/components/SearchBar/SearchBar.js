import React from "react";
import "./SearchBar.css";

const SearchBar = ({ searchTerm, handleChange, placeHolder }) => {
    return (
        <input
            className="search-bar"
            type="text"
            placeholder={placeHolder}
            value={searchTerm}
            onChange={handleChange}
        />
    );
};

export default SearchBar;
