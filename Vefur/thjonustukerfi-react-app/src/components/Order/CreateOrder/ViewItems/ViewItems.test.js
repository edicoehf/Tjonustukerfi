import React from "react";
import { shallow, mount } from "enzyme";
import ViewItems from "./ViewItems";
import { TableBody } from "@material-ui/core";

describe("<ViewItems />", () => {
    let wrapper;
    let testProps = [
        {
            id: 1,
            category: 1,
            service: 1,
            categoryName: "Lax",
            serviceName: "Birkireyking",
            amount: 1,
        },
        {
            id: 2,
            category: 2,
            service: 1,
            categoryName: "Silungur",
            serviceName: "Birkireyking",
            amount: 4,
        },
        {
            id: 3,
            category: 1,
            service: 2,
            categoryName: "Lax",
            serviceName: "Taðreyking",
            amount: 2,
        },
    ];

    beforeEach(() => {
        wrapper = mount(
            shallow(<ViewItems items={testProps} remove={() => {}} />).get(0)
        );
    });

    it("Should have 3 rows", () => {
        expect(wrapper.find(TableBody).props().children.length).toBe(3);
    });

    // Test categories
    it("Should display category of 1st row correctly", () => {
        expect(wrapper.find("tr.order-row").at(0).find("td").at(0).text()).toBe(
            testProps[0].categoryName
        );
    });

    it("Should display category of 2nd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(1).find("td").at(0).text()).toBe(
            testProps[1].categoryName
        );
    });

    it("Should display category of 3rd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(2).find("td").at(0).text()).toBe(
            testProps[2].categoryName
        );
    });

    // Test services
    it("Should display service of 1st row correctly", () => {
        expect(wrapper.find("tr.order-row").at(0).find("td").at(1).text()).toBe(
            testProps[0].serviceName
        );
    });

    it("Should display service of 2nd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(1).find("td").at(1).text()).toBe(
            testProps[1].serviceName
        );
    });

    it("Should display service of 3rd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(2).find("td").at(1).text()).toBe(
            testProps[2].serviceName
        );
    });

    // Test amount
    it("Should display amount of 1st row correctly", () => {
        expect(wrapper.find("tr.order-row").at(0).find("td").at(2).text()).toBe(
            testProps[0].amount.toString()
        );
    });

    it("Should display amount of 2nd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(1).find("td").at(2).text()).toBe(
            testProps[1].amount.toString()
        );
    });

    it("Should display amount of 3rd row correctly", () => {
        expect(wrapper.find("tr.order-row").at(2).find("td").at(2).text()).toBe(
            testProps[2].amount.toString()
        );
    });
});
