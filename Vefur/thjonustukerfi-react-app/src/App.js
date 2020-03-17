import React from "react";
import { Switch, Route } from "react-router-dom";

import "./App.css";
import NavigationBar from "./components/Header/NavigationBar/NavigationBar";
import LandingPage from "./components/LandingPage/LandingPage";
import CreateCustomer from "./components/Customer/CreateCustomer/CreateCustomer";
import CustomerView from "./components/Customer/CustomerView/CustomerView";
function App() {
    return (
        <div className="app">
            <div className="navbar-container">
                <NavigationBar />
            </div>
            <div className="container">
                <Switch>
                    <Route exact path="/" component={LandingPage} />
                    <Route
                        exact
                        path="/new-customer"
                        component={CreateCustomer}
                    />
                    <Route 
                        exact
                        path="/customer/:id"
                        component={CustomerView}
                    />
                </Switch>
            </div>
        </div>
    );
}

export default App;
