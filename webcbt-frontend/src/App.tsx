import React from 'react';
import {Routes, Route, Navigate} from 'react-router-dom';
import Footer from './components/Footer/Footer';
import Header from './components/Header/Header';
import Layout from './components/Layout/Layout';
import useAppSelector from './hooks/useAppSelector';
import Registration from './pages/Registration/Registration';
import Login from './pages/Login/Login';

import './App.css';
import selectIsAuth from './store/user/selectors/selectIsAuth';

function App() {
  const isAuth = useAppSelector(selectIsAuth);

  return (
    <div className="App">
      <Layout>
        <Header />
        <Routes>
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          {!isAuth && (
            <Route path="*" element={<Navigate to="/login" replace />} />
          )}
          {isAuth && <Route path="/" />}
        </Routes>

        <Footer />
      </Layout>
    </div>
  );
}

export default App;
