import React, { useRef } from "react";
import moment from "moment";
import "moment/locale/is";
import { Box, Paper } from "@material-ui/core";
import ReactToPrint from "react-to-print";
import "./PrintItemView.css";

const PrintItemView = ({ item }) => {
    const componentRef = useRef();

    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("ll");
    };

    return (
        <>
            <div style={{ display: "none" }}>
                <Box
                    ref={componentRef}
                    maxWidth="15cm"
                    maxHeight="10cm"
                    component={Paper}
                >
                    <div className="upper-line">
                        <div>
                            <b>Pöntunar nr: </b> {item.orderId}
                        </div>
                        <div>
                            <b>Vöru nr: </b> {item.id}
                        </div>
                        <div>
                            <b>Komudagur: </b> {dateFormat(item.dateCreated)}
                        </div>
                    </div>
                    <div className="lower-line">
                        <div className="left-line">
                            <div>
                                <b>Tegund: </b>
                                {item.json.otherCategory || item.category}
                            </div>
                            <div>
                                <b>Þjónusta: </b>
                                {item.json.otherSerice || item.service}
                            </div>
                            <div>
                                <b>Flökun: </b>
                                {item.json.filleted ? "Flakað" : "Óflakað"}
                            </div>
                            <div>
                                <b>Pökkun: </b>
                                {item.json.sliced ? "Bitar" : "Heilt Flak"}
                            </div>
                            {item.details ? (
                                <div>
                                    <b>Annað: </b> {item.details}
                                </div>
                            ) : (
                                <></>
                            )}
                        </div>
                        <div className="right-line">
                            <b>Strikamerki</b>
                        </div>
                    </div>
                </Box>
            </div>
            <div>
                <ReactToPrint
                    trigger={() => <button>Print this out!</button>}
                    content={() => componentRef.current}
                />
            </div>
        </>
    );
};

export default PrintItemView;
