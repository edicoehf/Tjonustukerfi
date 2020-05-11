import React from "react";
import { shallow, mount } from "enzyme";
import SuccessToaster from "./SuccessToaster";

describe("<SuccessToaster />", () => {
    let wrapper;
    const fn = () => {};
    describe("SuccessToaster component renders properly", () => {
        beforeEach(() => {
            wrapper = mount(
                shallow(
                    <SuccessToaster
                        success={true}
                        receivedSuccess={fn}
                        message="success"
                        cb={fn}
                        cbText="wow"
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
        it("should contain div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });
});
