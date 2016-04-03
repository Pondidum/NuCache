import React from 'react'
import { Component } from 'react'

import { connect } from 'react-redux'

import PackageList from './PackageList'
import Filter from './Filter'

const mapStateToProps = (state) => {
  return {
    packages: state
  }
}

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
      packages = packages.filter(p => p.name.search(exp) != -1);

    return (
      <div>
        <div className="row" style={{ marginBottom: "1em" }}>
          <div className="col-md-4 col-md-offset-8">
            <Filter onChange={ value => this.setState({ filter: value }) } />
          </div>
        </div>
        <PackageList packages={packages} />
      </div>
    );
  }
}

export default connect(mapStateToProps, null)(FilteredPackageList)
