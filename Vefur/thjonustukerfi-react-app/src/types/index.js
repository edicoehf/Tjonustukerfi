import PropTypes from "prop-types";

const {
    shape,
    arrayOf,
    func,
    bool,
    string,
    number,
    oneOfType,
    object,
    element,
} = PropTypes;

export const idType = oneOfType([number, string]);

export const categoriesType = arrayOf(object);
export const servicesType = arrayOf(object);

export const itemType = shape({
    id: idType.isRequired,
    category: string.isRequired,
    service: string.isRequired,
    barcode: string,
});

export const itemsType = arrayOf(itemType);

export const orderType = shape({
    id: idType.isRequired,
    customer: string.isRequired,
    customerId: idType.isRequired,
    barcode: string,
    items: itemsType.isRequired,
    dateCreated: string,
    dateModified: string,
    dateCompleted: string,
});

export const archivedOrderItemType = shape({
    category: string,
    service: string,
    extraDataJSON: string,
    dateCreated: string,
    dateCompleted: string,
});

export const archivedOrderItemsType = arrayOf(archivedOrderItemType);

export const archivedOrderType = shape({
    id: idType,
    customer: string,
    customerEmail: string,
    json: string,
    dateCreated: string,
    dateCompleted: string,
    items: archivedOrderItemsType,
});

export const archivedOrdersType = arrayOf(archivedOrderType);

export const ordersType = arrayOf(orderType);

export const customerType = shape({
    id: idType.isRequired,
    name: string.isRequired,
    ssn: string,
    telephone: string,
    email: string.isRequired,
    postalCode: string,
    address: string,
});

export const customersType = arrayOf(customerType);

export const existingCustomerType = shape({
    name: string,
    ssn: string,
    telephone: string,
    email: string,
    postalCode: string,
    address: string,
});

export const orderItemType = shape({
    category: string,
    service: string,
    amount: number,
    categoryName: string,
    serviceName: string,
});

export const orderFormValuesType = shape({
    items: arrayOf(orderItemType),
    customer: customerType,
});

export const orderFormFunctionsType = shape({
    addItems: func,
    removeItem: func,
    addCustomer: func,
});

export const iconType = element;

export const removeType = func;
export const handleCloseType = func;
export const handleOpenType = func;
export const handleCreateType = func;
export const handleDeleteType = func;
export const handleAcceptType = func;
export const addCustomerType = func;
export const addItemsType = func;
export const createOrderType = func;
export const cancelOrderType = func;
export const submitHandlerType = func;
export const cbType = func;
export const onSubmitType = func;

export const isLoadingType = bool;
export const openType = bool;
export const isProcessingType = bool;
export const isDeletingType = bool;
export const successType = bool;
export const updatedType = bool;
export const borderType = bool;
export const expandedType = bool;

export const descriptionType = string;
export const confirmTextType = string;
export const declineTextType = string;
export const titleType = string;
export const nameType = string;
export const errorType = string;
export const valueType = string;

export const sizeType = number;
