import React from "react";
import { shallow, mount } from "enzyme";
import CustomerListItem from "./CustomerListItem";
jest.mock("react-router-dom");

describe("<CustomerListItem />", () => {
    describe("CustomerListItem renders properly", () => {
        let wrapper;
        let customer;
        const testCustomer = { id: "1", name: "arni" };
        let CustomerListItemComponent = (
            <CustomerListItem customer={testCustomer} />
        );
        beforeEach(() => {
            wrapper = mount(CustomerListItemComponent);
            customer = wrapper.props().customer;
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("CustomerListItem component renders properly with props", () => {
            it("should not be null", () => {
                expect(wrapper).not.toBeNull;
            });
            it("should have property that is not null", () => {
                expect(customer).not.toBeNull;
            });
            it("should not have property with id 2", () => {
                expect(customer.id).not.toEqual("2");
            });
            it("should have property with id 1", () => {
                expect(customer.id).toEqual("1");
            });
            it("should not have property with name gunnar", () => {
                expect(customer.name).not.toEqual("gunnar");
            });
            it("should have property with name arni", () => {
                expect(customer.name).toEqual("arni");
            });
        });
    });
});
