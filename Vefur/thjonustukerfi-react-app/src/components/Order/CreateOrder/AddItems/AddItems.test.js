import React from "react";
import { shallow, mount } from "enzyme";
import { fireEvent } from "@testing-library/react";
import AddItems from "./AddItems";
import { RadioGroup, TextField } from "@material-ui/core";

const findByName = (fields, name) => {
    for (var i = 0; i < fields.length; i++) {
        if (fields.at(i).props().name === name) {
            return fields.at(i);
        }
    }
    return null;
};

describe("<AddItems />", () => {
    let wrapper;
    let checkWrapper;
    let testState;
    let radios;
    let textfields;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    const handler = () => {};
    useStateSpy.mockImplementation((init) => [init, setState]);
    checkWrapper = mount(shallow(<AddItems addItems={handler} />).get(0));

    beforeEach(() => {
        wrapper = mount(shallow(<AddItems addItems={handler} />).get(0));
        testState = {
            category: null,
            service: null,
            amount: 1,
        };
        radios = wrapper.find(RadioGroup);
        textfields = wrapper.find(TextField);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("Category select", () => {
        it("Should be null at start", () => {
            const cat = findByName(radios, "category");
            expect(cat.props().value).toBe(null);
        });

        it("Should capture Category correctly onChange", () => {
            const cat = findByName(radios, "category");
            cat.props().onChange({ target: { name: "category", value: "3" } });
            testState.category = "3";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Category incorrectly onChange", () => {
            const cat = findByName(radios, "category");
            cat.props().onChange({ target: { name: "category", value: "2" } });
            expect(cat.props().value).not.toBe("3");
        });
    });

    describe("Service select", () => {
        it("Should be null at start", () => {
            const serv = findByName(radios, "service");
            expect(serv.props().value).toBe(null);
        });

        it("Should capture Service correctly onChange", () => {
            const serv = findByName(radios, "service");
            serv.props().onChange({ target: { name: "service", value: "3" } });
            testState.service = "3";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Service incorrectly onChange", () => {
            const serv = findByName(radios, "service");
            serv.props().onChange({ target: { name: "service", value: "2" } });
            expect(serv.props().value).not.toBe("3");
        });
    });

    describe("Amount select", () => {
        it("Should be 1 at start", () => {
            const amnt = findByName(textfields, "amount");
            expect(amnt.props().value).toBe(1);
        });

        it("Should capture Amount correctly onChange", () => {
            const amnt = findByName(textfields, "amount");
            amnt.props().onChange({ target: { name: "amount", value: "3" } });
            testState.amount = "3";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Amount incorrectly onChange", () => {
            const amnt = findByName(textfields, "amount");
            amnt.props().onChange({ target: { name: "amount", value: "2" } });
            expect(amnt.props().value).not.toBe("3");
        });
    });
});
