import React from "react";
import { Switch, Route } from "react-router-dom";
import "./App.css";
import NavigationBar from "./components/Header/NavigationBar/NavigationBar";
import LandingPage from "./components/LandingPage/LandingPage";
import CreateCustomer from "./components/Customer/CreateCustomer/CreateCustomer";
import CustomerView from "./components/Customer/CustomerView/CustomerView";
import UpdateCustomer from "./components/Customer/UpdateCustomer/UpdateCustomer";
import OrderView from "./components/Order/OrderView/OrderView";
import CustomerMain from "./components/Customer/CustomerMain/CustomerMain";
import OrderMain from "./components/Order/OrderMain/OrderMain";

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
                    <Route exact path="/customers" component={CustomerMain} />
                    <Route
                        exact
                        path="/update-customer/:id"
                        component={UpdateCustomer}
                    />
                    <Route
                        exact
                        path="/customer/:id"
                        component={CustomerView}
                    />
                    <Route exact path="/order/:id" component={OrderView} />
                    <Route exact path="/orders" component={OrderMain} />
                </Switch>
            </div>
        </div>
    );
}

export default App;
