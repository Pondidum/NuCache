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
);

const PackageList = () => (
  <div className="row">
    <PackageTile />
    <PackageTile />
    <PackageTile />
    <PackageTile />
    <PackageTile />
    <PackageTile />
    <PackageTile />
    <PackageTile />
  </div>
);

const App = () => (
  <div>
    <h1>NuCache</h1>
    <PackageList />
  </div>
);

export default App
