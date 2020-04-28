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
    amount: 1,
    sliced: "",
    filleted: "",
    other: "",
};

const ItemForm = ({ existingItem, categories, services, submitHandler }) => {
    const [other, setOther] = React.useState(false);
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
            <FormLabel component="legend">Flökun:</FormLabel>
            <RadioGroup
                name="filleted"
                className="select"
                value={values.fillet}
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
                value={values.slices}
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
                        onClick={() => setOther(!other)}
                    >
                        Annað: <AddIcon fontSize="small" />
                    </FormLabel>
                    {errors.other && <p className="error">{errors.other}</p>}
                    {other ? (
                        <TextField
                            id="outlined-multiline-flexible select"
                            name="other"
                            label="Var það eitthvað annað?"
                            multiline
                            value={values.other}
                            onChange={handleChange}
                            variant="outlined"
                        />
                    ) : (
                        <></>
                    )}
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
