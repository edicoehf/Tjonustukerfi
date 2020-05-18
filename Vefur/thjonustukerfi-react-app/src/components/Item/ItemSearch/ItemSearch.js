import React from "react";
import useFindItem from "../../../hooks/useFindItem";
import { Button, Paper } from "@material-ui/core";
import Form from "../../Form/Form";
import { Link } from "react-router-dom";
import ItemDetails from "../ItemDetails/ItemDetails";
import SearchBar from "../../SearchBar/SearchBar";
import "./ItemSearch.css";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";

/**
 * A page for searching for a specific item, by id or barcode
 *
 * @component
 * @category Item
 */
const ItemSearch = () => {
    // Values in the searchbars
    const [values, setValues] = React.useState({ id: "", barcode: "" });

    // Use find item hook, we use the callbacks from it to refetch on search
    const {
        item,
        error,
        fetchItemById,
        fetchItemByBarcode,
        isLoading,
    } = useFindItem();

    // Handle change in the id searchbar input
    const handleIdChange = (event) => {
        setValues({ ...values, id: event.target.value });
    };

    // Handle change in the barcode searchbar input
    const handleBarcodeChange = (event) => {
        setValues({ ...values, barcode: event.target.value });
    };

    // Try to fetch an item with the id in the searchbar
    const handleIdFetch = () => {
        fetchItemById(values.id);
    };

    // Try to fetch an item with the barcode in the searchbar
    const handleBarcodeFetch = () => {
        fetchItemByBarcode(values.barcode);
    };

    return (
        <div className="item-search">
            <h1>Leita að vöru</h1>
            <Paper elevation={3} className="item-search-bars">
                <Form>
                    <SearchBar
                        htmlId="id-search-bar"
                        searchTerm={values.id}
                        handleChange={handleIdChange}
                        placeHolder="Leita eftir vörunúmeri"
                        handleClick={handleIdFetch}
                    />
                    <SearchBar
                        htmlId="barcode-search-bar"
                        searchTerm={values.barcode}
                        handleChange={handleBarcodeChange}
                        placeHolder="Leita eftir strikamerki"
                        handleClick={handleBarcodeFetch}
                    />
                </Form>
            </Paper>
            {isLoading ? (
                <ProgressComponent isLoading={isLoading} />
            ) : (
                <>
                    {!error ? (
                        <div className="item-search-results">
                            {Object.keys(item).length > 0 && (
                                <div className="item-search-result-info">
                                    <ItemDetails id={item.id} />
                                    <Link
                                        to={`/item/${item.id}`}
                                        className="item-link-button"
                                    >
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
                    ) : (
                        <div className="item-not-found">Vara fannst ekki</div>
                    )}
                </>
            )}
        </div>
    );
};

export default ItemSearch;
