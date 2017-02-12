using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner.Provider
{
	public class ProviderSettings
	{
		public string Name { get; set; }
		public string ProviderName { get; set; }
		public string ConnectionString { get; set; }

		public static ProviderSettings GetProviderByName(string name)
		{
			throw new NotImplementedException();
		}
	}
}
