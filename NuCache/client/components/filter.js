import React from 'react'
import { Input } from 'react-bootstrap'

const Filter = ({ onChange }) => {
  var input;
  return (
    <Input
      type="text"
      placeholder="filter..."
      ref={component => input = component}
      onChange={() => onChange(input.getValue()) }
      standalone
    />
  );
}

export default Filter;
