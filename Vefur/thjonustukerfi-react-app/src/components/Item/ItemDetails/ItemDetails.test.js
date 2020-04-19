import React from "react";
import { shallow, mount } from "enzyme";
import ItemDetails from "./ItemDetails";
import useGetItemById from "../../../hooks/useGetItemById";
import ItemStates from "../ItemStates/ItemStates";
jest.mock("../../../hooks/useGetItemById");
jest.mock("react-router-dom");

describe("<ItemDetails />", () => {
    let wrapper;
    const testItem = {
        id: 3,
        itemId: 3,
        service: "Taðreyking",
        category: "Lambakjöt",
        state: "Í vinnslu",
        dateModified: "2020-04-17T22:50:05.146677",
    };

    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    afterEach(() => {
        jest.clearAllMocks();
    });

    beforeEach(() => {
        wrapper = mount(shallow(<ItemDetails id={"1"} />).get(0));
    });

    useGetItemById.mockReturnValue({
        item: testItem,
        error: null,
    });

    it("Should display item id correctly", () => {
        expect(wrapper.find(".item-title").at(0).childAt(1).text()).toBe(
            testItem.id.toString()
        );
    });

    it("Should display item category correctly", () => {
        expect(wrapper.find(".item-category").at(0).childAt(1).text()).toBe(
            testItem.category
        );
    });

    it("Should display item service correctly", () => {
        expect(wrapper.find(".item-service").at(0).childAt(1).text()).toBe(
            testItem.service
        );
    });

    it("Should display item current state correctly", () => {
        expect(wrapper.find(".item-currentstate").at(0).childAt(1).text()).toBe(
            testItem.state
        );
    });
});
