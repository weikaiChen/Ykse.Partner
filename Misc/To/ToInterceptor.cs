
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ykse.Partner
{
	using ROD = ReadOnlyDictionary<string, IToInterceptor>;

	internal class ToInterceptor
	{
		internal static ROD _Interceptors;

		internal static void Collect()
		{
			_Interceptors = new ROD(
				Reflector.CollectImplementedObject<IToInterceptor>()
			);
		}

		internal static IToInterceptor Get(Type fromType)
		{
			if(_Interceptors.NullOrEmpty()) { return null; }

			var interceptor = _Interceptors.Values.FirstOrDefault(x =>
				x.FromType == fromType
			);
			return interceptor;
		}
	}
}
