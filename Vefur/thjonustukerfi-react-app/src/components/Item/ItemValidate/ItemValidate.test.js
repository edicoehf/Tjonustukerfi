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
                sliced: "whole",
                filleted: "filleted",
                details: "details",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
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
                sliced: "whole",
                filleted: "filleted",
                details: "details",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
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
                sliced: "whole",
                filleted: "filleted",
                details: "details",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
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

    // Validating sliced
    describe("Validate sliced", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: "1",
                sliced: "",
                filleted: "filleted",
                details: "details",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
            };
        });

        it("should contain 1 error if sliced is missing", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if amount is given", () => {
            testObject.sliced = "Bitar";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
        it("should have the error message Tilgreining á pökkun vantar when sliced is missing", () => {
            errorMsg = "Tilgreining á pökkun vantar";
            expect(itemValidate(testObject).sliced).toEqual(errorMsg);
        });
    });

    // Validating filleted
    describe("Validate filleted", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: "1",
                sliced: "bitar",
                filleted: "",
                details: "details",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
            };
        });

        it("should contain 1 error if filleted is missing", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if fillted is given", () => {
            testObject.filleted = "Flakað";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
        it("should have the error message Tilgreining á flökun vantar when filleted is missing", () => {
            errorMsg = "Tilgreining á flökun vantar";
            expect(itemValidate(testObject).filleted).toEqual(errorMsg);
        });
    });

    // Validating details
    describe("Validate details", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: "1",
                sliced: "bitar",
                filleted: "flakað",
                details: "",
                otherCategory: "lol",
                otherService: "lol",
                categories: [{}],
                services: [{}],
            };
        });

        it("should contain 0 errors if details is not given", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });

        it("should contain 0 errors if details is given", () => {
            testObject.details = "14 bitar";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });

        it("should have the error message Annað má aðeins vera 100 stafir when details is 251 characters", () => {
            errorMsg = "Annað má aðeins vera 100 stafir";
            testObject.details =
                "ekki skera i bita nema þetta hafi komið óflakað, ef þetta kom flakað þá má skera í bita nema þetta hafi verið síða þá verður að skera í síður. Passa verður að hafa ekki til mat í hádeginu fyrir starfsmenn ef þeir eru svangir. Við viljum ekki fita stm.";
            expect(itemValidate(testObject).details).toEqual(errorMsg);
        });

        it("should contain 0 errors if details is 100 characters", () => {
            testObject.details =
                "ekki skera i bita nema þetta hafi komið óflakað, ef þetta kom flakað þá má skera í bita nema þetta..";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
    });

    // Validating otherCategory
    // "Annað" is the last category in every category list so we check if
    // category is the same as the length of our
    // category list
    describe("Validate otherCategory", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: "1",
                sliced: "bitar",
                filleted: "flakað",
                details: "",
                otherCategory: "",
                otherService: "lol",
                categories: [{ some: "lol" }, { some: "lol" }],
                services: [{}],
            };
        });

        it("should contain 0 errors if otherCategory is not given when category is not Annað", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });

        it("should contain 1 errors if otherCategory is not given when category is Annað", () => {
            testObject.category = "2";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });

        it("should have the error message Vantar hvaða tegund vantar when otherCategory is missing", () => {
            errorMsg = "Vantar hvaða tegund";
            testObject.category = "2";
            expect(itemValidate(testObject).otherCategory).toEqual(errorMsg);
        });

        it("should have the 0 errors if category is Annað and otherCategory is given", () => {
            errorMsg = "Vantar hvaða tegund";
            testObject.category = "2";
            testObject.otherCategory = "Karfi";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
    });

    // Validating otherService
    // "Annað" is the last service in every service list so we check if
    // service is the same as the length of our
    // service list
    describe("Validate otherService", () => {
        beforeEach(() => {
            testObject = {
                category: 1,
                service: 1,
                amount: "1",
                sliced: "bitar",
                filleted: "flakað",
                details: "",
                otherCategory: "lol",
                otherService: "",
                services: [{ some: "lol" }, { some: "lol" }],
                categories: [{}],
            };
        });

        it("should contain 0 errors if otherService is not given when service is not Annað", () => {
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });

        it("should contain 1 errors if otherService is not given when category is Annað", () => {
            testObject.service = "2";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(1);
        });

        it("should have the error message Vantar hvaða þjónustu vantar when otherService is missing", () => {
            errorMsg = "Vantar hvaða þjónustu";
            testObject.service = "2";
            expect(itemValidate(testObject).otherService).toEqual(errorMsg);
        });

        it("should have the 0 errors if service is Annað and otherService is given", () => {
            errorMsg = "Vantar hvaða tegund";
            testObject.service = "2";
            testObject.otherService = "Elda";
            expect(Object.keys(itemValidate(testObject))).toHaveLength(0);
        });
    });
});
