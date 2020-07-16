import React from "react";
import { shallow, mount } from "enzyme";
import OrderItem from "./OrderItem";
import { Router } from "react-router-dom";

describe("<OrderItem />", () => {
    let wrapper;
    let testProps = {
        id: 52,
        category: "Lax",
        service: "Birkireyking",
        barcode: "50050001",
        json: { location: "none", sliced: true, filleted: true },
        state: "Vinnslu",
        details: "something",
    };
    const historyMock = {
        push: jest.fn(),
        location: {},
        listen: jest.fn(),
        createHref: jest.fn(),
    };
    beforeEach(() => {
        wrapper = mount(
            <Router history={historyMock}>
                <OrderItem key={testProps.id} item={testProps} />
            </Router>
        );
    });

    it("Should have 9 children", () => {
        expect(wrapper.find("tr").at(0).instance().children.length).toBe(9);
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

    it("Should display filleted correctly", () => {
        expect(
            wrapper.find(".order-item-filleted").at(0).childAt(0).text()
        ).toBe("Já");
    });

    it("Should display sliced correctly", () => {
        expect(wrapper.find(".order-item-sliced").at(0).childAt(0).text()).toBe(
            "Bitar"
        );
    });
    it("Should display details correctly", () => {
        expect(
            wrapper.find(".order-item-details").at(0).childAt(0).text()
        ).toBe("Annað: " + testProps.details);
    });
});
