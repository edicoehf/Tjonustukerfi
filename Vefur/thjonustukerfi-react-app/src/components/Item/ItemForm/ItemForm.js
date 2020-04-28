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
} from "@material-ui/core";

const initialState = {
    category: null,
    service: null,
    otherCategory: "",
    otherService: "",
    amount: 1,
    sliced: null,
    filleted: null,
    details: "",
};

const ItemForm = ({ existingItem, categories, services, submitHandler }) => {
    const [isDetailsHidden, setDetailsHidden] = React.useState(true);
    const [isOtherServiceHidden, setOtherServiceHidden] = React.useState(true);
    const [isOtherCategoryHidden, setOtherCategoryHidden] = React.useState(
        true
    );

    const handleOtherChange = (e) => {
        console.log(e.target);
        if (e.target.name === "service") {
            if (e.target.value === services.length.toString()) {
                setOtherServiceHidden(false);
            } else {
                setOtherServiceHidden(true);
            }
        }

        if (e.target.name === "category") {
            if (e.target.value === categories.length.toString()) {
                setOtherCategoryHidden(false);
            } else {
                setOtherCategoryHidden(true);
            }
        }
    };

    const getExistingItemWithIds = (item) => {
        let idItem = { ...item };
        const s = services[services.findIndex((s) => s.name === item.service)];
        const c =
            categories[categories.findIndex((c) => c.name === item.category)];
        if (s && c) {
            idItem.service = s.id.toString();
            idItem.category = c.id.toString();
        }
        return idItem;
    };

    const isExistingItem = existingItem && Object.keys(existingItem).length > 0;

    const state = isExistingItem
        ? getExistingItemWithIds(existingItem)
        : initialState;

    const handleSubmitAndReset = (values) => {
        setDetailsHidden(true);
        submitHandler(values, resetFields);
    };

    const { handleSubmit, handleChange, resetFields, values, errors } = useForm(
        state,
        itemValidate,
        handleSubmitAndReset
    );

    return (
        <FormControl component="fieldset">
            <FormLabel component="legend">Tegund:</FormLabel>
            <RadioGroup
                name="category"
                className="select"
                value={values.category}
                onChange={handleChange}
            >
                {errors.category && <p className="error">{errors.category}</p>}
                {categories.map((cat) => (
                    <FormControlLabel
                        key={cat.id}
                        value={`${cat.id}`}
                        control={
                            <Radio onChange={(e) => handleOtherChange(e)} />
                        }
                        label={cat.name}
                    />
                ))}
                <FormControlLabel
                    key={categories.length + 1}
                    value={`${categories.length + 1}`}
                    control={<Radio onChange={handleOtherChange} />}
                    label="Annað"
                />
                <TextField
                    name="otherCategory"
                    className="select"
                    value={values.otherCategory}
                    type="text"
                    variant="standard"
                    onChange={handleChange}
                    placeholder="Hvaða Tegund?"
                    hidden={isOtherCategoryHidden}
                />
            </RadioGroup>
            <FormLabel component="legend">Þjónusta:</FormLabel>
            <RadioGroup
                name="service"
                className="select"
                value={values.service}
                onChange={handleChange}
            >
                {errors.service && <p className="error">{errors.service}</p>}
                {services.map((serv) => (
                    <FormControlLabel
                        key={serv.id}
                        value={`${serv.id}`}
                        control={<Radio onChange={handleOtherChange} />}
                        label={serv.name}
                    />
                ))}
                <FormControlLabel
                    key={services.length + 1}
                    value={`${services.length + 1}`}
                    control={<Radio onChange={handleOtherChange} />}
                    label="Annað"
                />
                <TextField
                    name="otherService"
                    className="select"
                    value={values.otherService}
                    type="text"
                    variant="standard"
                    onChange={handleChange}
                    placeholder="Hvaða þjónusta?"
                    hidden={isOtherServiceHidden}
                />
            </RadioGroup>
            <FormLabel component="legend">Flökun:</FormLabel>
            <RadioGroup
                name="filleted"
                className="select"
                value={values.filleted}
                onChange={handleChange}
            >
                {errors.filleted && <p className="error">{errors.filleted}</p>}
                <FormControlLabel
                    value="filleted"
                    control={<Radio />}
                    label="Flakað"
                />
                <FormControlLabel
                    value="notFilleted"
                    control={<Radio />}
                    label="Óflakað"
                />
            </RadioGroup>
            <FormLabel component="legend">Pökkun:</FormLabel>
            <RadioGroup
                name="sliced"
                className="select"
                value={values.sliced}
                onChange={handleChange}
            >
                {errors.sliced && <p className="error">{errors.sliced}</p>}
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
            </RadioGroup>
            {!isExistingItem && (
                <>
                    <FormLabel component="legend">Fjöldi:</FormLabel>
                    {errors.amount && <p className="error">{errors.amount}</p>}
                    <TextField
                        name="amount"
                        className="select"
                        value={values.amount}
                        type="number"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        variant="standard"
                        onChange={handleChange}
                    />
                </>
            )}
            {!isExistingItem && (
                <>
                    <FormLabel
                        component="legend"
                        onClick={() => setDetailsHidden(!isDetailsHidden)}
                    >
                        Annað: <AddIcon fontSize="small" />
                    </FormLabel>
                    {errors.details && (
                        <p className="error">{errors.details}</p>
                    )}
                    <TextField
                        id="outlined-multiline-flexible select"
                        name="details"
                        label="Var það eitthvað annað?"
                        multiline
                        value={values.details}
                        onChange={handleChange}
                        variant="outlined"
                        hidden={isDetailsHidden}
                    />
                </>
            )}
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

export default ItemForm;
