import React from "react";
import LinearScaleIcon from "@material-ui/icons/LinearScale";
import {
    Dialog,
    DialogTitle,
    List,
    ListItem,
    ListItemAvatar,
    Avatar,
    ListItemText,
    Button,
    DialogActions,
    DialogContent,
    DialogContentText,
    MobileStepper,
} from "@material-ui/core";
import useGetNextStatesById from "../../../hooks/useGetNextStatesById";
import useGetItemLocations from "../../../hooks/useGetItemLocations";
import useUpdateItemState from "../../../hooks/useUpdateItemState";
import "./StateSelection.css";
import ProgressComponent from "../../Feedback/ProgressComponent/ProgressComponent";
import useGetAllStates from "../../../hooks/useGetAllStates";
import ProgressButton from "../../Feedback/ProgressButton/ProgressButton";
import { idType, cbType } from "../../../types";

/**
 * Select new state for item. Button which opens a modal with the available next states for an item.
 * Also has the option to select from any other state via small modal button, incase of human error
 *
 * @component
 * @category Item
 */
const StateSelection = ({ id, hasUpdated, componentLoading }) => {
    // Get the next states for an item
    const {
        states,
        error,
        fetchNextStates,
        isLoading: statesLoading,
    } = useGetNextStatesById(id);
    // Get the locations that exists
    const {
        itemLocations,
        error: locationsError,
        fetchItemLocations,
        isLoading: locationsLoading,
    } = useGetItemLocations();
    // Get all possible states
    const {
        states: allStates,
        error: allStatesError,
        isLoading: allStatesLoading,
    } = useGetAllStates();

    // Select state from all possible states or the next available states
    const [selectFromAllStates, setSelectFromAllStates] = React.useState(false);
    // Which stgate is the item going in
    const [state, setState] = React.useState(null);
    // Which location in the state is the item going in
    const [location, setLocation] = React.useState("");
    // What is the name of the state that was picked
    const [stateName, setStateName] = React.useState("");
    // Which step in the stepper is active
    const [activeStep, setActiveStep] = React.useState(0);

    // Pick state from all possible steps instead
    const handleSelectFromAllStates = () => {
        setSelectFromAllStates(true);
    };

    // Pick state from the next available steps for this item
    const handleSelectFromNextStates = () => {
        setSelectFromAllStates(false);
    };

    // Move to the next step in the modal, skip location selection if first or last state was picked
    const handleNext = (selectedState) => {
        if (
            selectedState &&
            activeStep === 0 &&
            (selectedState.id === 1 || selectedState.id === allStates.length)
        ) {
            setActiveStep((prevActiveStep) => prevActiveStep + 2);
        } else {
            setActiveStep((prevActiveStep) => prevActiveStep + 1);
        }
    };

    // Move to the previous step in the modal, skip location selection if first or last state was picked
    const handleBack = () => {
        if (activeStep > 0 && (state === 1 || state === allStates.length)) {
            setActiveStep((prevActiveStep) => prevActiveStep - 2);
        } else {
            setActiveStep((prevActiveStep) => prevActiveStep - 1);
        }
    };

    // Select a state and move to next step
    const handleStateSelection = (state) => {
        handleNext(state);
        setState(state.id);
        setStateName(state.name);
    };

    // Select location and move to next step
    const handleLocationSelection = (location) => {
        handleNext();
        setLocation(location);
    };

    // Refetch next states and locations as the item has updated
    const handleStateUpdate = () => {
        hasUpdated();
        fetchNextStates();
        fetchItemLocations();
    };

    // use update item state hook, send handlestateupdate as cb to be called on success
    const { updateError, handleUpdate, isProcessing } = useUpdateItemState(
        handleStateUpdate
    );

    // List of the next available states
    const [nextStates, setNextStates] = React.useState([]);

    // Sort next available states
    React.useEffect(() => {
        if (states.nextAvailableStates) {
            setNextStates(
                states.nextAvailableStates.sort((a, b) => a.id - b.id)
            );
        }
    }, [states]);

    // Tell parent component whether its loading or not, but only if parent provides such function
    React.useEffect(() => {
        if (componentLoading !== undefined) {
            componentLoading(
                statesLoading || locationsLoading || allStatesLoading
            );
        }
    }, [statesLoading, locationsLoading, allStatesLoading, componentLoading]);

    // Is modal open
    const [openSelection, setOpenSelection] = React.useState(false);

    // Reset everything and close modal
    const handleClose = () => {
        setOpenSelection(false);
        setActiveStep(0);
        setLocation("");
        setState(null);
        setStateName("");
        setSelectFromAllStates(false);
    };

    // Open modal
    const handleOpen = () => {
        setOpenSelection(true);
    };

    // Send the selected state and location to be updated in api
    // then close the modal
    const handleSelection = () => {
        if (!isProcessing) {
            handleUpdate({
                item: parseInt(id),
                state: state,
                location: location,
            });
        }
        handleClose();
    };

    return (
        <div className="state-selection">
            {statesLoading || locationsLoading ? (
                <ProgressComponent isLoading={componentLoading === undefined} />
            ) : (
                <>
                    {!error && !locationsError && !allStatesError ? (
                        <>
                            <ProgressButton isLoading={isProcessing}>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={handleOpen}
                                    disabled={isProcessing}
                                    className="state-btn"
                                >
                                    Færa um stöðu
                                </Button>
                            </ProgressButton>
                            <Dialog
                                onClose={handleClose}
                                open={openSelection}
                                className="state-selection-modal"
                            >
                                <MobileStepper
                                    variant="dots"
                                    steps={3}
                                    position="static"
                                    activeStep={activeStep}
                                />
                                {activeStep === 0 && !selectFromAllStates && (
                                    <>
                                        <DialogTitle id="state-dialog-title">
                                            Veldu næstu stöðu
                                        </DialogTitle>
                                        <DialogContent>
                                            <List className="selection-list">
                                                {nextStates.map((state) => (
                                                    <ListItem
                                                        button
                                                        onClick={() =>
                                                            handleStateSelection(
                                                                state
                                                            )
                                                        }
                                                        key={state.id}
                                                    >
                                                        <ListItemAvatar>
                                                            <Avatar>
                                                                <LinearScaleIcon />
                                                            </Avatar>
                                                        </ListItemAvatar>
                                                        <ListItemText
                                                            primary={state.name}
                                                        />
                                                    </ListItem>
                                                ))}
                                            </List>
                                        </DialogContent>
                                        <DialogActions>
                                            <Button
                                                color="primary"
                                                onClick={
                                                    handleSelectFromAllStates
                                                }
                                            >
                                                Velja aðra stöðu
                                            </Button>
                                        </DialogActions>
                                    </>
                                )}
                                {activeStep === 0 && selectFromAllStates && (
                                    <>
                                        <DialogTitle id="state-dialog-title">
                                            Veldu stöðu
                                        </DialogTitle>
                                        <DialogContent>
                                            <List className="selection-list">
                                                {allStates.map((state) => (
                                                    <ListItem
                                                        button
                                                        onClick={() =>
                                                            handleStateSelection(
                                                                state
                                                            )
                                                        }
                                                        key={state.id}
                                                    >
                                                        <ListItemAvatar>
                                                            <Avatar>
                                                                <LinearScaleIcon />
                                                            </Avatar>
                                                        </ListItemAvatar>
                                                        <ListItemText
                                                            primary={state.name}
                                                        />
                                                    </ListItem>
                                                ))}
                                            </List>
                                        </DialogContent>
                                        <DialogActions>
                                            <Button
                                                color="primary"
                                                onClick={
                                                    handleSelectFromNextStates
                                                }
                                            >
                                                Velja úr næstu stöðum
                                            </Button>
                                        </DialogActions>
                                    </>
                                )}
                                {activeStep === 1 && (
                                    <>
                                        <DialogTitle id="state-dialog-title">
                                            Veldu staðsetningu
                                        </DialogTitle>
                                        <DialogContent>
                                            <List className="selection-list">
                                                {itemLocations.map(
                                                    (location) => (
                                                        <ListItem
                                                            button
                                                            key={location}
                                                            onClick={() =>
                                                                handleLocationSelection(
                                                                    location
                                                                )
                                                            }
                                                        >
                                                            <ListItemAvatar>
                                                                <Avatar>
                                                                    <LinearScaleIcon />
                                                                </Avatar>
                                                            </ListItemAvatar>
                                                            <ListItemText
                                                                primary={
                                                                    location
                                                                }
                                                            />
                                                        </ListItem>
                                                    )
                                                )}
                                            </List>
                                        </DialogContent>
                                        <DialogActions>
                                            <Button
                                                color="primary"
                                                onClick={handleBack}
                                            >
                                                Til baka
                                            </Button>
                                        </DialogActions>
                                    </>
                                )}
                                {activeStep === 2 && (
                                    <>
                                        <DialogTitle id="state-dialog-title">
                                            Staðfestu nýja stöðu
                                        </DialogTitle>
                                        <DialogContent>
                                            <DialogContentText className="selection-list">
                                                {stateName}
                                                {location !== "" &&
                                                    `, ${location}`}
                                            </DialogContentText>
                                        </DialogContent>
                                        <DialogActions>
                                            <Button
                                                color="primary"
                                                onClick={handleBack}
                                            >
                                                Til baka
                                            </Button>
                                            <Button
                                                color="primary"
                                                onClick={handleSelection}
                                            >
                                                Staðfesta
                                            </Button>
                                        </DialogActions>
                                    </>
                                )}
                            </Dialog>
                            {updateError && (
                                <p className="error">Gat ekki uppfært stöðu</p>
                            )}
                        </>
                    ) : (
                        <p className="error">Gat ekki sótt stöður</p>
                    )}
                </>
            )}
        </div>
    );
};

StateSelection.propTypes = {
    /** Item ID */
    id: idType,
    /** CB to let parent know that the item has been updated, this is incase a parent has multiple components that display info on the item, so they can refetch */
    hasUpdated: cbType,
    /** CB to let parent know if item is still being fetched
     * @param {bool} isLoading - Is component still loading
     */
    componentLoading: cbType,
};

export default StateSelection;
