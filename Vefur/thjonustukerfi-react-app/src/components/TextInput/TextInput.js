import React from "react";
import PropTypes from "prop-types";
import { TextField } from "@material-ui/core";

/**
 * Custom Textfield input using mUI
 *
 * @component
 * @category Input
 */

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
    /** Value to be displayed */
    value: PropTypes.string,
    /** Element ID */
    htmlId: PropTypes.string,
    /** Element name */
    name: PropTypes.string,
    /** Function called onInput */
    onInput: PropTypes.func,
    /** Error message to be displayed */
    errorMessage: PropTypes.string,
    /** Input label */
    label: PropTypes.string,
};

export default TextInput;
