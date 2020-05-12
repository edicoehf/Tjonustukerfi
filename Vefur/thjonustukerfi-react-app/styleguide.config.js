module.exports = {
    handlers: (componentPath) =>
        require("react-docgen").defaultHandlers.concat(
            require("react-docgen-external-proptypes-handler")(componentPath),
            require("react-docgen-displayname-handler").createDisplayNameHandler(
                componentPath
            )
        ),
};
