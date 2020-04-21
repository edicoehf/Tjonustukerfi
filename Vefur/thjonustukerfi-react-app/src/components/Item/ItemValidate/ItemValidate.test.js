import itemValidate from "./ItemValidate";

describe("Validate item", () => {
    var testObject = {};
    var errorMsg = "";

    // Validating categories
    describe("Validate Category", () => {
        beforeEach(() => {
            testObject = {
                category: null,
                service: 1,
                amount: 1,
            };
        });

        it("should contain 1 error if category is missing", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if category is given", () => {
            testObject.category = "3";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
        it("should have the error message Tegund vantar when category is missing", () => {
            errorMsg = "Tegund vantar";
            expect(itemValidate(testObject).category).toEqual(errorMsg);
        });
    });

    // Validating services
    describe("Validate Service", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: null,
                amount: 1,
            };
        });

        it("should contain 1 error if service is missing", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if service is given", () => {
            testObject.service = "2";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
        it("should have the error message Þjónustu vantar when service is missing", () => {
            errorMsg = "Þjónustu vantar";
            expect(itemValidate(testObject).service).toEqual(errorMsg);
        });
    });

    // Validating amounts
    describe("Validate Amount", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: null,
            };
        });

        it("should contain 1 error if amount is missing", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if amount is given", () => {
            testObject.amount = 2;
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
        it("should have the error message Fjölda vantar when amount is missing", () => {
            errorMsg = "Fjölda vantar";
            expect(itemValidate(testObject).amount).toEqual(errorMsg);
        });

        it("should contain 1 error if amount is less than 1", () => {
            testObject.amount = 0;
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });

        it("should have the error message Fjöldi verður að vera stærri en 0 when service is less than 1", () => {
            testObject.amount = 0;
            errorMsg = "Fjöldi verður að vera stærri en 0";
            expect(itemValidate(testObject).amount).toEqual(errorMsg);
        });
    });
});
