import React from "react";
import PropTypes from "prop-types";
import "./Input.css";

const Input = props => {
    const { value, onInput, type, errorMessage, label, name, htmlId } = props;
    return (
        <div className="form-group">
            {label ? (
                <div className="label">
                    {" "}
                    <label htmlFor={htmlId} className="control-label">
                        {label}
                    </label>{" "}
                    <span className="error">{errorMessage}</span>{" "}
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
    value: PropTypes.string.isRequired,
    htmlId: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    onInput: PropTypes.func.isRequired,
    type: PropTypes.oneOf(["text"]),
    errorMessage: PropTypes.string,
    label: PropTypes.string
};

export default Input;
