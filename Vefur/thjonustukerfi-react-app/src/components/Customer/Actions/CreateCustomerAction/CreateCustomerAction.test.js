import React from "react";
import { shallow, mount } from "enzyme";
import CreateCustomerAction from "./CreateCustomerAction";
jest.mock("react-router-dom");

describe("<CreateCustomerAction />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<CreateCustomerAction />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("CreateCustomerAction component renders properly", () => {
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
