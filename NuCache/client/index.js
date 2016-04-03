import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import { createStore, applyMiddleware } from 'redux'
import { Provider } from 'react-redux'
import remoteMiddleware from './infrastructure/remoteMiddleware'

import { refreshPackages } from './actions'
import appReducer from './reducers'

import App from './components/app'

const createStoreWithRemote = applyMiddleware(remoteMiddleware)(createStore);
const store = createStoreWithRemote(appReducer);

store.dispatch(refreshPackages());

render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('container')
)
