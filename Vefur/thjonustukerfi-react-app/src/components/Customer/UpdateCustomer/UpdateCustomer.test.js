import React from "react";
import { shallow, mount } from "enzyme";
import UpdateCustomer from "./UpdateCustomer";
jest.mock("react-router-dom", () => ({
    useHistory: () => ({
        push: jest.fn(),
    }),
}));

describe("<UpdateCustomer />", () => {
    let wrapper;
    const matchProp = { params: { id: 1 }, isExact: true, path: "", url: "" };
    beforeEach(() => {
        wrapper = mount(shallow(<UpdateCustomer match={matchProp} />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("UpdateCustomer component renders properly", () => {
        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain b HTML tag", () => {
            const h1Tag = wrapper.find("h1");
            expect(h1Tag).not.toBeNull;
        });
        it("should contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });
});
