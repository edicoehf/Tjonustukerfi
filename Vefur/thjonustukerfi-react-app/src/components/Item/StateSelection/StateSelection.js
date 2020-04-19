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
} from "@material-ui/core";

const StateSelection = ({ id }) => {
    const { updateError, handleUpdate, isProcessing } = useUpdateItemState();
    const { states, error } = useGetNextStatesById(id);

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
        setOpenSelection(false);
    };
    const handleOpen = () => {
        setOpenSelection(true);
    };

    const handleSelection = (stateId) => {
        if (!isProcessing) {
            handleUpdate({ itemId: id, stateChangeTo: stateId });
        }
    };
    return (
        <div className="state-selection">
            {!error ? (
                <>
                    <Button
                        variant="contained"
                        color="primary"
                        onClick={handleOpen}
                        disabled={nextStates.length > 0}
                    >
                        Færa í næstu stöðu
                    </Button>
                    <StateSelection
                        handleClose={handleClose}
                        handleSelection={handleSelection}
                        open={openSelection}
                        states={nextStates}
                    />
                    <Dialog onClose={handleClose} open={open}>
                        <DialogTitle id="state-dialog-title">
                            Veldu næstu stöðu
                        </DialogTitle>
                        <List>
                            {states.map((state) => (
                                <ListItem
                                    button
                                    onClick={() => handleSelection(state.id)}
                                    key={state.id}
                                >
                                    <ListItemAvatar>
                                        <Avatar>
                                            <LinearScaleIcon />
                                        </Avatar>
                                    </ListItemAvatar>
                                    <ListItemText primary={state.name} />
                                </ListItem>
                            ))}
                        </List>
                    </Dialog>
                </>
            ) : (
                <p className="error">Gat ekki sótt næstu stöður</p>
            )}
            {updateError && <p className="error">Gat ekki uppfært stöðu</p>}
        </div>
    );
};

export default StateSelection;
