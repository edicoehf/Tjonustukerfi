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

/**
 * A modal that displays all the orders connected to a customer
 */

const CustomerOrderListModal = ({ customerId, open, handleClose }) => {
    // Fetch the customers orders
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
    /** Customer ID */
    customerId: idType,
    /** CB function that closes modal */
    handleClose: handleCloseType,
    /** Should modal be displayed */
    open: openType,
};

export default CustomerOrderListModal;
