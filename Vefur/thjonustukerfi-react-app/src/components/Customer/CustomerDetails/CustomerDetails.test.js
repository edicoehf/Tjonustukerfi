import React from "react";
import { shallow, mount } from "enzyme";
import CustomerDetails from "./CustomerDetails";
import useGetCustomerById from "../../../hooks/useGetCustomerById";
jest.mock("../../../hooks/useGetCustomerById");
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
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    beforeEach(() => {
        testCustomer = {
            name: "",
            ssn: "",
            phone: "",
            email: "",
            postalCode: "",
            address: "",
        };
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("Displays rows", () => {
        it("Should only render email row before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find("tr");
            expect(fields.length).toBe(1);
        });

        it("Should render 2 rows after fetch with 2 set values", () => {
            testCustomer.name = "Siggi Viggi";
            testCustomer.email = "siggi@viggi.is";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find("tr");
            expect(fields.length).toBe(2);
        });
    });

    describe("Display error", () => {
        it("Should not render error when fetch is successful", () => {
            testCustomer.name = "Siggi Viggi";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find(".error");
            expect(fields.length).toBe(0);
        });

        it("Should  render error when fetch is unsuccessful", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: "FAILED",
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = wrapper.find(".error");
            expect(fields.length).toBe(1);
        });
    });

    describe("Name row", () => {
        it("Should not render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.name = "Viggi Siggi";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "name");
            expect(fields.childAt(1).text()).toBe(testCustomer.name);
        });
    });

    describe("SSN row", () => {
        it("Should not render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.ssn = "1304873579";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "ssn");
            expect(fields.childAt(1).text()).toBe(testCustomer.ssn);
        });
    });

    describe("Telephone row", () => {
        it("Should not render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "telephone");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.email = "email@email.com";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "telephone");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.phone = "5812345";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "phone");
            expect(fields.childAt(1).text()).toBe(testCustomer.phone);
        });
    });

    describe("Email row", () => {
        it("Should render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            expect(fields).not.toBe(null);
        });

        it("Should render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            expect(fields).not.toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.email = "siggi@viggi.is";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "email");
            // TODO: test that this is a TextField!!!
            //expect(fields.childAt(1).text()).toBe(testCustomer.email);
        });
    });

    describe("Address row", () => {
        it("Should not render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.address = "Bakkabakki 2";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "address");
            expect(fields.childAt(1).text()).toBe(testCustomer.address);
        });
    });

    describe("Postalcode row", () => {
        it("Should not render before fetch", () => {
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields).toBe(null);
        });

        it("Should not render if it doesnt have value", () => {
            testCustomer.name = "Siggi Viggi";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields).toBe(null);
        });

        it("Should render correct value when it has value", () => {
            testCustomer.postalCode = "800";
            useGetCustomerById.mockReturnValue({
                customer: testCustomer,
                error: null,
            });
            wrapper = mount(shallow(<CustomerDetails id="2" />).get(0));
            const fields = findByName(wrapper.find("tr"), "postalcode");
            expect(fields.childAt(1).text()).toBe(testCustomer.postalCode);
        });
    });
});
