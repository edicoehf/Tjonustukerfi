import React from "react";
import { shallow, mount } from "enzyme";
import CustomerMain from "./CustomerMain";
jest.mock("react-router-dom", () => ({
    useHistory: () => ({
        push: jest.fn(),
    }),
}));

describe("<CustomerMain />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<CustomerMain />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("CustomerMain component renders properly", () => {
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
