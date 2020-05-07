import React from "react";
import LoadingProgress from "../LoadingProgress/LoadingProgress";
import "./ProgressButton.css";

const ProgressButton = ({ isLoading, children }) => {
    return (
        <div className="button-wrapper progress-button">
            {children}
            <LoadingProgress loading={isLoading} />
        </div>
    );
};

export default ProgressButton;
