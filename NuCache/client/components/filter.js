import React from 'react'
import { Input } from 'react-bootstrap'

const Filter = ({ onChange }) => {

  var input;

  return (
    <div className="row" style={{ marginBottom: "1em" }}>
      <div className="col-md-4 col-md-offset-8">
        <Input
          type="text"
          placeholder="filter..."
          ref={component => input = component}
          onChange={() => onChange(input.getValue()) } 
          standalone
        />
      </div>
    </div>
  );
}

export default Filter;
