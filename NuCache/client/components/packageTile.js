import React from 'react'
import { connect } from 'react-redux'
import { remove } from '../actions'

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    remove: (name, version) => dispatch(remove(name, version)),
    details: ownProps.details
  }
}

const PackageTile = ({ details, remove }) => {

  const onClick = (e) => {
    e.preventDefault();
    remove(details.name, details.version);
  }

  return (
    <div className="list-group col-xs-4">
      <div className="list-group-item">
        <div className="row-action-primary">
          <i>{details.version}</i>
        </div>
        <div className="row-content">
          <div className="least-content">
            <a href="#" onClick={onClick}>
              <i className="material-icons" style={{ verticalAlign: "middle" }}>delete</i>
            </a>
          </div>
          <h4 className="list-group-item-heading">{details.name}</h4>
          <p className="list-group-item-text">Downloads: {details.downloads}</p>
        </div>
      </div>
    </div>
  )
}

export default connect(null, mapDispatchToProps)(PackageTile)
