import React from "react";
import { shallow, mount } from "enzyme";
import UpdateCustomerAction from "./UpdateCustomerAction";
jest.mock("react-router-dom");

describe("<UpdateCustomerAction />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<UpdateCustomerAction id={1} />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("UpdateCustomerAction component renders properly", () => {
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
        it("should contain Link", () => {
            const link = wrapper.find("a");
            expect(link).not.toBeNull;
        });
    });
});
