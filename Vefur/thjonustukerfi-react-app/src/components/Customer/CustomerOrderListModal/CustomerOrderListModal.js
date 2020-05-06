import React from "react";
import {
    Button,
    Dialog,
    DialogContent,
    DialogActions,
} from "@material-ui/core";
import { idType, handleCloseType, openType } from "../../../types/index";
import OrderList from "../../Order/OrderList/OrderList";
import useGetOrdersByCustomerId from "../../../hooks/useGetOrdersByCustomerId";

const CustomerOrderListModal = ({ customerId, open, handleClose }) => {
    const { orders, error, isLoading } = useGetOrdersByCustomerId(customerId);
    return (
        <Dialog open={open} onClose={handleClose} maxWidth="xl">
            <DialogContent className="dialog-cont">
                <OrderList
                    orders={orders}
                    error={error}
                    isLoading={isLoading}
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose} color="secondary">
                    Loka
                </Button>
            </DialogActions>
        </Dialog>
    );
};

CustomerOrderListModal.propTypes = {
    customerId: idType,
    handleClose: handleCloseType,
    open: openType,
};

export default CustomerOrderListModal;
