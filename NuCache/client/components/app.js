import React from 'react'

import PackageList from './PackageList'
import Filter from './Filter'

const App = () => (
  <div>
    <h1 className="text-center">NuCache</h1>
    <Filter />
    <PackageList />
  </div>
);

export default App
