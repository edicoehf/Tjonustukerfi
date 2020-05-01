import React from "react";
import { shallow, mount } from "enzyme";
import OrderDetails from "./OrderDetails";
import OrderItemList from "../OrderItemList/OrderItemList";
import useGetOrderById from "../../../hooks/useGetOrderById";
import { Router } from "react-router-dom";
jest.mock("../../../hooks/useGetOrderById");

describe("<OrderDetails />", () => {
    let wrapper;
    let testOrder = {
        id: 1,
        customer: "Lewin Lime",
        customerId: 1,
        barcode: "20200001",
        items: [
            {
                id: 1,
                category: "Lax",
                service: "Birkireyking",
                barcode: "50500001",
                json: { location: "somewhere", slices: "2 bitar" },
            },
            {
                id: 2,
                category: "Þorskur",
                service: "Taðreyking",
                barcode: "50503001",
                json: { location: "somewhere", slices: "2 bitar" },
            },
        ],
        dateCreated: "2020-04-02T13:56:40.088218",
        dateModified: "2020-04-02T13:56:40.088256",
        dateCompleted: null,
    };
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);
    const historyMock = {
        push: jest.fn(),
        location: {},
        listen: jest.fn(),
        createHref: jest.fn(),
    };

    afterEach(() => {
        jest.clearAllMocks();
    });

    beforeEach(() => {
        wrapper = mount(
            <Router history={historyMock}>
                <OrderDetails id={"1"} />
            </Router>
        );
    });

    useGetOrderById.mockReturnValue({
        order: testOrder,
        error: null,
    });

    it("Should display order id correctly", () => {
        expect(wrapper.find(".order-title").at(0).childAt(1).text()).toBe(
            testOrder.id.toString()
        );
    });

    it("Should display order barcode correctly", () => {
        expect(wrapper.find(".order-barcode").at(2).childAt(2).text()).toBe(
            testOrder.barcode
        );
    });

    it("Should display order creation date correctly", () => {
        expect(wrapper.find(".order-date").at(2).childAt(2).text()).toBe(
            "2. apríl 2020 kl. 13:56"
        );
    });

    it("Should not display date completed", () => {
        expect(wrapper.containsMatchingElement(".order-completed")).toEqual(
            false
        );
    });

    it("Should display table for items", () => {
        expect(wrapper.find(OrderItemList)).toHaveLength(1);
    });

    it("Should display 3 rows items", () => {
        expect(wrapper.find("tbody").at(1).instance().children).toHaveLength(3);
    });
});
