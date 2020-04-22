import React from "react";
import { ListItem } from "@material-ui/core";
import "./ListItemLink.css";

const ListItemLink = (props) => {
    return (
        <div className="link-item">
            <ListItem button component="a" {...props} />
        </div>
    );
};

export default ListItemLink;
