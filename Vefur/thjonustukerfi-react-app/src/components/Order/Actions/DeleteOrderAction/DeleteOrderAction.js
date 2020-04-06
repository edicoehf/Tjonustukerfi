import React from "react";
import { Button } from "react-bootstrap";
import useDeleteOrderById from "../../../../hooks/useDeleteOrderById";

const DeleteOrderAction = ({ id }) => {
    const { error, handleDelete, isDeleting } = useDeleteOrderById(id);

    return (
        <div className="delete-order">
            <Button
                variant="danger"
                disabled={isDeleting}
                onClick={handleDelete}
            >
                Eyða
            </Button>
            {error && <p className="delete-error">Gat ekki eytt pöntun</p>}
        </div>
    );
};

export default DeleteOrderAction;
