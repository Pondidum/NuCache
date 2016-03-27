import React from 'react'

import PackageTile from './PackageTile'

const PackageList = ({ packages }) => (
  <div className="row">
  { packages.map((p, i) => <PackageTile key={i} />) }
  </div>
);

export default PackageList
