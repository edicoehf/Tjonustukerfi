import React from "react";
import useFindItem from "../../../hooks/useFindItem";
import {
    IconButton,
    FormControl,
    InputLabel,
    Input,
    InputAdornment,
    Button,
} from "@material-ui/core";
import SearchIcon from "@material-ui/icons/Search";
import "./ItemSearch.css";
import Form from "../../Form/Form";
import { Link } from "react-router-dom";
import ItemDetails from "../ItemDetails/ItemDetails";

const ItemSearch = () => {
    const [values, setValues] = React.useState({ id: "", barcode: "" });
    const { item, error, fetchItemById, fetchItemByBarcode } = useFindItem();

    const handleChange = (name) => (event) => {
        setValues({ ...values, [name]: event.target.value });
    };
    console.log(item);
    return (
        <div className="item-search">
            <h1>Leita að vöru</h1>
            <div className="item-search-bars">
                <Form>
                    <FormControl>
                        <InputLabel htmlFor="item-id-search">
                            Leita eftir vörunúmeri
                        </InputLabel>
                        <Input
                            id="item-id-search"
                            type="text"
                            autoComplete="off"
                            value={values.id}
                            onChange={handleChange("id")}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton
                                        onClick={() => {
                                            fetchItemById(values.id);
                                        }}
                                    >
                                        <SearchIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                    <FormControl>
                        <InputLabel htmlFor="item-id-barcode">
                            Leita eftir strikamerki
                        </InputLabel>
                        <Input
                            id="item-id-barcode"
                            type="text"
                            autoComplete="off"
                            value={values.barcode}
                            onChange={handleChange("barcode")}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton
                                        onClick={() => {
                                            fetchItemByBarcode(values.barcode);
                                        }}
                                    >
                                        <SearchIcon />
                                    </IconButton>
                                </InputAdornment>
                            }
                        />
                    </FormControl>
                </Form>
            </div>
            {error ? (
                <div className="item-not-found">Vara fannst ekki</div>
            ) : (
                <div className="item-search-results">
                    {Object.keys(item).length > 0 && (
                        <div className="item-search-result-info">
                            <ItemDetails id={item.id} />
                            <Link to={`/item/${item.id}`}>
                                <Button
                                    className="details-item-button"
                                    variant="contained"
                                    color="primary"
                                >
                                    Skoða nánar
                                </Button>
                            </Link>
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};

export default ItemSearch;
