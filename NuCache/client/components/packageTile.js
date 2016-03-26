import React from 'react'

const PackageTile = () => (
  <div className="list-group col-xs-4">
    <div className="list-group-item">
      <div className="row-action-primary">
        <i className="material-icons">folder</i>
      </div>
      <div className="row-content">
        <div className="least-content">20</div>
        <h4 className="list-group-item-heading">Package Name Here</h4>
        <p className="list-group-item-text">Package description perhaps could go here? Or date added?</p>
      </div>
    </div>
  </div>
)

export default PackageTile
