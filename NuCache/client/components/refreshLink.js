import React from 'react'
import { connect } from 'react-redux'
import { refreshPackages } from '../actions'

const mapDispatchToProps = (dispatch) => {
  return {
    refresh: () => dispatch(refreshPackages())
  }
}

const RefreshLink = ({ refresh }) => {

  const onClick = e => {
    e.preventDefault();
    refresh();
  }

  return (
    <a href='#' onClick={onClick}>Refresh</a>
  );
}

export default connect(null, mapDispatchToProps)(RefreshLink);
