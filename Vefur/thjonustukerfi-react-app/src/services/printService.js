import moment from "moment";

const printOrder = (order) => {
    console.log("About to print order");

    window.BrowserPrint.getDefaultDevice("printer", function(device){
        console.log("Successfully got the default printer");
        console.log(device);

        if (device)  {
            internalPrintOrder(order, device);
        } else {
            console.log("Device NOT acquired");
        }
    }, function(error){
        console.log("Error in attempting to load default printer");
    });
};

const printItem = (item) => {
    window.BrowserPrint.getDefaultDevice("printer", function(device){
        console.log("Successfully got the default printer");
        console.log(device);

        if (device)  {
            internalPrintSingleLabel(item, device);
        } else {
            console.log("Device NOT acquired");
        }
    }, function(error){
        console.log("Error in attempting to load default printer");
    });
};

const dateFormat = (date) => {
    moment.locale("is");
    return moment(date).format("ll");
};

const internalPrintOrder = (order, device) => {
    console.log("Order is: ");
    console.log(order);
    for (var i = 0; i < order.items.length; ++i) {
        var item = order.items[i];
        internalPrintSingleLabel(item, device);
    }
};

const internalPrintSingleLabel = (item, device) => {

    var strPrintData = `^XA
^CI28

^FX Top section with logo, name and address.
^CF0,40
^FO50,30^FDPöntun nr ${item.orderId} - vara nr. ${item.id}^FS
^FO50,80^FDDagsetning: ${dateFormat(item.dateCreated)}^FS
^FO50,150^GB700,1,3^FS

^FX Second section with recipient address and permit information.
^CFA,30
^FO50,180^FDTegund:   ${item.category}^FS
^FO50,220^FDÞjónusta: ${item.service}^FS
^FO50,260^FDFlökun:   ${item.json.sliced === true ? "Já" : "Nei"}^FS
^FO50,300^FDPökkun:   ${item.json.filleted === true ? "Heilt flak" : "Bitar"}^FS
^FO50,340^FDMagn:     ${item.quantity}^FS
^CFA,15
^FO50,390^GB700,1,3^FS

^FX Third section with barcode.
^BY5,2,140
^FO0,420^BC^FD${item.barcode}^FS

^XZ`;

    console.log("About to print the following ZPL:");
    console.log(strPrintData);
    console.log("from the following item:");
    console.log(item);

    device.send(strPrintData, function(success){
        console.log("Sent to printer");
        console.log(success);
    }, function(error){
        console.error("Error:" + error);
    });
};

export default {
    printOrder,
    printItem
};