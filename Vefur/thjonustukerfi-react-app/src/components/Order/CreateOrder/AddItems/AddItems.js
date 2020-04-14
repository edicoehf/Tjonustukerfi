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

const { services, serviceError: error } = useGetServices();
const { categories, categoryError: error } = useGetCategories();

const AddItems = () => {
    return (
        <div className="add-items">
            <h3>Bæta við vöru</h3>
            <FormControl component="fieldset">
                <FormLabel component="legend">Tegund:</FormLabel>
                <RadioGroup
                    name="category"
                    value={value}
                    onChange={handleChange}
                >
                    {categories.map((cat) => (
                        <FormControlLabel
                            value={cat.id}
                            control={<Radio />}
                            label={cat.name}
                        />
                    ))}
                </RadioGroup>
            </FormControl>
            <FormControl component="fieldset">
                <FormLabel component="legend">Þjónusta:</FormLabel>
                <RadioGroup
                    name="services"
                    value={value}
                    onChange={handleChange}
                >
                    {services.map((serv) => (
                        <FormControlLabel
                            value={serv.id}
                            control={<Radio />}
                            label={serv.name}
                        />
                    ))}
                </RadioGroup>
            </FormControl>
            <FormControl component="fieldset">
                <FormLabel component="legend">Fjöldi:</FormLabel>
                <ButtonGroup size="small">
                    <Button
                        disabled={counter < 1}
                        onClick={this.handleDecrement}
                    >
                        -
                    </Button>
                    <Button disabled>{this.state.counter}</Button>
                    <Button onClick={this.handleIncrement}>+</Button>
                </ButtonGroup>
            </FormControl>
        </div>
    );
};

export default AddItems;
