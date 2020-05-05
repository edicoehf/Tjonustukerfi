import React from "react";
import LoadingProgress from "../LoadingProgress/LoadingProgress";
import "./ProgressComponent.css";

const ProgressComponent = ({ isLoading }) => {
    return (
        <div className="progress-component">
            <LoadingProgress loading={isLoading} size={68} />
        </div>
    );
};

export default ProgressComponent;
