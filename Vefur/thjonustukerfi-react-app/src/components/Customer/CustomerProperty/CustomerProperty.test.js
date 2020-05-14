import React from "react";
import { shallow, mount } from "enzyme";
import CustomerProperty from "./CustomerProperty";

describe("<CustomerProperty />", () => {
    let wrapper;
    const title = "Nafn";
    const name = "Nafn";
    const value = "Baldvin";

    describe("CustomerProperty component renders properly when given value", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerProperty title={title} name={name} value={value} />
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
        it("should contain tr HTML tag", () => {
            const trTag = wrapper.find("tr");
            expect(trTag).not.toBeNull;
        });
        it("should contain td HTML tag", () => {
            const tdTag = wrapper.find("tdTag");
            expect(tdTag).not.toBeNull;
        });
        it("should have the title Nafn", () => {
            const title = wrapper.props().title;
            expect(title).toEqual("Nafn");
        });
        it("should not have the title Heimilisfang", () => {
            const title = wrapper.props().title;
            expect(title).not.toEqual("Heimilisfang");
        });
    });
    describe("CustomerProperty component renders empty when not given value", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <CustomerProperty title={title} name={name} value={null} />
                ).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should contain tr HTML tag", () => {
            const trTag = wrapper.find("tr");
            expect(trTag).toBeNull;
        });
        it("should contain td HTML tag", () => {
            const tdTag = wrapper.find("tdTag");
            expect(tdTag).toBeNull;
        });
    });
});
