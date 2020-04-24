import React from "react";
import { Button } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import useDeleteOrderById from "../../../../hooks/useDeleteOrderById";
import {
    idType,
    handleDeleteType,
    isDeletingType,
} from "../../../../types/index";
import "./DeleteOrderAction.css";

const DeleteOrderAction = ({ id }) => {
    const { error, handleDelete, isDeleting } = useDeleteOrderById(id);

    return (
        <div className="delete-order">
            <Button
                className="delete-order-button"
                size="medium"
                color="secondary"
                variant="contained"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                <DeleteIcon className="delete-order-icon" size="small" />
                <b>Eyða Pöntun</b>
            </Button>
            {error && <p className="delete-error">Gat ekki eytt pöntun</p>}
        </div>
    );
};

DeleteOrderAction.propTypes = {
    id: idType,
    handleDelete: handleDeleteType,
    isDeleting: isDeletingType,
};

export default DeleteOrderAction;
