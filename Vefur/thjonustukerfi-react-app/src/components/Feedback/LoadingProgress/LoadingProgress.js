import React from "react";
import CircularProgress from "@material-ui/core/CircularProgress";

const LoadingProgress = ({ loading, size }) => {
    return <>{loading === true && <CircularProgress size={size || 24} />}</>;
};

export default LoadingProgress;
