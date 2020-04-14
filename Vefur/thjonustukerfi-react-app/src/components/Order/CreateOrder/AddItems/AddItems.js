import React from "react";
import {
    Radio,
    RadioGroup,
    FormControlLabel,
    FormControl,
    FormLabel,
    TextField,
    Button,
} from "@material-ui/core";
import AddShoppingCartIcon from "@material-ui/icons/AddShoppingCart";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import useForm from "../../../../hooks/useForm";
import itemValidate from "../ItemValidate/ItemValidate";

const initialState = {
    category: null,
    service: null,
    amount: 1,
};

const AddItems = ({ addItems }) => {
    const { services, error: serviceError } = useGetServices();
    const { categories, error: categoryError } = useGetCategories();
    const { handleSubmit, handleChange, values, errors } = useForm(
        initialState,
        itemValidate,
        addItems
    );

    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <FormControl component="fieldset">
                <FormLabel component="legend">Tegund:</FormLabel>
                <RadioGroup
                    name="category"
                    value={values.category}
                    onChange={handleChange}
                >
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
                    value={values.service}
                    onChange={handleChange}
                >
                    {services.map((serv) => (
                        <FormControlLabel
                            key={serv.id}
                            value={`${serv.id}`}
                            control={<Radio />}
                            label={serv.name}
                        />
                    ))}
                </RadioGroup>
                <FormLabel component="legend">Fjöldi:</FormLabel>
                <TextField
                    value={values.amount}
                    type="number"
                    InputLabelProps={{
                        shrink: true,
                    }}
                    variant="standard"
                    onChange={handleChange}
                    name="amount"
                />
                <Button
                    variant="contained"
                    color="primary"
                    size="large"
                    startIcon={<AddShoppingCartIcon />}
                    onClick={handleSubmit}
                >
                    Bæta við pöntun
                </Button>
            </FormControl>
        </div>
    );
};

export default AddItems;
