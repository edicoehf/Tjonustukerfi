import React from 'react';
import logo from './logo.svg';
import './App.css';
import CustomerInputForm from './components/Customer/CustomerInputForm/CustomerInputForm';

function App() {
  return (
    <div className="App">
      <header className="App-header">
				<h1>Landing Page!</h1>
        <CustomerInputForm />
      </header>
    </div>
  );
}

export default App;
