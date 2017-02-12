using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public static partial class IsExtender
	{
		public static bool Is<T>(this object value)
		{
			if(null == value) { return false; }

			return typeof(T).IsAssignableFrom(
				value is Type
					? (Type)value
					: value.GetType()
			);
		}

		public static bool Is(this object value, Type type)
		{
			if(null == value) { return false; }
			if(null == type) { return false; }
			return type.IsAssignableFrom(
				value is Type
					? (Type)value
					: value.GetType()
			);
		}
	}
}
