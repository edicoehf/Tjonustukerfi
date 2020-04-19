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
import useGetNextStatesById from "../../../hooks/useGetNextStatesById";
import useUpdateItemState from "../../../hooks/useUpdateItemState";
import "./StateSelection.css";

const StateSelection = ({ id, hasUpdated }) => {
    const { states, error, fetchNextStates } = useGetNextStatesById(id);

    const handleStateUpdate = () => {
        hasUpdated();
        fetchNextStates();
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
        setOpenSelection(false);
    };
    const handleOpen = () => {
        setOpenSelection(true);
    };

    const handleSelection = (stateId) => {
        if (!isProcessing) {
            handleUpdate({ item: parseInt(id), state: stateId });
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
                        <DialogTitle id="state-dialog-title">
                            Veldu næstu stöðu
                        </DialogTitle>
                        <List>
                            {nextStates.map((state) => (
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
