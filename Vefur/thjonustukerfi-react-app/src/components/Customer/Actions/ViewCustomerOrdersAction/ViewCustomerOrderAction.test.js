import React from "react";
import { shallow, mount } from "enzyme";
import ViewCustomerOrdersAction from "./ViewCustomerOrdersAction";

describe("<ViewCustomerOrderAction />", () => {
    let wrapper;
    const fn = () => {};

    describe("ViewCustomerOrderAction component renders properly when open", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<ViewCustomerOrdersAction handleOpen={fn} />).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain b HTML tag", () => {
            const bTag = wrapper.find("b");
            expect(bTag).not.toBeNull;
        });
        it("should contain a button", () => {
            const button = wrapper.find("button");
            expect(button).not.toBeNull;
        });
    });

    describe("ViewCustomerOrderAction should have working handlers", () => {
        const mockHandleOpen = jest.fn();

        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ViewCustomerOrdersAction handleOpen={mockHandleOpen} />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should detect handleOpen when Pantanir is clicked", () => {
            const button = wrapper.find("button").at(0);
            button.simulate("click");
            expect(mockHandleOpen).toBeCalled();
        });
        it("should not detect handleOpen when Pantanir is not clicked", () => {
            expect(mockHandleOpen).not.toBeCalled();
        });
    });
});
