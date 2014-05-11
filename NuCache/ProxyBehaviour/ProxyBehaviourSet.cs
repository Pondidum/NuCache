using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace NuCache.ProxyBehaviour
{
	public class ProxyBehaviourSet
	{
		private readonly List<IProxyBehaviour> _behaviours;

		public ProxyBehaviourSet(IEnumerable<IProxyBehaviour> behaviours)
		{
			_behaviours = behaviours.ToList();
		}

		public void Execute(HttpRequestMessage request, HttpResponseMessage response)
		{
			_behaviours.ForEach(b => b.Execute(request, response));
		}
	}
}
