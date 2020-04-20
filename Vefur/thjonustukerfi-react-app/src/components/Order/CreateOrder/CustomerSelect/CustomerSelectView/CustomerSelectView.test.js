import React from "react";
import { shallow, mount } from "enzyme";
import CustomerSelectView from "./CustomerSelectView";
import useGetAllCustomers from "../../../../../hooks/useGetAllCustomers";
import useSearchBar from "../../../../../hooks/useSearchBar";
jest.mock("../../../../../hooks/useGetAllCustomers");
jest.mock("../../../../../hooks/useSearchBar");
jest.mock("react-router-dom");

describe("<CustomerSelectView />", () => {
    let wrapper;
    let testProps = [
        {
            id: 1,
            name: "Viktor Sveinsson",
        },
        {
            id: 2,
            name: "Árni Magnússon",
        },
        {
            id: 3,
            name: "Kári Stefánsson",
        },
    ];

    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    afterEach(() => {
        jest.clearAllMocks();
    });

    beforeEach(() => {
        wrapper = mount(
            shallow(<CustomerSelectView addCustomer={() => {}} />).get(0)
        );
    });

    useGetAllCustomers.mockReturnValue({
        customers: testProps,
        errror: null,
        isLoading: false,
    });

    useSearchBar.mockReturnValue({ searchResults: testProps });

    it("Should have 3 items", () => {
        expect(wrapper.find("div.customer-select-list-item").length).toBe(3);
    });
});