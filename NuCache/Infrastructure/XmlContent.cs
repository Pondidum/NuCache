using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NuCache.Infrastructure
{
	public class XmlContent : HttpContent
	{
		private readonly MemoryStream _stream;

		public XmlContent(XDocument document)
		{
			_stream = new MemoryStream();
			document.Save(_stream);

			_stream.Position = 0;
			Headers.ContentType = new MediaTypeHeaderValue("application/xml");
		}

		protected override Task SerializeToStreamAsync(Stream stream, System.Net.TransportContext context)
		{
			_stream.CopyTo(stream);

			var tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		protected override bool TryComputeLength(out long length)
		{
			length = _stream.Length;
			return true;
		}
	}
}
