import React from 'react'

import FilteredPackageList from './FilteredPackageList'

const packages = [
  { name: "Finite", description: "Finite State Machine", version: "3.5.2" }
];

const App = () => (
  <div>
    <h1 className="text-center">NuCache</h1>
    <FilteredPackageList packages={packages}/>
  </div>
);

export default App
