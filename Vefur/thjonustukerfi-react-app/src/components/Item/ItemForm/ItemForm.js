import React from "react";
import useForm from "../../../hooks/useForm";
import EditIcon from "@material-ui/icons/Edit";
import AddShoppingCartIcon from "@material-ui/icons/AddShoppingCart";
import itemValidate from "../ItemValidate/ItemValidate";
import AddIcon from "@material-ui/icons/Add";
import "./ItemForm.css";
import {
    FormControl,
    FormLabel,
    RadioGroup,
    FormControlLabel,
    Radio,
    TextField,
    Button,
    Paper,
    MobileStepper,
} from "@material-ui/core";
import { useTheme } from "@material-ui/core/styles";
import KeyboardArrowLeft from "@material-ui/icons/KeyboardArrowLeft";
import KeyboardArrowRight from "@material-ui/icons/KeyboardArrowRight";
import useItemStepper from "../../../hooks/useItemStepper";
import {
    itemType,
    categoriesType,
    servicesType,
    submitHandlerType,
} from "../../../types";

const initialState = {
    category: null,
    service: null,
    otherCategory: "",
    otherService: "",
    amount: 1,
    sliced: "sliced",
    filleted: "filleted",
    details: "",
    categories: null,
    services: null,
};

/**
 * Form used to add a new item to an order or edit an existing item
 *
 * @component
 * @category Item
 */
const ItemForm = ({ existingItem, categories, services, submitHandler }) => {
    // Show details input if an existing item is provided that uses the details property
    const setDetailsHiddenBool =
        existingItem && existingItem.details ? false : true;
    // Show other services input if an existing item is provided that uses the otherService property
    const setOtherServiceHiddenBool =
        existingItem && existingItem.json.otherService ? false : true;
    // Show other category input if an existing item is provided that uses the otherCategory property
    const setOtherCategoryHiddenBool =
        existingItem && existingItem.json.otherCategory ? false : true;

    // Get mUI theme, used for stepper
    const theme = useTheme();

    // Should details input be shown
    const [isDetailsHidden, setDetailsHidden] = React.useState(
        setDetailsHiddenBool
    );

    // Should other service input be shown
    const [isOtherServiceHidden, setOtherServiceHidden] = React.useState(
        setOtherServiceHiddenBool
    );

    // Should other category input be shown
    const [isOtherCategoryHidden, setOtherCategoryHidden] = React.useState(
        setOtherCategoryHiddenBool
    );

    // Was an existing item passsed down as a prop, or is the form being used for new item
    const isExistingItem = existingItem && Object.keys(existingItem).length > 0;

    // Reconstruct existing item for form compatibility
    const reconstructExistingItem = (item) => {
        let idItem = { ...item };
        idItem.service = item.serviceId.toString();
        idItem.category = item.categoryId.toString();
        idItem.filleted = item.json.filleted ? "filleted" : "notFilleted";
        idItem.sliced = item.json.sliced ? "sliced" : "whole";
        idItem.otherCategory = item.json.otherCategory || "";
        idItem.otherService = item.json.otherService || "";
        idItem.categories = categories;
        idItem.services = services;
        return idItem;
    };

    // Init state for either creating a new item or editing an existing one
    const state = isExistingItem
        ? reconstructExistingItem(existingItem)
        : initialState;

    // Pass the categories and services into the state once loaded/changed
    // This is used while constructing the item so it can be properly validated
    // It is removed before submitting
    React.useEffect(() => {
        state.categories = categories;
        state.services = services;
    }, [state, categories, services]);

    // Show other service/category input when selected
    // Hide rm otherService/Category value when deselected
    const handleOtherChange = (e) => {
        if (e.target.name === "service") {
            if (e.target.value === services.length.toString()) {
                setOtherServiceHidden(false);
            } else {
                setOtherServiceHidden(true);
                values.otherService = "";
            }
        }

        if (e.target.name === "category") {
            if (e.target.value === categories.length.toString()) {
                setOtherCategoryHidden(false);
            } else {
                setOtherCategoryHidden(true);
                values.otherCategory = "";
            }
        }
    };

    // Submit item, hide other and detail inputs.
    // rm categories and services from the state thats passed to the submithandler
    // All inputs are reset afterwards
    const handleSubmitAndReset = (values) => {
        setDetailsHidden(true);
        setOtherCategoryHidden(true);
        setOtherServiceHidden(true);
        delete values.categories;
        delete values.services;
        submitHandler(values, resetFields);
        handleStepReset();
    };

    // Use form hook
    const { handleSubmit, handleChange, resetFields, values, errors } = useForm(
        state,
        itemValidate,
        handleSubmitAndReset
    );

    // Use item stepper hook, uses values, services and categories to show appropriate steps and errors
    const {
        activeStep,
        handleStepChange,
        isForwardError,
        handleBack,
        handleStepReset,
    } = useItemStepper(values, services, categories);

    return (
        <FormControl component="fieldset">
            <Paper elevation={3} className="item-form-paper">
                {isForwardError && (
                    <p className="service-text error">
                        {`Fylla skal ${
                            activeStep === 0 ? `Tegund` : `Þjónustu`
                        }`}
                    </p>
                )}
                {activeStep === 0 && (
                    <>
                        <FormLabel component="legend" className="first-label">
                            Tegund:
                        </FormLabel>
                        <RadioGroup
                            name="category"
                            className="select"
                            value={values.category}
                            onChange={handleChange}
                        >
                            {errors.category && (
                                <p className="error">{errors.category}</p>
                            )}
                            {categories.map((cat) => (
                                <FormControlLabel
                                    key={cat.id}
                                    value={`${cat.id}`}
                                    control={
                                        <Radio
                                            onChange={(e) =>
                                                handleOtherChange(e)
                                            }
                                        />
                                    }
                                    label={cat.name}
                                />
                            ))}
                            {errors.otherCategory && (
                                <p className="error">{errors.otherCategory}</p>
                            )}

                            <TextField
                                name="otherCategory"
                                className="select other-input"
                                value={values.otherCategory}
                                type="text"
                                variant="standard"
                                onChange={handleChange}
                                placeholder="Hvaða Tegund?"
                                hidden={isOtherCategoryHidden}
                            />
                        </RadioGroup>
                    </>
                )}
                {activeStep === 1 && (
                    <>
                        <FormLabel component="legend">Þjónusta:</FormLabel>
                        <RadioGroup
                            name="service"
                            className="select"
                            value={values.service}
                            onChange={handleChange}
                        >
                            {errors.service && (
                                <p className="error">{errors.service}</p>
                            )}
                            {services.map((serv) => (
                                <FormControlLabel
                                    key={serv.id}
                                    value={`${serv.id}`}
                                    control={
                                        <Radio onChange={handleOtherChange} />
                                    }
                                    label={serv.name}
                                />
                            ))}
                            {errors.otherService && (
                                <p className="error">{errors.otherService}</p>
                            )}
                            <TextField
                                name="otherService"
                                className="select other-input"
                                value={values.otherService}
                                type="text"
                                variant="standard"
                                onChange={handleChange}
                                placeholder="Hvaða þjónusta?"
                                hidden={isOtherServiceHidden}
                            />
                        </RadioGroup>
                    </>
                )}
                {activeStep === 2 && (
                    <>
                        <FormLabel component="legend">Flökun:</FormLabel>
                        <RadioGroup
                            name="filleted"
                            className="select"
                            value={values.filleted}
                            onChange={handleChange}
                        >
                            {errors.filleted && (
                                <p className="error">{errors.filleted}</p>
                            )}
                            <FormControlLabel
                                value="filleted"
                                control={<Radio />}
                                label="Já"
                            />
                            <FormControlLabel
                                value="notFilleted"
                                control={<Radio />}
                                label="Nei"
                            />
                        </RadioGroup>
                        <FormLabel component="legend">Pökkun:</FormLabel>
                        <RadioGroup
                            name="sliced"
                            className="select"
                            value={values.sliced}
                            onChange={handleChange}
                        >
                            {errors.sliced && (
                                <p className="error">{errors.sliced}</p>
                            )}
                            <FormControlLabel
                                value="whole"
                                control={<Radio />}
                                label="Heilt Flak"
                            />
                            <FormControlLabel
                                value="sliced"
                                control={<Radio />}
                                label="Bitar"
                            />
                            <FormControlLabel
                                value="undisclosed"
                                control={<Radio />}
                                label="Ótilgreint"
                            />
                        </RadioGroup>
                        {!isExistingItem && (
                            <>
                                <FormLabel component="legend">
                                    Fjöldi:
                                </FormLabel>
                                {errors.amount && (
                                    <p className="error">{errors.amount}</p>
                                )}
                                <TextField
                                    name="amount"
                                    className="select amount"
                                    value={values.amount}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    variant="standard"
                                    onChange={handleChange}
                                />
                            </>
                        )}
                        <FormLabel
                            component="legend"
                            onClick={() => setDetailsHidden(!isDetailsHidden)}
                            className="details-input-text"
                        >
                            Annað:{" "}
                            <AddIcon className="plus-icon" fontSize="small" />
                        </FormLabel>
                        {errors.details && (
                            <p className="error">{errors.details}</p>
                        )}
                        <TextField
                            id="outlined-multiline-flexible select"
                            name="details"
                            label="Var það eitthvað annað?"
                            className="details-input"
                            multiline
                            value={values.details}
                            onChange={handleChange}
                            variant="outlined"
                            hidden={isDetailsHidden}
                        />
                    </>
                )}
                <MobileStepper
                    variant="dots"
                    steps={3}
                    position="static"
                    className="item-stepper"
                    activeStep={activeStep}
                    nextButton={
                        <Button
                            size="small"
                            className="stepper-button item-next-button"
                            onClick={handleStepChange}
                            disabled={activeStep === 2}
                        >
                            Áfram
                            {theme.direction === "rtl" ? (
                                <KeyboardArrowLeft />
                            ) : (
                                <KeyboardArrowRight />
                            )}
                        </Button>
                    }
                    backButton={
                        <Button
                            size="small"
                            className="stepper-button back-button"
                            onClick={handleBack}
                            disabled={activeStep === 0}
                        >
                            {theme.direction === "rtl" ? (
                                <KeyboardArrowRight />
                            ) : (
                                <KeyboardArrowLeft />
                            )}
                            Til Baka
                        </Button>
                    }
                />
            </Paper>
            <Button
                className="sbm-btn"
                variant="contained"
                color="primary"
                size="large"
                startIcon={
                    isExistingItem ? <EditIcon /> : <AddShoppingCartIcon />
                }
                onClick={handleSubmit}
            >
                {!isExistingItem ? "Bæta við pöntun" : "Breyta vöru"}
            </Button>
        </FormControl>
    );
};

ItemForm.propTypes = {
    /** An item to be edited, leave empty for creating new item */
    existingItem: itemType,
    /** List of categories */
    categories: categoriesType,
    /** List of services */
    services: servicesType,
    /** CB that handles the submission, e.g. add item to order or send updated item to api */
    submitHandler: submitHandlerType,
};

export default ItemForm;
