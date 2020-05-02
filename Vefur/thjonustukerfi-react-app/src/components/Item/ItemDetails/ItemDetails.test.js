import React from "react";
import { shallow, mount } from "enzyme";
import ItemDetails from "./ItemDetails";
import useGetItemById from "../../../hooks/useGetItemById";
jest.mock("../../../hooks/useGetItemById");
jest.mock("react-router-dom");

describe("<ItemDetails />", () => {
    let wrapper;
    const testItem = {
        id: 3,
        categoryId: 1,
        category: "Lax",
        serviceId: 4,
        service: "Salt pækill",
        state: "Í vinnslu",
        dateModified: "2020-04-17T22:50:05.146677",
        json: {
            location: "Vinnslu",
            sliced: false,
            filleted: false,
            otherCategory: "",
            otherService: "",
        },
        barcode: "50500001",
        details: "",
        orderId: 7,
        state: "Vinnslu",
        stateId: 1,
        dateCompleted: null,
    };
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    afterEach(() => {
        jest.clearAllMocks();
    });

    beforeEach(() => {
        wrapper = mount(shallow(<ItemDetails id={"3"} />).get(0));
    });

    useGetItemById.mockReturnValue({
        item: testItem,
        error: null,
    });

    it("Should display item id correctly", () => {
        expect(wrapper.find("h3").at(0).text()).toBe(
            "Vara " + testItem.id.toString()
        );
    });

    it("Should detect if item id is incorrect", () => {
        expect(wrapper.find("h3").at(0).text()).not.toBe("1");
    });

    it("Should display item category correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(0).text()).toBe(
            testItem.category
        );
    });

    it("Should detect if item category is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(0).text()
        ).not.toBe("Silungur");
    });

    it("Should display item service correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(3).text()).toBe(
            testItem.service
        );
    });

    it("Should detect if item service is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(3).text()
        ).not.toBe("Taðreyking");
    });

    it("Should display item current state correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(12).text()).toBe(
            testItem.state
        );
    });

    it("Should detect if item current state is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(12).text()
        ).not.toBe("Afgreitt");
    });

    it("Should display item filleted correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(6).text()).toBe(
            "Óflakað"
        );
    });

    it("Should detect if item filleted is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(6).text()
        ).not.toBe("Flakað");
    });

    it("Should display item sliced correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(9).text()).toBe(
            "Heilt Flak"
        );
    });

    it("Should detect if item sliced is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(9).text()
        ).not.toBe("Bitar");
    });

    it("Should display item barcode correctly", () => {
        expect(wrapper.find(".details-item-barcode-cell").at(0).text()).toBe(
            "Strikamerki: " + testItem.barcode
        );
    });

    it("Should detect if item barcode is incorrect", () => {
        expect(
            wrapper.find(".details-item-barcode-cell").at(0).text()
        ).not.toBe("200");
    });

    it("Should display item details correctly", () => {
        expect(wrapper.find(".details-item-content-cell").at(15).text()).toBe(
            testItem.details
        );
    });

    it("Should detect if item details is incorrect", () => {
        expect(
            wrapper.find(".details-item-content-cell").at(15).text()
        ).not.toBe("Skóstærð: 11");
    });

    it("Should display item orderId correctly", () => {
        expect(wrapper.find("Link").props().children[1]).toBe(testItem.orderId);
    });

    it("Should detect if item orderId is incorrect", () => {
        expect(wrapper.find("Link").props().children[1]).not.toBe(8);
    });
});
