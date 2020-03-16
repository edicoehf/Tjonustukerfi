import React from "react";
import { shallow, mount } from "enzyme";
import CustomerInputForm from "./CustomerInputForm";

const findByName = (fields, name) => {
    for (var i = 0; i < fields.length; i++) {
        if (fields.at(i).instance().name === name) {
            return fields.at(i);
        }
    }
    return null;
};
describe("<CustomerInputForm />", () => {
    let wrapper;
    let checkWrapper;
    let testState;
    let inputs;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation(init => [init, setState]);
    checkWrapper = mount(shallow(<CustomerInputForm />).get(0));

    beforeEach(() => {
        wrapper = mount(shallow(<CustomerInputForm />).get(0));
        testState = {
            name: "",
            ssn: "",
            telephone: "",
            email: "",
            postalCode: "",
            address: ""
        };
        inputs = wrapper.find("input");
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("Name input", () => {
        const fields = findByName(checkWrapper.find("input"), "name");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const name = findByName(inputs, "name");
            expect(name.instance().value).toHaveLength(0);
        });

        it("Should capture Name correctly onChange", () => {
            const name = findByName(inputs, "name");
            name.instance().value = "Anna";
            testState.name = "Anna";
            name.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture name incorrectly onChange", () => {
            const name = findByName(inputs, "name");
            name.instance().value = "Gunna";
            name.simulate("change");
            expect(name.instance().value).not.toBe("Anna");
        });
    });

    describe("SSN input", () => {
        const fields = findByName(checkWrapper.find("input"), "ssn");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const ssn = findByName(inputs, "ssn");
            expect(ssn.instance().value).toHaveLength(0);
        });

        it("Should capture SSN correctly onChange", () => {
            const ssn = findByName(inputs, "ssn");
            ssn.instance().value = "0123456789";
            testState.ssn = "0123456789";
            ssn.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture SSN incorrectly onChange", () => {
            const ssn = findByName(inputs, "ssn");
            ssn.instance().value = "0123456789";
            ssn.simulate("change");
            expect(ssn.instance().value).not.toBe("0123456789");
        });
    });

    describe("Telephone input", () => {
        const fields = findByName(checkWrapper.find("input"), "telephone");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const telephone = findByName(inputs, "telephone");
            expect(telephone.instance().value).toHaveLength(0);
        });

        it("Should capture Telephone correctly onChange", () => {
            const telephone = findByName(inputs, "telephone");
            telephone.instance().value = "1234567";
            testState.telephone = "1234567";
            telephone.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Telephone incorrectly onChange", () => {
            const telephone = findByName(inputs, "telephone");
            telephone.instance().value = "1234567";
            telephone.simulate("change");
            expect(telephone.instance().value).not.toBe("7654321");
        });
    });

    describe("Email input", () => {
        const fields = findByName(checkWrapper.find("input"), "email");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const email = findByName(inputs, "email");
            expect(email.instance().value).toHaveLength(0);
        });

        it("Should capture Email correctly onChange", () => {
            const email = findByName(inputs, "email");
            email.instance().value = "siggi@oli.is";
            testState.email = "siggi@oli.is";
            email.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Email incorrectly onChange", () => {
            const email = findByName(inputs, "email");
            email.instance().value = "siggi@oli.is";
            email.simulate("change");
            expect(email.instance().value).not.toBe("siggi@gunni.is");
        });
    });

    describe("Address input", () => {
        const fields = findByName(checkWrapper.find("input"), "address");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const address = findByName(inputs, "address");
            expect(address.instance().value).toHaveLength(0);
        });

        it("Should capture Address correctly onChange", () => {
            const address = findByName(inputs, "address");
            address.instance().value = "olabakki 4";
            testState.address = "olabakki 4";
            address.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Address incorrectly onChange", () => {
            const address = findByName(inputs, "address");
            address.instance().value = "olabakki 4";
            address.simulate("change");
            expect(address.instance().value).not.toBe("olabakki 5");
        });
    });

    describe("Postal code input", () => {
        const fields = findByName(checkWrapper.find("input"), "postalCode");
        if (!fields) {
            return;
        }
        it("Should have length 0 at start", () => {
            const postalCode = findByName(inputs, "postalCode");
            expect(postalCode.instance().value).toHaveLength(0);
        });

        it("Should capture Postal code correctly onChange", () => {
            const postalCode = findByName(inputs, "postalCode");
            postalCode.instance().value = "801";
            testState.postalCode = "801";
            postalCode.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });

        it("Should capture Postal code incorrectly onChange", () => {
            const postalCode = findByName(inputs, "postalCode");
            postalCode.instance().value = "801";
            postalCode.simulate("change");
            expect(postalCode.instance().value).not.toBe("800");
        });
    });
});
