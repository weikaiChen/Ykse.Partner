using System;
namespace Ykse.Partner
{
	public partial class IsToConverter
	{
		public class FromString
		{
			// ToEnum
			private static Func<Type, string, object> _ToEnum;
			public static Func<Type, string, object> ToEnum
			{
				get
				{
					return null == _ToEnum
						? ToEnumDefault
						: _ToEnum;
				}
				set
				{
					_ToEnum = value;
				}
			}

			private static object ToEnumDefault(
				Type toType,
				string value)
			{
				var result = Enum.Parse(toType, value);
				return result;
			}
		}
	}
}
