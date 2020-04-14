import React from "react";
import {
    Radio,
    RadioGroup,
    FormControlLabel,
    FormControl,
    FormLabel,
    ButtonGroup,
    Button,
} from "@material-ui/core";
import useGetServices from "../../../../hooks/useGetServices";
import useGetCategories from "../../../../hooks/useGetCategories";
import useForm from "../../../../hooks/useForm";
import itemValidate from "../ItemValidate/ItemValidate";

const initialState = {
    category: 0,
    service: 0,
    amount: 1,
};

const AddItems = () => {
    const { services, error: serviceError } = useGetServices();
    const { categories, error: categoryError } = useGetCategories();
    const { handleSubmit, handleChange, values, errors } = useForm(
        initialState,
        itemValidate,
        submitHandler
    );

    const check = (e) => {
        console.log(e.target);
    };

    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <FormControl component="fieldset">
                <FormLabel component="legend">Tegund:</FormLabel>
                <RadioGroup
                    name="category"
                    // value={value}
                    onChange={check}
                >
                    {categories.map((cat) => (
                        <FormControlLabel
                            key={cat.id}
                            value={cat.id}
                            control={<Radio />}
                            label={cat.name}
                        />
                    ))}
                </RadioGroup>
                <FormLabel component="legend">Þjónusta:</FormLabel>
                <RadioGroup
                    name="services"
                    // value={value}
                    // onChange={handleChange}
                >
                    {services.map((serv) => (
                        <FormControlLabel
                            key={serv.id}
                            value={serv.id}
                            control={<Radio />}
                            label={serv.name}
                        />
                    ))}
                </RadioGroup>
                <FormLabel component="legend">Fjöldi:</FormLabel>
                <ButtonGroup size="small">
                    <Button
                    // disabled={counter < 1}
                    // onClick={this.handleDecrement}
                    >
                        -
                    </Button>
                    {/* <Button disabled>{this.state.counter}</Button> */}
                    {/* <Button onClick={this.handleIncrement}>+</Button> */}
                </ButtonGroup>
            </FormControl>
        </div>
    );
};

export default AddItems;
