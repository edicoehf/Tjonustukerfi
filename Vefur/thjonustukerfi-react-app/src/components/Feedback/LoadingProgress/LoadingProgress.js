import React from "react";
import CircularProgress from "@material-ui/core/CircularProgress";
import { isLoadingType, sizeType } from "../../../types";

/**
 * Display a spinner when loading
 *
 * @component
 * @category Feedback
 */
const LoadingProgress = ({ loading, size }) => {
    return <>{loading === true && <CircularProgress size={size || 24} />}</>;
};

LoadingProgress.propTypes = {
    /** Is component loading */
    loading: isLoadingType,
    /** Size of spinner */
    size: sizeType,
};

export default LoadingProgress;
