import React, { useState, useMemo } from "react";
import { Switch, Route } from "react-router-dom";
import "./App.css";
import NavigationBar from "./components/Header/NavigationBar/NavigationBar";
import LandingPage from "./components/LandingPage/LandingPage";
import CreateCustomer from "./components/Customer/CreateCustomer/CreateCustomer";
import CustomerView from "./components/Customer/CustomerView/CustomerView";
import { CustomerContext } from "./context/customerContext";

function App() {
    const [customer, setCustomer] = useState({});
    const providerValue = useMemo(() => ({ customer, setCustomer }), [
        customer,
        setCustomer
    ]);
    return (
        <div className="app">
            <div className="navbar-container">
                <NavigationBar />
            </div>
            <div className="container">
                <Switch>
                    <Route exact path="/" component={LandingPage} />
                    <CustomerContext.Provider value={providerValue}>
                        <Route
                            path="/new-customer"
                            render={({ customer }) => (
                                <CreateCustomer customer={customer} />
                            )}
                        />
                        <Route
                            exact
                            path="/customer/:id"
                            component={CustomerView}
                        />
                    </CustomerContext.Provider>
                </Switch>
            </div>
        </div>
    );
}

export default App;
