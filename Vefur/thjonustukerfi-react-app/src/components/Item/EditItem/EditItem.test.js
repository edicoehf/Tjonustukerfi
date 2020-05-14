import React from "react";
import { shallow, mount } from "enzyme";
import EditItem from "./EditItem";

describe("<EditItem />", () => {
    let wrapper;
    const mockHistoryProp = {};
    const mockMatchProp = { params: { id: 1 } };
    beforeEach(() => {
        wrapper = mount(
            shallow(
                <EditItem match={mockMatchProp} history={mockHistoryProp} />
            ).get(0)
        );
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("EditItem component renders properly", () => {
        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });
        it("should contain h3 HTML tag", () => {
            const h3Tag = wrapper.find("h3");
            expect(h3Tag).not.toBeNull;
        });
        it("should contain a div HTML tag", () => {
            const divTag = wrapper.find("div");
            expect(divTag).not.toBeNull;
        });
    });
});
