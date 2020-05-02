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
} from "@material-ui/core";
import useGetNextStatesById from "../../../hooks/useGetNextStatesById";
import useGetItemLocations from "../../../hooks/useGetItemLocations";
import useUpdateItemState from "../../../hooks/useUpdateItemState";
import "./StateSelection.css";

const StateSelection = ({ id, hasUpdated }) => {
    const { states, error, fetchNextStates } = useGetNextStatesById(id);
    const {
        itemLocations,
        error: locationsError,
        fetchItemLocations,
    } = useGetItemLocations();
    const [location, setLocation] = React.useState("");
    const [state, setState] = React.useState(null);
    const [stateName, setStateName] = React.useState("");
    const [activeStep, setActiveStep] = React.useState(0);
    const handleNext = () => {
        if (states.nextAvailableStates.length > 1 && activeStep < 2) {
            setActiveStep((prevActiveStep) => prevActiveStep + 1);
        } else if (activeStep === 0) {
            setActiveStep((prevActiveStep) => prevActiveStep + 2);
        }
    };
    const handleBack = () => {
        if (activeStep > 0 && states.nextAvailableStates.length > 1) {
            setActiveStep((prevActiveStep) => prevActiveStep - 1);
        } else if (activeStep === 2) {
            setActiveStep((prevActiveStep) => prevActiveStep - 2);
        }
    };

    const handleStateSelection = (state) => {
        handleNext();
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
    }, [states]);

    const [openSelection, setOpenSelection] = React.useState(false);

    const handleClose = () => {
        setActiveStep(0);
        setLocation("");
        setState(null);
        setStateName("");
        setOpenSelection(false);
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
            {!error ? (
                <>
                    {nextStates.length > 0 && (
                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleOpen}
                            disabled={nextStates.length === 0}
                            className="state-btn"
                        >
                            Færa í næstu stöðu
                        </Button>
                    )}
                    <Dialog onClose={handleClose} open={openSelection}>
                        {activeStep === 0 && (
                            <>
                                <DialogTitle id="state-dialog-title">
                                    Veldu næstu stöðu
                                </DialogTitle>
                                <DialogContent>
                                    <List>
                                        {nextStates.map((state) => (
                                            <ListItem
                                                button
                                                onClick={() =>
                                                    handleStateSelection(state)
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
                            </>
                        )}
                        {activeStep === 1 && (
                            <>
                                <DialogTitle id="state-dialog-title">
                                    Veldu staðsetningu
                                </DialogTitle>
                                <DialogContent>
                                    <List>
                                        {itemLocations.map((location) => (
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
                                                    primary={location}
                                                />
                                            </ListItem>
                                        ))}
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
                                    <DialogContentText>
                                        {stateName}
                                        {location !== "" && `, ${location}`}
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
                <p className="error">Gat ekki sótt næstu stöður</p>
            )}
        </div>
    );
};

export default StateSelection;
