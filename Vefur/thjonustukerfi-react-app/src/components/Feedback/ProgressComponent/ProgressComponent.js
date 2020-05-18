import React from "react";
import LoadingProgress from "../LoadingProgress/LoadingProgress";
import "./ProgressComponent.css";
import { isLoadingType } from "../../../types";

/**
 * Display large spinner while a component is loading
 *
 * @component
 * @category Feedback
 */
const ProgressComponent = ({ isLoading }) => {
    return (
        <div className="progress-component">
            <LoadingProgress loading={isLoading} size={68} />
        </div>
    );
};

ProgressComponent.propTypes = {
    /** Is the component loading */
    isLoading: isLoadingType,
};

export default ProgressComponent;
