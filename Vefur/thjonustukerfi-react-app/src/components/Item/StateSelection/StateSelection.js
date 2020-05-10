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

const StateSelection = ({ id, hasUpdated, componentLoading }) => {
    const {
        states,
        error,
        fetchNextStates,
        isLoading: statesLoading,
    } = useGetNextStatesById(id);
    const {
        itemLocations,
        error: locationsError,
        fetchItemLocations,
        isLoading: locationsLoading,
    } = useGetItemLocations();
    const {
        states: allStates,
        error: allStatesError,
        isLoading: allStatesLoading,
    } = useGetAllStates();

    const [selectFromAllStates, setSelectFromAllStates] = React.useState(false);
    const [location, setLocation] = React.useState("");
    const [state, setState] = React.useState(null);
    const [stateName, setStateName] = React.useState("");
    const [activeStep, setActiveStep] = React.useState(0);

    const handleSelectFromAllStates = () => {
        setSelectFromAllStates(true);
    };

    const handleSelectFromNextStates = () => {
        setSelectFromAllStates(false);
    };

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

    const handleBack = () => {
        if (activeStep > 0 && (state === 1 || state === allStates.length)) {
            setActiveStep((prevActiveStep) => prevActiveStep - 2);
        } else {
            setActiveStep((prevActiveStep) => prevActiveStep - 1);
        }
    };

    const handleStateSelection = (state) => {
        handleNext(state);
        setState(state.id);
        setStateName(state.name);
    };

    const handleLocationSelection = (location) => {
        handleNext();
        setLocation(location);
    };

    const handleStateUpdate = () => {
        hasUpdated();
        fetchNextStates();
        fetchItemLocations();
    };

    const { updateError, handleUpdate, isProcessing } = useUpdateItemState(
        handleStateUpdate
    );

    const [nextStates, setNextStates] = React.useState([]);

    React.useEffect(() => {
        if (states.nextAvailableStates) {
            setNextStates(
                states.nextAvailableStates.sort((a, b) => a.id - b.id)
            );
        }
        if (componentLoading !== undefined) {
            componentLoading(
                statesLoading || locationsLoading || allStatesLoading
            );
        }
    }, [
        states,
        componentLoading,
        statesLoading,
        locationsLoading,
        allStatesLoading,
    ]);

    const [openSelection, setOpenSelection] = React.useState(false);

    const handleClose = () => {
        setOpenSelection(false);
        setActiveStep(0);
        setLocation("");
        setState(null);
        setStateName("");
        setSelectFromAllStates(false);
    };
    const handleOpen = () => {
        setOpenSelection(true);
    };

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

export default StateSelection;
