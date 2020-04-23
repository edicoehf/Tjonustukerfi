import React from "react";

const OrderForm = () => {
    const { services } = useGetServices();
    const { categories } = useGetCategories();

    return (
        <div className="order-form">
            <AddItems
                addItems={addItems}
                categories={categories}
                services={services}
            />
            <ViewItems items={items} remove={removeItem} />
            <AddCustomer customer={customer} addCustomer={addCustomer} />
        </div>
    );
};

export default OrderForm;
