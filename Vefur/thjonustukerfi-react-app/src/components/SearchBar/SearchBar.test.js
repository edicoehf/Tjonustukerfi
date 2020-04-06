import React from "react";
import { shallow, mount } from "enzyme";
import SearchBar from "./SearchBar";

describe("<SearchBar />", () => {
    let wrapper;
    let shallowWrapper;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    const handler = event => {
        setState(event.target.value);
    };
    useStateSpy.mockImplementation(init => [init, setState]);

    beforeEach(() => {
        wrapper = mount(
            <SearchBar
                searchTerm={"Oli"}
                handleChange={handler}
                placeHolder={"Hallo"}
            />
        );
        shallowWrapper = mount(
            shallow(
                <SearchBar
                    searchTerm={"Oli"}
                    handleChange={handler}
                    placeHolder={"Hallo"}
                />
            ).get(0)
        );
    });

    describe("Searchbar should render properly", () => {
        it("Should not be null on render", () => {
            expect(wrapper).not.toBeNull;
        });
    });

    describe("Searchbar has proper working searchTerm", () => {
        it("should have searchTerm", () => {
            expect(wrapper.props().searchTerm).not.toBeNull;
        });
        it("should have correct value of Oli", () => {
            expect(wrapper.props().searchTerm).toEqual("Oli");
        });
        it("should not have incorrect value of Hallo", () => {
            expect(wrapper.props().searchTerm).not.toEqual("Hallo");
        });
    });

    describe("Searchbar has proper working placeholder", () => {
        it("should have a placeholder", () => {
            expect(wrapper.props().placeHolder).not.toBeNull;
        });

        it("should have correct value of Hallo", () => {
            expect(wrapper.props().placeHolder).toEqual("Hallo");
        });

        it("should not have incorrect value of Oli", () => {
            expect(wrapper.props().placeHolder).not.toEqual("Oli");
        });
    });

    describe("Searchbar has correct working searchTermState", () => {
        it("should capture empty string", () => {
            const searchBar = shallowWrapper.find("input");
            searchBar.simulate("change", { target: { value: "" } });
            expect(setState).toHaveBeenCalledWith("");
        });

        it("should capture incorrect change", () => {
            const searchBar = shallowWrapper.find("input");
            searchBar.simulate("change", { target: { value: "Arni" } });
            expect(setState).not.toHaveBeenCalledWith("Maggi");
        });

        it("should correctly change on input", () => {
            const searchBar = shallowWrapper.find("input");
            searchBar.simulate("change", { target: { value: "A" } });
            expect(setState).toHaveBeenCalledWith("A");
        });

        it("should correctly change on icelandic letters", () => {
            const searchBar = shallowWrapper.find("input");
            searchBar.simulate("change", { target: { value: "Árni" } });
            expect(setState).toHaveBeenCalledWith("Árni");
        });
    });
});
