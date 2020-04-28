import React from "react";
import PropTypes from "prop-types";
import { TextField } from "@material-ui/core";

const TextInput = (props) => {
    const {
        value,
        onInput,
        errorMessage,
        label,
        name,
        htmlId,
        className,
    } = props;
    const error = errorMessage ? true : false;
    return (
        <TextField
            id={htmlId}
            className={className}
            name={name}
            label={label}
            helperText={errorMessage}
            error={error}
            value={value}
            onChange={onInput}
        />
    );
};

TextInput.propTypes = {
    value: PropTypes.string.isRequired,
    htmlId: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    onInput: PropTypes.func.isRequired,
    errorMessage: PropTypes.string,
    label: PropTypes.string,
};

export default TextInput;
