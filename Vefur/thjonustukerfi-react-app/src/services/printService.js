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

    // For some reason, we get the JSON object either as a string or as an object,
    // so we try to handle both cases:
    var jsonData = item.json;
    if (typeof jsonData === 'string') {
        jsonData = JSON.parse(jsonData);
    }

    var category = item.category;
    if (jsonData && jsonData.otherCategory && jsonData.otherCategory.length > 0) {
        category = jsonData.otherCategory;
    }

    var service = item.service;
    if (jsonData && jsonData.otherService && jsonData.otherService.length > 0){
        service = jsonData.otherService;
    }

    // Flökun:
    var filleted = "Nei";
    if (jsonData && jsonData.filleted === true){
        filleted = "Já";
    }

    // Pökkun:
    var sliced = "Bitar";
    if (jsonData && jsonData.sliced === false) {
        sliced = "Heilt flak";
    }


    var strPrintData = `^XA
^CI28

^FX Top section with logo, name and address.
^CF0,40
^FO50,30^FDPöntun nr ${item.orderId} - vara nr. ${item.id}^FS
^FO50,80^FDDagsetning: ${dateFormat(item.dateCreated)}^FS
^FO50,140^GB700,1,3^FS

^FX Second section with recipient address and permit information.
^CFA,30
^FO50,170^FDTegund:   ${category}^FS
^FO50,210^FDÞjónusta: ${service}^FS
^FO50,250^FDFlökun:   ${filleted}^FS
^FO50,290^FDPökkun:   ${sliced}^FS
^FO50,330^FDMagn:     ${item.quantity}^FS
`;

if (item.details && item.details.length > 0) {
    strPrintData += `^FO50,370^FDAnnað:    ${item.details}^FS`;
} else {
    strPrintData += `^FO50,380^GB700,1,3^FS`;
}

strPrintData += `
^CFA,15

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