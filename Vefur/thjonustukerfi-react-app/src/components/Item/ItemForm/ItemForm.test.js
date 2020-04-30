import React from "react";
import { shallow, mount } from "enzyme";
import { fireEvent } from "@testing-library/react";
import ItemForm from "./ItemForm";
import { RadioGroup, TextField } from "@material-ui/core";

const findByName = (fields, name) => {
    for (var i = 0; i < fields.length; i++) {
        if (fields.at(i).props().name === name) {
            return fields.at(i);
        }
    }
    return null;
};

describe("<ItemForm />", () => {
    const categories = [
        {
            id: 0,
            name: "silungur",
        },
        {
            id: 1,
            name: "lax",
        },
        {
            id: 2,
            name: "thorskur",
        },
    ];
    const services = [
        {
            id: 0,
            name: "birkireyking",
        },
        {
            id: 1,
            name: "tadreyking",
        },
        {
            id: 2,
            name: "graf",
        },
    ];

    let wrapper;
    let checkWrapper;
    let testState;
    let radios;
    let textfields;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    const handler = () => {};
    useStateSpy.mockImplementation((init) => [init, setState]);
    checkWrapper = mount(
        shallow(
            <ItemForm
                submitHandler={handler}
                categories={categories}
                services={services}
            />
        ).get(0)
    );

    beforeEach(() => {
        wrapper = mount(
            shallow(
                <ItemForm
                    submitHandler={handler}
                    categories={categories}
                    services={services}
                />
            ).get(0)
        );
        testState = {
            category: null,
            service: null,
            amount: 1,
            sliced: "",
            filleted: "",
            details: "",
            otherCategory: "",
            otherService: "",
            categories: null,
            services: null,
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

    describe("Filleted select", () => {
        it("Should be empty string at start", () => {
            const fill = findByName(radios, "filleted");
            expect(fill.props().value).toBe("");
        });

        it("Should capture filleted correctly onChange", () => {
            const fill = findByName(radios, "filleted");
            fill.props().onChange({
                target: { name: "filleted", value: "Flakað" },
            });
            testState.filleted = "Flakað";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture filleted incorrectly onChange", () => {
            const fill = findByName(radios, "filleted");
            fill.props().onChange({
                target: { name: "filleted", value: "Flakað" },
            });
            expect(fill.props().value).not.toBe("Óflakað");
        });
    });

    describe("Sliced select", () => {
        it("Should be empty string at start", () => {
            const sliced = findByName(radios, "sliced");
            expect(sliced.props().value).toBe("");
        });

        it("Should capture sliced correctly onChange", () => {
            const sliced = findByName(radios, "sliced");
            sliced.props().onChange({
                target: { name: "sliced", value: "Bitar" },
            });
            testState.sliced = "Bitar";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture sliced incorrectly onChange", () => {
            const sliced = findByName(radios, "sliced");
            sliced.props().onChange({
                target: { name: "sliced", value: "Bitar" },
            });
            expect(sliced.props().value).not.toBe("Heilt Flak");
        });
    });

    describe("Details select", () => {
        it("Should be empty string at start", () => {
            const det = findByName(textfields, "details");
            expect(det.props().value).toBe("");
        });

        it("Should capture details correctly onChange", () => {
            const det = findByName(textfields, "details");
            det.props().onChange({
                target: { name: "details", value: "4 bitar" },
            });
            testState.details = "4 bitar";
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture details incorrectly onChange", () => {
            const det = findByName(textfields, "details");
            det.props().onChange({
                target: { name: "details", value: "4 bitar" },
            });
            expect(det.props().value).not.toBe("3 bitar");
        });
    });
});
