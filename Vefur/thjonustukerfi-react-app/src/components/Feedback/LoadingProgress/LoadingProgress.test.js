import React from "react";
import { shallow, mount } from "enzyme";
import LoadingProgress from "./LoadingProgress";

describe("<LoadingProgress />", () => {
    let wrapper;

    describe("LoadingProgress component renders properly when loading", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<LoadingProgress loading={true} size="small" />).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
        it("should have size property with the value small", () => {
            expect(wrapper.props().size).toEqual("small");
        });
        it("should not have size property with the value large", () => {
            expect(wrapper.props().size).not.toEqual("large");
        });
    });

    describe("LoadingProgress component renders properly when not loading", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(<LoadingProgress loading={false} size="small" />).get(0)
            );
        });
        afterEach(() => {
            jest.clearAllMocks();
        });

        it("should contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).toBeNull;
        });
    });
});
