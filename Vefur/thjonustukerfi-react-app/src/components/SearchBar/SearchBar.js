import React from "react";
import "./SearchBar.css";
import {
    FormControl,
    InputLabel,
    Input,
    InputAdornment,
    IconButton,
} from "@material-ui/core";
import SearchIcon from "@material-ui/icons/Search";

const SearchBar = ({
    searchTerm,
    handleChange,
    placeHolder,
    handleClick,
    htmlId,
}) => {
    return (
        <div className="search-bar">
            <FormControl>
                <InputLabel htmlFor={htmlId}>{placeHolder}</InputLabel>
                <Input
                    id="item-id-search"
                    type="text"
                    autoComplete="off"
                    value={searchTerm}
                    onChange={handleChange}
                    endAdornment={
                        <InputAdornment position="end">
                            {handleClick ? (
                                <IconButton onClick={handleClick}>
                                    <SearchIcon />
                                </IconButton>
                            ) : (
                                <SearchIcon />
                            )}
                        </InputAdornment>
                    }
                />
            </FormControl>
        </div>
    );
};

export default SearchBar;
