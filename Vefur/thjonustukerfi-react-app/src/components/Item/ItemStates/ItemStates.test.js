import React from "react";
import { shallow, mount } from "enzyme";
import ItemStates from "./ItemStates";

describe("<ItemStates />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<ItemStates />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("ItemStates component renders properly", () => {
        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain h1 HTML tag", () => {
            const h1Tag = wrapper.find("h1");
            expect(h1Tag).not.toBeNull;
        });
        it("should contain a div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });
});
