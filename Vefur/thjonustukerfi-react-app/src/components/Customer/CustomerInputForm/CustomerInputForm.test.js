import React from 'react';
import { shallow, mount } from 'enzyme';
import CustomerInputForm from './CustomerInputForm';
import Input from '../../Input/Input';

describe('<CustomerInputForm />', () => {
    let wrapper;
    let testState;
    const setState = jest.fn();
    const useStateSpy = jest.spyOn(React, "useState");
    useStateSpy.mockImplementation((init) => [init, setState]);

    beforeEach(() => {
        wrapper = mount(shallow(<CustomerInputForm />).get(0));
        testState = {
            name: '',
            ssn: '',
            telephone: '',
            email: '',
            postalCode: '',
            address: ''
        };
    });

    afterEach(() => {
        jest.clearAllMocks();
    });
    
    describe('Name input', () => {
        it('Should capture Name correctly onChange', () => {
            const name = wrapper.find("input").at(0);
            name.instance().value = "test";
            testState.name = "test";
            name.simulate("change");
            expect(setState).toHaveBeenCalledWith(testState);
        });
    });
});