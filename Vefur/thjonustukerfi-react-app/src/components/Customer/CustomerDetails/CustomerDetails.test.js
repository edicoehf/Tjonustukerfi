import React from "react";
import { shallow, mount } from "enzyme";
import CustomerDetails from "./CustomerDetails";
import useCustomerService from "../../../hooks/useCustomerService";
jest.mock("../../../hooks/useCustomerService");
jest.mock("react-router-dom");

const findByName = (fields, name) => {
    for (var i = 0; i < fields.length; i++) {
        if (fields.at(i).instance().title === name) {
            return fields.at(i);
        }
    }
    return null;
};

describe("<CustomerDetails />", () => {
    let wrapper;
    let testCustomer;
    let rows;
    let error;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation(init => [init, setState]);

    beforeEach(() => {
        testCustomer = {
            name: "",
            ssn: "",
            telephone: "",
            email: "",
            postalCode: "",
            address: ""
        };
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("Displays rows", () => {
        it("Should not render any rows before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find("tr");
            expect(fields.length).toBe(0);
        });

        it("Should render 2 rows after fetch with 2 set values", () => {
            testCustomer.name = "Siggi Viggi";
            testCustomer.email = "siggi@viggi.is";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find("tr");
            expect(fields.length).toBe(2);
        });
    });

    describe("Display error", () => {
        it("Should not render error when fetch is successful", () => {
            testCustomer.name = "Siggi Viggi";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find(".error");
            expect(fields.length).toBe(0);
        });

        it("Should  render error when fetch is unsuccessful", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: "FAILED"
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find(".error");
            expect(fields.length).toBe(1);
        });
    });

    describe("Name row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.name = "Viggi Siggi";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields.childAt(1).text()).toBe(testCustomer.name);
        });
    });

    describe("SSN row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.ssn = "1304873579";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields.childAt(1).text()).toBe(testCustomer.ssn);
        });
    });

    describe("Telephone row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "telephone");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "telephone");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.telephone = "5812345";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "telephone");
            expect(fields.childAt(1).text()).toBe(testCustomer.telephone);
        });
    });

    describe("Email row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.email = "siggi@viggi.is";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            expect(fields.childAt(1).text()).toBe(testCustomer.email);
        });
    });

    describe("Address row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.address = "Bakkabakki 2";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields.childAt(1).text()).toBe(testCustomer.address);
        });
    });

    describe("Postalcode row", () => {
        it("Should not render before fetch", () => {
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.postalCode = "800";
            useCustomerService.mockReturnValue({
                customer: testCustomer,
                error: null
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields.childAt(1).text()).toBe(testCustomer.postalCode);
        });
    });
});