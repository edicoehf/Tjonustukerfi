import React from "react";
import moment from "moment";
import "moment/locale/is";
import { shallow, mount } from "enzyme";
import PrintItemView from "./PrintItemView";
import useItemPrintDetails from "../../../hooks/useItemPrintDetails";
jest.mock("../../../hooks/useItemPrintDetails");

describe("<PrintItemView />", () => {
    let wrapper;
    const dateFormat = (date) => {
        moment.locale("is");
        return moment(date).format("ll");
    };
    const testItem = {
        id: "3",
        category: "Lax",
        service: "Salt pækill",
        dateCreated: "2020-04-17T22:50:05.146677",
        json: {
            sliced: false,
            filleted: false,
            otherCategory: "",
            otherService: "",
        },
        barcode: "50500001",
        details: "",
        orderId: "7",
    };
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    describe("PrintItemView component renders properly", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<PrintItemView id={1} width={15} height={10} />).get(0)
            );
        });
        useItemPrintDetails.mockReturnValue({
            item: testItem,
            isLoading: true,
            error: null,
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain a button", () => {
            const button = wrapper.find("button");
            expect(button).not.toBeNull;
        });
        it("should contain a div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });

    describe("PrintItemView on loading", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<PrintItemView id={1} width={15} height={10} />).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should be an empty wrapper", () => {
            useItemPrintDetails.mockReturnValue({
                item: testItem,
                isLoading: true,
                error: null,
            });
            expect(wrapper.at(0).props()).toStrictEqual({});
        });
    });

    describe("PrintItemView on not loading", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<PrintItemView id={1} width={15} height={10} />).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not be an empty wrapper", () => {
            useItemPrintDetails.mockReturnValue({
                item: testItem,
                isLoading: false,
                error: null,
            });
            expect(wrapper.at(0).props()).toStrictEqual({});
        });
        it("should have max width of 15", () => {
            expect(wrapper.at(0).children().at(0).props().maxWidth).toBe(15);
        });
        it("should not have max width of 10", () => {
            expect(wrapper.at(0).children().at(0).props().maxWidth).not.toBe(
                10
            );
        });
        it("should have max height of 10", () => {
            expect(wrapper.at(0).children().at(0).props().maxHeight).toBe(10);
        });
        it("should not have max height of 15", () => {
            expect(wrapper.at(0).children().at(0).props().maxHeight).not.toBe(
                15
            );
        });
        it("should have the correct order id", () => {
            expect(
                wrapper.find(".print-order-id").at(0).children().at(1).text()
            ).toBe(testItem.orderId);
        });
        it("should have the correct item id", () => {
            expect(
                wrapper.find(".print-item-id").at(0).children().at(1).text()
            ).toBe(testItem.id);
        });
        it("should have the correct date created", () => {
            expect(
                wrapper
                    .find(".print-date-created")
                    .at(0)
                    .children()
                    .at(1)
                    .text()
            ).toBe(dateFormat(testItem.dateCreated));
        });
        it("should have the correct category", () => {
            expect(
                wrapper
                    .find(".print-item-category")
                    .at(0)
                    .children()
                    .at(1)
                    .text()
            ).toBe(testItem.category);
        });
        it("should have the correct service", () => {
            expect(
                wrapper
                    .find(".print-item-service")
                    .at(0)
                    .children()
                    .at(1)
                    .text()
            ).toBe(testItem.service);
        });
        it("should have the correct filleted", () => {
            expect(
                wrapper
                    .find(".print-item-filleted")
                    .at(0)
                    .children()
                    .at(1)
                    .text()
            ).toBe(testItem.json.filleted ? "Flakað" : "Óflakað");
        });
        it("should have the correct sliced", () => {
            expect(
                wrapper.find(".print-item-sliced").at(0).children().at(1).text()
            ).toBe(testItem.json.sliced ? "Bitar" : "Heilt Flak");
        });
        it("should have the right barcode", () => {
            expect(
                wrapper.find(".right-line").at(0).children().at(1).text()
            ).toBe(testItem.barcode);
        });
    });
});
