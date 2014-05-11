using System.Collections.Generic;
using NuCache.Infrastructure.NuGet;

namespace NuCache.Models
{
	public class ManageViewModel
	{
		 public IEnumerable<PackageID> Packages {get; set; }
	}
}
