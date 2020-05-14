import React from "react";
import LoadingProgress from "../LoadingProgress/LoadingProgress";
import "./ProgressButton.css";
import { isLoadingType } from "../../../types";

/**
 * Wraps a button and shows spinner inside it when loading
 *
 * @category Feedback
 */
const ProgressButton = ({ isLoading, children }) => {
    return (
        <div className="button-wrapper progress-button">
            {children}
            <LoadingProgress loading={isLoading} />
        </div>
    );
};

ProgressButton.propTypes = {
    /** Is the button function processing */
    isLoading: isLoadingType,
};

export default ProgressButton;
