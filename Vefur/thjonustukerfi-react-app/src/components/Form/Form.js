import React from "react";
import { onSubmitType } from "../../types";

/**
 * Wrapper for forms that overrides submit event with prop
 *
 * @component
 * @category Input
 */

const Form = (props) => {
    return (
        <form onSubmit={props.onSubmit} className="form form-horizontal">
            {props.children}
        </form>
    );
};

Form.propTypes = {
    /** Function to be called on submit */
    onSubmit: onSubmitType,
};

export default Form;
