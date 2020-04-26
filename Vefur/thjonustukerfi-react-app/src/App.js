import React from "react";
import { Switch, Route } from "react-router-dom";
import "./App.css";
import NavigationBar from "./components/Header/NavigationBar/NavigationBar";
// import LandingPage from "./components/LandingPage/LandingPage";
import CreateCustomer from "./components/Customer/CreateCustomer/CreateCustomer";
import CustomerView from "./components/Customer/CustomerView/CustomerView";
import UpdateCustomer from "./components/Customer/UpdateCustomer/UpdateCustomer";
import OrderView from "./components/Order/OrderView/OrderView";
import CustomerMain from "./components/Customer/CustomerMain/CustomerMain";
import CreateOrderView from "./components/Order/CreateOrder/CreateOrderView/CreateOrderView";
import OrderMain from "./components/Order/OrderMain/OrderMain";
import ItemView from "./components/Item/ItemView/ItemView";
import EditItem from "./components/Item/EditItem/EditItem";
import UpdateOrderView from "./components/Order/CreateOrder/UpdateOrderView/UpdateOrderView";
import ItemSearch from "./components/Item/ItemSearch/ItemSearch";

function App() {
    return (
        <div className="app">
            <div className="navbar-container">
                <NavigationBar />
            </div>
            <div className="main-container">
                <Switch>
                    <Route exact path="/" component={CreateOrderView} />
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
                    <Route
                        exact
                        path="/new-order"
                        component={CreateOrderView}
                    />
                    <Route
                        exact
                        path="/update-order/:id"
                        component={UpdateOrderView}
                    />
                    <Route exact path="/orders" component={OrderMain} />
                    <Route exact path="/item/:id" component={ItemView} />
                    <Route exact path="/update-item/:id" component={EditItem} />
                    <Route exact path="/item-search" component={ItemSearch} />
                </Switch>
            </div>
        </div>
    );
}

export default App;
