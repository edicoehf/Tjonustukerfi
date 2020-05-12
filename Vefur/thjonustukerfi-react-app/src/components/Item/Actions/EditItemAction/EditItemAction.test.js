import React from "react";
import { shallow, mount } from "enzyme";
import EditItemAction from "./EditItemAction";
jest.mock("react-router-dom");

describe("<EditItemAction />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<EditItemAction id={1} />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("EditItemAction component renders properly", () => {
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
        it("should contain a HTML tag", () => {
            const aTag = wrapper.find("a");
            expect(aTag).not.toBeNull;
        });
    });
});
