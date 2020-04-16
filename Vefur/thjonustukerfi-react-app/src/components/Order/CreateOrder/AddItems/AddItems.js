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
import "./AddItems.css";

const initialState = {
    category: null,
    service: null,
    amount: 1,
};

const AddItems = ({ addItems }) => {
    const submitHandler = (values) => {
        addItems(values, resetFields);
    };

    const { services } = useGetServices();
    const { categories } = useGetCategories();
    const { handleSubmit, handleChange, resetFields, values, errors } = useForm(
        initialState,
        itemValidate,
        submitHandler
    );

    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <FormControl component="fieldset">
                <FormLabel component="legend">Tegund:</FormLabel>
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
                    {errors.service && (
                        <p className="error">{errors.service}</p>
                    )}
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
                <Button
                    className="sbm-btn"
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
