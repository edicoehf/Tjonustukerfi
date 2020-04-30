import React from "react";
import { shallow, mount } from "enzyme";
import OrderList from "./OrderList";
jest.mock("react-router-dom");

describe("<OrderList />", () => {
    let wrapper;
    const testOrder1 = {
        id: "1",
        customer: "arni mar",
        customerId: 1,
        barcode: "20200001",
        items: [
            {
                id: 1,
                category: "Test 4",
                service: "Birkireyking",
                barcode: "50500001",
            },
            {
                id: 2,
                category: "Test 4",
                service: "Birkireyking",
                barcode: "50500002",
            },
        ],
    };
    const testOrder2 = {
        id: "2",
        customer: "Bjarni Olafur",
        customerId: 2,
        barcode: "20200002",
        items: [
            {
                id: 1,
                category: "Test 3",
                service: "Birkireyking",
                barcode: "50500004",
            },
            {
                id: 2,
                category: "Test 4",
                service: "Birkireyking",
                barcode: "50500005",
            },
        ],
    };

    beforeEach(() => {
        wrapper = mount(
            shallow(
                <OrderList
                    orders={[testOrder1, testOrder2]}
                    isLoading={false}
                    error={null}
                />
            ).get(0)
        );
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    describe("OrderList component renders properly", () => {
        it("should not be null", () => {
            expect(wrapper).not.toBeNull;
        });

        it("should not contain p HTML tag", () => {
            const pTag = wrapper.find("p");
            expect(pTag).toBeNull;
        });

        it("should contain Pöntunarnúmer", () => {
            expect(wrapper.contains("Pöntunarnúmer")).toEqual(true);
        });

        it("should contain Viðskiptavinur", () => {
            expect(wrapper.contains("Viðskiptavinur")).toEqual(true);
        });

        it("should contain Dagsetning", () => {
            expect(wrapper.contains("Dagsetninga")).toEqual(true);
        });
    });
});
