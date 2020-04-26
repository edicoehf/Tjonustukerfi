import React from "react";
import { shallow, mount } from "enzyme";
import OrderItemList from "./OrderItemList";
jest.mock("react-router-dom");

describe("<OrderItemList />", () => {
    let wrapper;
    let testProps = [
        {
            id: 52,
            category: "Lax",
            service: "Birkireyking",
            barcode: "50050001",
            json: { location: "none", slices: "1 biti" },
        },
        {
            id: 42,
            category: "Bleikja",
            service: "Taðreyking",
            barcode: "50450001",
            json: { location: "none", slices: "1 biti" },
        },
        {
            id: 53,
            category: "Sjófiskur",
            service: "Birkireyking",
            barcode: "52050001",
            json: { location: "none", slices: "1 biti" },
        },
    ];

    beforeEach(() => {
        wrapper = mount(shallow(<OrderItemList items={testProps} />).get(0));
    });

    it("Should have 5 columns", () => {
        expect(
            wrapper.find("tr.order-item").at(0).instance().children.length
        ).toBe(5);
    });
});
