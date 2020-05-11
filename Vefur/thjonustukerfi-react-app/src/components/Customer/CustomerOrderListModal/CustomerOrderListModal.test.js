import React from "react";
import { shallow, mount } from "enzyme";
import CustomerOrderListModal from "./CustomerOrderListModal";

describe("<CustomerOrderListModal />", () => {
    let wrapper;
    const fn = () => {};

    describe("CustomerOrderListModal component renders properly when open", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerOrderListModal
                        customerId={1}
                        open={true}
                        handleClose={fn}
                    />
                ).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain button", () => {
            const button = wrapper.find("button");
            expect(button).not.toBeNull;
        });
        it("should contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });

    describe("CustomerOrderListModal component renders properly when closed", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerOrderListModal
                        customerId={1}
                        open={false}
                        handleClose={fn}
                    />
                ).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain button", () => {
            const button = wrapper.find("button");
            expect(button).toBeNull;
        });
        it("should not contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).toBeNull;
        });
    });

    describe("CustomerOrderListModal should have working handleClose function", () => {
        const mockHandleClose = jest.fn();
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerOrderListModal
                        customerId={1}
                        open={true}
                        handleClose={mockHandleClose}
                    />
                ).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should detect handleClose when Loka is clicked", () => {
            const button = wrapper.find("button");
            button.simulate("click");
            expect(mockHandleClose).toBeCalled();
        });
        it("should not detect handleclose when Loka is not clicked", () => {
            expect(mockHandleClose).not.toBeCalled();
        });
    });
});
