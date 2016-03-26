import React from 'react'
import { Input } from 'react-bootstrap'

const Filter = () => (
  <div className="row" style={{ marginBottom: "1em" }}>
    <div className="col-md-4 col-md-offset-8">
      <Input type="text" placeholder="filter..." standalone />
    </div>
  </div>
);

export default Filter;
