import React from "react";
import { shallow, mount } from "enzyme";
import OrderItem from "./OrderItem";

describe("<OrderItem />", () => {
    let wrapper;
    let testProps = {
        id: 52,
        type: "Lax",
        service: "Birkireyking",
        barcode: "50050001",
    };

    beforeEach(() => {
        wrapper = mount(
            shallow(
                <OrderItem
                    key={testProps.id}
                    type={testProps.type}
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

    it("Should display type correctly", () => {
        expect(wrapper.find(".order-item-type").at(0).childAt(0).text()).toBe(
            testProps.type
        );
    });

    it("Should service type correctly", () => {
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
