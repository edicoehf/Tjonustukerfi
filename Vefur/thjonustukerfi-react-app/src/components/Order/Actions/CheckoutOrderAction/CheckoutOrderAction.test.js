import React from "react";
import { shallow, mount } from "enzyme";
import CheckoutOrderAction from "./CheckoutOrderAction";

describe("<CheckoutOrderAction />", () => {
    let wrapper;
    const fn = () => {};
    beforeEach(() => {
        wrapper = mount(
            shallow(<CheckoutOrderAction id={1} hasUpdated={fn} />).get(0)
        );
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("CheckoutOrderAction component renders properly", () => {
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
