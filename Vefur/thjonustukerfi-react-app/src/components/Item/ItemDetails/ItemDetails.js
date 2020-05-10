import React from "react";
import useGetItemById from "../../../hooks/useGetItemById";
import {
    TableContainer,
    Table,
    Paper,
    TableRow,
    TableCell,
    TableBody,
    TableHead,
} from "@material-ui/core";
import { Link } from "react-router-dom";
import "./ItemDetails.css";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

const ItemDetails = ({ id, updated, receivedUpdate, componentLoading }) => {
    const { item, error, fetchItem, isLoading } = useGetItemById(id);
    const { category, service, orderId, state, json, barcode, details } = item;
    const [other, setOther] = React.useState({
        location: "",
        sliced: false,
        filleted: false,
        otherCategory: "",
        otherService: "",
    });
    React.useEffect(() => {
        if (json) {
            setOther(json);
        }
    }, [json]);

    if (updated) {
        receivedUpdate();
        fetchItem();
    }

    React.useEffect(() => {
        if (componentLoading !== undefined) {
            componentLoading(isLoading);
        }
    }, [isLoading, componentLoading]);

    return (
        <>
            {!isLoading ? (
                <div className="item-details">
                    {!error ? (
                        <TableContainer component={Paper} elevation={3}>
                            <h3 className="details-item-title">Vara {id}</h3>
                            <Table>
                                <TableHead>
                                    <TableRow className="details-item-info-row no-border">
                                        <TableCell className="details-item-orderid-cell">
                                            <Link to={`/order/${orderId}`}>
                                                Pöntun: {orderId}
                                            </Link>
                                        </TableCell>
                                        <TableCell className="details-item-barcode-cell">
                                            Strikamerki: {barcode}
                                        </TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    <TableRow className="details-item-row no-border">
                                        <TableCell className="details-item-title-cell">
                                            Tegund:
                                        </TableCell>
                                        <TableCell className="details-item-content-cell">
                                            {other.otherCategory
                                                ? other.otherCategory
                                                : category}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow className="details-item-row">
                                        <TableCell className="details-item-title-cell">
                                            Þjónusta:
                                        </TableCell>
                                        <TableCell className="details-item-content-cell">
                                            {other.otherService
                                                ? other.otherService
                                                : service}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow className="details-item-row">
                                        <TableCell className="details-item-title-cell">
                                            Flökun:
                                        </TableCell>
                                        <TableCell className="details-item-content-cell">
                                            {other.filleted
                                                ? "Flakað"
                                                : "Óflakað"}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow className="details-item-row">
                                        <TableCell className="details-item-title-cell">
                                            Pökkun:
                                        </TableCell>
                                        <TableCell className="details-item-content-cell">
                                            {other.sliced
                                                ? "Bitar"
                                                : "Heilt Flak"}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow className="details-item-row">
                                        <TableCell className="details-item-title-cell">
                                            Staða:
                                        </TableCell>
                                        <TableCell className="details-item-content-cell">
                                            {state}
                                        </TableCell>
                                    </TableRow>
                                    {other.location !== "" && (
                                        <TableRow className="details-item-row">
                                            <TableCell className="details-item-title-cell">
                                                Staðsetning:
                                            </TableCell>
                                            <TableCell className="details-item-content-cell">
                                                {other.location}
                                            </TableCell>
                                        </TableRow>
                                    )}
                                    {details !== "" && (
                                        <TableRow className="details-item-row">
                                            <TableCell className="details-item-title-cell">
                                                Annað:
                                            </TableCell>
                                            <TableCell className="details-item-content-cell">
                                                {details}
                                            </TableCell>
                                        </TableRow>
                                    )}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    ) : (
                        <p className="error">
                            Gat ekki sótt upplýsingar um vöru
                        </p>
                    )}
                </div>
            ) : (
                <ProgressComponent isLoading={componentLoading === undefined} />
            )}
        </>
    );
};

export default ItemDetails;
