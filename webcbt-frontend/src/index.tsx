import React from 'react';
import ReactDOM from 'react-dom';

import 'antd/dist/antd.css';
import './index.css';
import {Provider} from 'react-redux';

import {BrowserRouter as Router} from 'react-router-dom';
import {ToastContainer} from 'react-toastify';

import App from './App';
import {store} from './store/store';
import * as serviceWorker from './serviceWorker';

import 'react-toastify/dist/ReactToastify.min.css';

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <Router>
        <ToastContainer className="toast" autoClose={4000} limit={3} />
        <App />
      </Router>
    </Provider>
  </React.StrictMode>,
  document.getElementById('root'),
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
