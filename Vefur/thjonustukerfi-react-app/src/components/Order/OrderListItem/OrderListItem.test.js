import React from "react";
import { shallow, mount } from "enzyme";
import OrderListItem from "./OrderListItem";
jest.mock("react-router-dom", () => ({
    useHistory: () => ({
        push: jest.fn(),
    }),
}));

describe("<OrderListItem />", () => {
    describe("OrderListItem renders properly", () => {
        let wrapper;
        let order;
        const testOrder = {
            id: 1,
            customerId: 1,
            customer: "arni",
            customerEmail: "arni@arni.is",
            dateCreated: "1. mars",
            items: [{ id: 1, category: "1", service: "1" }],
        };
        let OrderListItemComponent = <OrderListItem order={testOrder} />;
        beforeEach(() => {
            wrapper = mount(OrderListItemComponent);
            order = wrapper.props().order;
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("OrderListItem component renders properly with props", () => {
            it("should not be null", () => {
                expect(wrapper).not.toBeNull;
            });

            it("should have property that is not null", () => {
                expect(order).not.toBeNull;
            });

            it("should have property with name arni", () => {
                expect(order.customer).toEqual("arni");
            });

            it("should not have property with name einar", () => {
                expect(order.customer).not.toEqual("einar");
            });
            it("should have property with email arni@arni.is", () => {
                expect(order.customerEmail).toEqual("arni@arni.is");
            });
            it("should not have property with email arni@inra.is", () => {
                expect(order.customerEmail).not.toEqual("arni@inra.is");
            });
            it("should have correct date created", () => {
                expect(order.dateCreated).toEqual("1. mars");
            });
            it("should not have incorrect date created", () => {
                expect(order.dateCreated).not.toEqual("1. feb");
            });
        });
    });
});
