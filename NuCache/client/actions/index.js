export const remove = (name, version) => {
  return {
    meta: {
      remote: true,
      url: "/api/packages/",
      method: "DELETE"
    },
    type: "REMOVE",
    name: name,
    version: version
  }
}

export const setState = (state) => {
  return {
    type: "SET_STATE",
    state: state
  }
}
