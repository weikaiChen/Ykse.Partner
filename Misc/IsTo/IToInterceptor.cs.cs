using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public interface IToInterceptor
	{
		Type FromType { get; }

		bool TryTo(
			object value,
			Type toType,
			out object result,
			string format = ""
		);
	}
}
