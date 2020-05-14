import React from "react";
import PropTypes from "prop-types";
import "./Input.css";

/**
 * Custom input controlled by states
 *
 * @component
 * @category Input
 */

const Input = (props) => {
    const { value, onInput, type, errorMessage, label, name, htmlId } = props;
    return (
        <div className="form-group">
            {label ? (
                <div className="labels">
                    {" "}
                    <label htmlFor={htmlId} className="control-label">
                        {label}
                    </label>{" "}
                    <span className="errors">{errorMessage}</span>{" "}
                </div>
            ) : (
                <></>
            )}
            <input
                type={type}
                value={value}
                onChange={onInput}
                name={name}
                id={htmlId}
                className="form-control"
            />
        </div>
    );
};

Input.propTypes = {
    /** Value to be displayed */
    value: PropTypes.string,
    /** Element ID */
    htmlId: PropTypes.string,
    /** Element name */
    name: PropTypes.string,
    /** Function called onInput */
    onInput: PropTypes.func,
    /** Type of input */
    type: PropTypes.oneOf(["text"]),
    /** Error message to be displayed */
    errorMessage: PropTypes.string,
    /** Input label */
    label: PropTypes.string,
};

export default Input;
