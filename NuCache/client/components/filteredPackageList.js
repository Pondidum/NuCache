import React from 'react'
import { Component } from 'react'

import PackageList from './PackageList'
import Filter from './Filter'

class FilteredPackageList extends Component {

  constructor() {
    super();
    this.state = { filter: "" };
  }

  render() {

    var hasFilter = this.state.filter !== "";

    var exp = new RegExp(this.state.filter, "i");

    var packages = this.props.packages;

    if (hasFilter)
      packages = packages.filter(p => {
        var isName = p.name.search(exp) != -1;
        var isDesc = p.description.search(exp) != -1;

        return isName || isDesc;
      });

    console.log(packages);
    return (
      <div>
        <Filter onChange={ value => this.setState({ filter: value }) } />
        <PackageList packages={packages} />
      </div>
    );
  }
}

export default FilteredPackageList
