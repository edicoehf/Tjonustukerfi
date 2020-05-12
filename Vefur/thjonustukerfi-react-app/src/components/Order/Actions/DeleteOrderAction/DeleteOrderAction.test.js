import React from "react";
import { shallow, mount } from "enzyme";
import DeleteOrderAction from "./DeleteOrderAction";
jest.mock("react-router-dom", () => ({
    useHistory: () => ({
        push: jest.fn(),
    }),
}));

describe("<DeleteOrderAction />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<DeleteOrderAction id={1} />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("DeleteOrderAction component renders properly", () => {
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
});
