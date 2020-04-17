import React from "react";
import { shallow, mount } from "enzyme";
import CustomerSelectListItem from "./CustomerSelectListItem";

describe("<CustomerSelectListItem />", () => {
    describe("CustomerSelectListItem renders properly", () => {
        let wrapper;
        let customer;
        const testCustomer = { id: "1", name: "arni", email: "arni@arni.is" };
        let CustomerSelectListItemComponent = (
            <CustomerSelectListItem
                customer={testCustomer}
                addCustomer={() => {}}
            />
        );
        beforeEach(() => {
            wrapper = mount(CustomerSelectListItemComponent);
            customer = wrapper.props().customer;
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("CustomerSelectListItem component renders properly with props", () => {
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

            it("should not have property with email arni@inra.is", () => {
                expect(customer.email).not.toEqual("arni@inra.is");
            });

            it("should have property with email arni@arni.is", () => {
                expect(customer.email).toEqual("arni@arni.is");
            });
        });
    });
});
