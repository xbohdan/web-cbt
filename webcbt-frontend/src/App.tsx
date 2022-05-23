import React from 'react';
import Footer from './components/Footer/Footer';
import Header from './components/Header/Header';
import Layout from './components/Layout/Layout';

import './App.css';
import Router from './components/Router/Router';

function App() {
  return (
    <div className="App">
      <Layout>
        <Header />
        <Router />
        <Footer />
      </Layout>
    </div>
  );
}

export default App;
