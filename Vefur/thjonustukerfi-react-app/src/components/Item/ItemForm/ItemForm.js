import React from "react";
import useForm from "../../../hooks/useForm";
import EditIcon from "@material-ui/icons/Edit";
import AddShoppingCartIcon from "@material-ui/icons/AddShoppingCart";
import itemValidate from "../ItemValidate/ItemValidate";
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
    amount: 1,
    slices: "",
};

const ItemForm = ({ existingItem, categories, services, submitHandler }) => {
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
                        control={<Radio />}
                        label={cat.name}
                    />
                ))}
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
                        control={<Radio />}
                        label={serv.name}
                    />
                ))}
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
                    <FormLabel component="legend">Skurður:</FormLabel>
                    {errors.slices && <p className="error">{errors.slices}</p>}
                    <TextField
                        name="slices"
                        className="select"
                        value={values.slices}
                        type="text"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        variant="standard"
                        onChange={handleChange}
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
