import React from "react";
import { shallow, mount } from "enzyme";
import ConfirmationDialog from "./ConfirmationDialog";

describe("<ConfirmationDialog />", () => {
    let wrapper;
    const title = "Viðskiptavinur";
    const description = "Nánar um viðskiptavin";
    const fn = () => {};
    const declineText = "Nei takk";
    const confirmText = "Já takk";

    describe("ConfirmationDialog component renders properly when open", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ConfirmationDialog
                        title={title}
                        description={description}
                        handleAccept={fn}
                        handleClose={fn}
                        open={true}
                        declineText={declineText}
                        confirmText={confirmText}
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

    describe("ConfirmationDialog component renders properly when closed", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ConfirmationDialog
                        title={title}
                        description={description}
                        handleAccept={fn}
                        handleClose={fn}
                        open={false}
                        declineText={declineText}
                        confirmText={confirmText}
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

    describe("ConfirmationDialog should have working handler functions", () => {
        const mockHandleClose = jest.fn();
        const mockHandleAccept = jest.fn();
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <ConfirmationDialog
                        title={title}
                        description={description}
                        handleAccept={mockHandleAccept}
                        handleClose={mockHandleClose}
                        open={true}
                        declineText={declineText}
                        confirmText={confirmText}
                    />
                ).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should detect handleClose when Nei takk is clicked", () => {
            const button = wrapper.find("button").at(0);
            button.simulate("click");
            expect(mockHandleClose).toBeCalled();
        });
        it("should not detect handleclose when Nei takk is not clicked", () => {
            expect(mockHandleClose).not.toBeCalled();
        });
        it("should detect handleAccept when Já takk is clicked", () => {
            const button = wrapper.find("button").at(1);
            button.simulate("click");
            expect(mockHandleAccept).toBeCalled();
        });
        it("should not detect handleAccept when Já takk is not clicked", () => {
            expect(mockHandleAccept).not.toBeCalled();
        });
        it("should not detect handleAccept when Nei takk is clicked", () => {
            const button = wrapper.find("button").at(0);
            button.simulate("click");
            expect(mockHandleAccept).not.toBeCalled();
        });
        it("should not detect handleClose when Já takk is clicked", () => {
            const button = wrapper.find("button").at(1);
            button.simulate("click");
            expect(mockHandleClose).not.toBeCalled();
        });
    });
});
