import React from "react";
import { shallow, mount } from "enzyme";
import ForceDeleteCustomerAction from "./ForceDeleteCustomerAction";

describe("<ForceDeleteCustomerACtion />", () => {
    let wrapper;
    const fn = () => {};

    describe("ForceDeleteCustomerAction component renders properly when open", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ForceDeleteCustomerAction
                        open={true}
                        handleClose={fn}
                        handleDelete={fn}
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
        it("should contain b HTML tag", () => {
            const bTag = wrapper.find("b");
            expect(bTag).not.toBeNull;
        });
        it("should contain a button", () => {
            const button = wrapper.find("button");
            expect(button).not.toBeNull;
        });
        it("should contain h6 HTML tag", () => {
            const h6Tag = wrapper.find("h6");
            expect(h6Tag).not.toBeNull;
        });
        it("should contain h3 HTML tag", () => {
            const h3Tag = wrapper.find("h3");
            expect(h3Tag).not.toBeNull;
        });
    });

    describe("ForceDeleteCustomerAction component renders properly when closed", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ForceDeleteCustomerAction
                        open={false}
                        handleClose={fn}
                        handleDelete={fn}
                    />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain b HTML tag", () => {
            const bTag = wrapper.find("b");
            expect(bTag).toBeNull;
        });
        it("should not contain a button", () => {
            const button = wrapper.find("button");
            expect(button).toBeNull;
        });
        it("should not contain h6 HTML tag", () => {
            const h6Tag = wrapper.find("h6");
            expect(h6Tag).toBeNull;
        });
        it("should not contain h3 HTML tag", () => {
            const h3Tag = wrapper.find("h3");
            expect(h3Tag).toBeNull;
        });
    });

    describe("ForceDeleteCustomerAction should have working handlers", () => {
        const mockHandleClose = jest.fn();
        const mockHandleDelete = jest.fn();

        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ForceDeleteCustomerAction
                        open={true}
                        handleClose={mockHandleClose}
                        handleDelete={mockHandleDelete}
                    />
                ).get(0)
            );
        });

        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should detect handleClose when Hætta við is clicked", () => {
            const button = wrapper.find("button").at(0);
            button.simulate("click");
            expect(mockHandleClose).toBeCalled();
        });
        it("should not detect handleDelete when Hætta við is clicked", () => {
            const button = wrapper.find("button").at(0);
            button.simulate("click");
            expect(mockHandleDelete).not.toBeCalled();
        });
        it("should detect handleDelete when Eyða is clicked", () => {
            const button = wrapper.find("button").at(1);
            button.simulate("click");
            expect(mockHandleDelete).toBeCalled();
        });
    });
});
