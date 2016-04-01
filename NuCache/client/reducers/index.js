const packages = (state = [], action) => {

  switch (action.type) {
    case "SET_STATE":
      return action.state;

    case "REMOVE":
      return state.filter(p => p.name !== action.name || p.version !== action.version);

    default:
      return state;

  }
}

export default packages
