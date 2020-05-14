import React, { useRef } from "react";
import moment from "moment";
import "moment/locale/is";
import { Box, Paper } from "@material-ui/core";
import ReactToPrint from "react-to-print";
import "./PrintItemView.css";
import useItemPrintDetails from "../../../hooks/useItemPrintDetails";
import { Button } from "@material-ui/core";
import PrintIcon from "@material-ui/icons/Print";

const PrintItemView = ({ id, width, height }) => {
    const componentRef = useRef();
    const { item, isLoading } = useItemPrintDetails(id);

    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("ll");
    };

    return (
        <>
            {!isLoading ? (
                <div style={{ display: "none" }}>
                    <Box
                        className="box"
                        ref={componentRef}
                        maxWidth={width}
                        maxHeight={height}
                        component={Paper}
                    >
                        <div className="upper-line">
                            <div className="print-order-id">
                                <b>Pöntunar nr: </b>
                                {item.orderId}
                            </div>
                            <div className="print-item-id">
                                <b>Vöru nr: </b>
                                {item.id}
                            </div>
                            <div className="print-date-created">
                                <b>Komudagur: </b>
                                {dateFormat(item.dateCreated)}
                            </div>
                        </div>
                        <div className="lower-line">
                            <div className="left-line">
                                <div className="print-item-category">
                                    <b>Tegund: </b>
                                    {item.json.otherCategory || item.category}
                                </div>
                                <div className="print-item-service">
                                    <b>Þjónusta: </b>
                                    {item.json.otherService || item.service}
                                </div>
                                <div className="print-item-filleted">
                                    <b>Flökun: </b>
                                    {item.json.filleted ? "Flakað" : "Óflakað"}
                                </div>
                                <div className="print-item-sliced">
                                    <b>Pökkun: </b>
                                    {item.json.sliced ? "Bitar" : "Heilt Flak"}
                                </div>
                            </div>
                            <div className="right-line">
                                <img
                                    src={
                                        `data:image/jpeg;base64, ` +
                                        item.barcodeImage
                                    }
                                    alt="could not load barcode"
                                />
                                {item.barcode}
                            </div>
                        </div>
                    </Box>
                </div>
            ) : (
                <>Loading</>
            )}
            <div>
                <ReactToPrint
                    trigger={() => (
                        <Button
                            className="print-item-button"
                            variant="contained"
                            size="medium"
                        >
                            <PrintIcon className="print-icon" size="small" />
                            <b>Prenta</b>
                        </Button>
                    )}
                    content={() => componentRef.current}
                />
            </div>
        </>
    );
};

export default PrintItemView;
