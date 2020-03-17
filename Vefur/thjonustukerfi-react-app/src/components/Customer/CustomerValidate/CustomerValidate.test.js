import validateForm from "./CustomerValidate";

describe("Validate form", () => {
    var testObject = {};
    var errorMsg = "";

    // Validating empty form
    describe("Validate empty form", () => {
        it("should contain 2 errors on no inputs", () => {
            testObject = {
                name: "",
                email: ""
            };
            expect(Object.keys(validateForm(testObject))).toHaveLength(2);
        });
    });

    // Validating name
    describe("Validate Name", () => {
        beforeEach(() => {
            // Object with valid email address
            testObject = {
                name: "",
                email: "siggi@siggi.is"
            };
        });

        it("should contain 1 error if name is missing", () => {
            expect(Object.keys(validateForm(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if name is given", () => {
            testObject.name = "Olafur";
            expect(Object.keys(validateForm(testObject))).toHaveLength(0);
        });
        it("should have the error message Nafn vantar when name is missing", () => {
            errorMsg = "Nafn vantar";
            expect(validateForm(testObject).name).toEqual(errorMsg);
        });

        it("should contain 1 error if name is more than 100 characters", () => {
            testObject.name =
                "SigurdurSighvaturHalfdanarValdimarAslaugarGudlaugsogIngibjarnaBjarnarsonProppeKaldalonMeggBreidfjordSigmars";
            expect(Object.keys(validateForm(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if name is 99 characters", () => {
            testObject.name =
                "SigurdurSighvaturHalfdanarValdimarAslaugarogIngibjarnaBjarnarsonProppeKaldalonMeggBreidfjordSigmars";
            expect(Object.keys(validateForm(testObject))).toHaveLength(0);
        });
        it("should have the error message Nafn verður að vera minna en 100 stafir if name is more than 100 characters", () => {
            testObject.name =
                "SigurdurSighvaturHalfdanarValdimarAslaugarGudlaugsogIngibjarnaBjarnarsonProppeKaldalonMeggBreidfjordSigmars";
            errorMsg = "Nafn verður að vera minna en 100 stafir";
            expect(validateForm(testObject).name).toEqual(errorMsg);
        });
    });

    // Validating email
    describe("Validate Email", () => {
        beforeEach(() => {
            // Object with valid name
            testObject = {
                name: "Olafur",
                email: ""
            };
        });

        it("should contain 1 error if email is missing", () => {
            expect(Object.keys(validateForm(testObject))).toHaveLength(1);
        });
        it("should contain 0 errors if valid email is given", () => {
            testObject.email = "siggi@oli.is";
            expect(Object.keys(validateForm(testObject))).toHaveLength(0);
        });
        it("should have the error message Netfang vantar when email is missing", () => {
            errorMsg = "Netfang vantar";
            expect(validateForm(testObject).email).toEqual(errorMsg);
        });

        it("should contain 1 error if email address does not include @ symbol", () => {
            testObject.email = "siggi(at)oli.is";
            expect(Object.keys(validateForm(testObject))).toHaveLength(1);
        });
        it("should contain 1 error if email does not end with .x", () => {
            testObject.email = "siggi@oliis";
            expect(Object.keys(validateForm(testObject))).toHaveLength(1);
        });
        it("should have the error message Ógilt netfang when email is missing", () => {
            testObject.email = "siggi(at)oli.is";
            errorMsg = "Ógilt netfang";
            expect(validateForm(testObject).email).toEqual(errorMsg);
        });
    });
});
