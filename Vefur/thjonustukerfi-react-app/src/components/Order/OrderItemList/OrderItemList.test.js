import React from "react";
import { shallow, mount } from "enzyme";
import OrderItemList from "./OrderItemList";

describe("<OrderItemList />", () => {
    let wrapper;
    let testProps = [
        {
            id: 52,
            type: "Lax",
            service: "Birkireyking",
            barcode: "50050001",
        },
        {
            id: 42,
            type: "Bleikja",
            service: "Taðreyking",
            barcode: "50450001",
        },
        {
            id: 53,
            type: "Sjófiskur",
            service: "Birkireyking",
            barcode: "52050001",
        },
    ];

    beforeEach(() => {
        wrapper = mount(shallow(<OrderItemList items={testProps} />).get(0));
    });

    it("Should have 3 rows", () => {
        expect(
            wrapper.find("tr.order-item").at(0).instance().children.length
        ).toBe(3);
    });
});
