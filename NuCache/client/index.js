import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import App from './components/app'

var packages = [];

const renderApp = () => {
  render(
    <App packages={packages} />,
    document.getElementById('container')
  )
}

$.ajax({
  url: 'http://localhost:55628/api/stats',
  success: function(data) {
    packages = data;
    renderApp();
  }
});

renderApp();
