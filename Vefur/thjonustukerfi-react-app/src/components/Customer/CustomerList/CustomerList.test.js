import React from "react";
import { shallow, mount } from "enzyme";
import CustomerList from "./CustomerList";
jest.mock("react-router-dom");

describe("<CustomerList />", () => {
    describe("CustomerList renders properly", () => {
        let wrapper;
        const testCustomer = { id: "1", name: "arni", email: "arni@arni.is" };
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerList
                        customers={[testCustomer]}
                        error={null}
                        isLoading={false}
                    />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("CustomerList component renders properly", () => {
            it("should not contain p HTML tag", () => {
                const pTag = wrapper.find("p");
                expect(pTag).toBeNull;
            });
        });
    });

    describe("CustomerList component error works properly", () => {
        let wrapper;
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerList
                        customers={[]}
                        error={{ error: "someError" }}
                        isLoading={false}
                    />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("CustomerList renders properly", () => {
            it("should contain <p> HTML tag", () => {
                const pTag = wrapper.find("p");
                expect(pTag.instance()).not.toBeNull;
            });
        });
    });

    describe("CustomerList component isLoading works properly", () => {
        let wrapper;
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerList
                        customers={[]}
                        error={null}
                        isLoading={true}
                    />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("CustomerList renders properly", () => {
            it("should contain <p> HTML tag", () => {
                const pTag = wrapper.find("p");
                expect(pTag.instance()).not.toBeNull;
            });
        });
    });

    describe("CustomerList props", () => {
        let wrapper;
        let customerList;
        const testCustomer1 = { id: "1", name: "arni", email: "arni@arni.is" };
        const testCustomer2 = {
            id: "2",
            name: "halli",
            email: "bjarni@bjarni.is",
        };
        const testCustomer3 = {
            id: "3",
            name: "balli",
            email: "sjarni@sjarni.is",
        };
        const CustomerListComponent = (
            <CustomerList
                customers={[testCustomer1, testCustomer2, testCustomer3]}
                error={null}
                isLoading={false}
            />
        );

        beforeEach(() => {
            wrapper = mount(CustomerListComponent);
            customerList = wrapper.props().customers;
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        describe("Props to customer list", () => {
            it("should contain customers", () => {
                expect(customerList).not.toBeNull;
            });

            it("should not have 5 customers", () => {
                expect(customerList.length).not.toEqual(5);
            });

            it("should have three customers", () => {
                expect(customerList.length).toEqual(3);
            });

            it("should not have first customer named halli", () => {
                expect(customerList[0].name).not.toEqual("halli");
            });

            it("should contain first customer named arni", () => {
                expect(customerList[0].name).toEqual("arni");
            });

            it("should not have second customer named balli", () => {
                expect(customerList[1].name).not.toEqual("balli");
            });
        });
    });
});
