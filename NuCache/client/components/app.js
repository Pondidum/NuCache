import React from 'react'
import { Input } from 'react-bootstrap'

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

const Filter = () => (
  <div className="row" style={{ marginBottom: "1em" }}>
    <div className="col-md-4 col-md-offset-8">
      <Input type="text" placeholder="filter..." standalone />
    </div>
  </div>
);

const App = () => (
  <div>
    <h1 className="text-center">NuCache</h1>
    <Filter />
    <PackageList />
  </div>
);

export default App
