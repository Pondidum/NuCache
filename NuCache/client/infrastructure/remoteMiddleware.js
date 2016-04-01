const remoteMiddleware = store => next => action => {

  if (action.meta && action.meta.remote)
    $.ajax({
      url: "http://localhost:55628" + action.meta.url,
      method: action.meta.method,
      data: JSON.stringify(action)
    });

  next(action);
}

export default remoteMiddleware
