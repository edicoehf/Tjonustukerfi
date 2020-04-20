import React from "react";
import { shallow, mount } from "enzyme";
import OrderItem from "./OrderItem";

describe("<OrderItem />", () => {
    let wrapper;
    let testProps = {
        id: 52,
        category: "Lax",
        service: "Birkireyking",
        barcode: "50050001",
    };

    beforeEach(() => {
        wrapper = mount(
            shallow(
                <OrderItem
                    key={testProps.id}
                    category={testProps.category}
                    service={testProps.service}
                    barcode={testProps.barcode}
                />
            ).get(0)
        );
    });

    it("Should have 3 children", () => {
        expect(
            wrapper.find(".order-item").at(0).instance().children.length
        ).toBe(3);
    });

    it("Should display category correctly", () => {
        expect(
            wrapper.find(".order-item-category").at(0).childAt(0).text()
        ).toBe(testProps.category);
    });

    it("Should display service correctly", () => {
        expect(
            wrapper.find(".order-item-service").at(0).childAt(0).text()
        ).toBe(testProps.service);
    });

    it("Should display barcode correctly", () => {
        expect(
            wrapper.find(".order-item-barcode").at(0).childAt(0).text()
        ).toBe(testProps.barcode);
    });
});
