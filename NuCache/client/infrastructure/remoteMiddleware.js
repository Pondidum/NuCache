const remoteMiddleware = store => next => action => {

  if (action.meta && action.meta.remote)
    $.ajax({
      url: action.meta.url,
      method: action.meta.method,
      data: JSON.stringify(action),
      success: (data) => {
        if (action.meta.success)
          action.meta.success(store, data);
      }
    });

  next(action);
}

export default remoteMiddleware
