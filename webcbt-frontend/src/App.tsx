import React from 'react';
import {Routes, Route} from 'react-router-dom';
import Footer from './components/Footer/Footer';
import Header from './components/Header/Header';
import Layout from './components/Layout/Layout';
import Registration from './pages/Registration/Registration';
import Login from './pages/Login/Login';

import './App.css';
import 'antd/dist/antd.css';

function App() {
  return (
    <div className="App">
      <Layout>
        <Header />
        <Routes>
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
        </Routes>
        <Footer />
      </Layout>
    </div>
  );
}

export default App;
