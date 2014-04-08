﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuCache.Infrastructure;

namespace NuCache.PackageSources
{
	public class ProxyingPackageSource : IPackageSource
	{
		private readonly ApplicationSettings _settings;
		private readonly WebClient _client;
		private readonly UriHostTransformer _transformer;

		public ProxyingPackageSource(ApplicationSettings settings, WebClient client, UriHostTransformer transformer)
		{
			_settings = settings;
			_client = client;
			_transformer = transformer;
		}

		private Uri GetTargetUrl(Uri request)
		{
			return _transformer.Transform(_settings.RemoteFeed, request);
		}

		public async Task<HttpResponseMessage> Get(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> Metadata(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> List(Uri request)
		{
			var response = await _client.GetResponseAsync(GetTargetUrl(request));

			using (var inputStream = await response.Content.ReadAsStreamAsync())
			{
				var doc = XDocument.Load(inputStream);
				var ns = doc.Root.Name.Namespace;

				var attributes = doc.Root
					.Elements(ns + "entry")
					.SelectMany(e => e.Elements(ns + "content"))
					.Select(e => e.Attribute("src"));

				foreach (var attribute in attributes)
				{
					var url = new Uri(attribute.Value);
					attribute.SetValue(_transformer.Transform(request, url));
				}

				var pushContent = new PushStreamContent((stream, content, context) =>
				{
					doc.Save(stream);
					stream.Close();
				}, response.Content.Headers.ContentType);

				response.Content = pushContent;
			}

			return response;
		}

		public async Task<HttpResponseMessage> Search(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> FindPackagesByID(Uri request)
		{
			return await _client.GetResponseAsync(GetTargetUrl(request));
		}

		public async Task<HttpResponseMessage> GetPackageByID(Uri request)
		{
			var result = await _client.GetResponseAsync(GetTargetUrl(request));

			//not certain why this gets missed by the web client on a download
			var name = Path.GetFileName(result.RequestMessage.RequestUri.AbsolutePath);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };

			return result;
		}
	}
}
