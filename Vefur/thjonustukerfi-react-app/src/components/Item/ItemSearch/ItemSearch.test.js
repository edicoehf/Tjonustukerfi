import React from "react";
import { shallow, mount } from "enzyme";
import ItemSearch from "./ItemSearch";

describe("<ItemSearch />", () => {
    let wrapper;
    beforeEach(() => {
        wrapper = mount(shallow(<ItemSearch />).get(0));
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("ItemSearch component renders properly", () => {
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
