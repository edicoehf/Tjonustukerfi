import React from "react";
import { shallow, mount } from "enzyme";
import OrderItem from "./OrderItem";
jest.mock("react-router-dom");

describe("<OrderItem />", () => {
    let wrapper;
    let testProps = {
        id: 52,
        category: "Lax",
        service: "Birkireyking",
        barcode: "50050001",
        json: { location: "none", slices: "1 biti" },
        state: "Vinnslu",
    };

    beforeEach(() => {
        wrapper = mount(
            shallow(<OrderItem key={testProps.id} item={testProps} />).get(0)
        );
    });

    it("Should have 6 children", () => {
        expect(wrapper.find("tr").at(0).instance().children.length).toBe(6);
    });

    it("Should display category correctly", () => {
        expect(
            wrapper.find(".order-item-category").at(0).childAt(0).text()
        ).toBe(testProps.category);
    });

    it("Should display service correctly", () => {
        expect(
            wrapper.find(".order-item-service").at(0).childAt(0).text()
        ).toBe(testProps.service);
    });

    it("Should display barcode correctly", () => {
        expect(
            wrapper.find(".order-item-barcode").at(0).childAt(0).text()
        ).toBe(testProps.barcode);
    });

    it("Should display state correctly", () => {
        expect(wrapper.find(".order-item-state").at(0).childAt(0).text()).toBe(
            testProps.state
        );
    });
});
