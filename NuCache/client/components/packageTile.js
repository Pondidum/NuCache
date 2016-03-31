import React from 'react'

const PackageTile = ({ details }) => (
  <div className="list-group col-xs-4">
    <div className="list-group-item">
      <div className="row-action-primary">
        <i>1.4.5</i>
      </div>
      <div className="row-content">
        <div className="least-content"><a href="#" alt="Remove from cache"><i className="material-icons" style={{ verticalAlign: "middle" }}>delete</i></a></div>
        <h4 className="list-group-item-heading">{details.name}</h4>
        <p className="list-group-item-text">Downloads: {details.downloads}</p>
      </div>
    </div>
  </div>
)

export default PackageTile
