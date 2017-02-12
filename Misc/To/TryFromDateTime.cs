﻿

using System;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromDateTime(
			DateTime value,
			XInfo from,
			XInfo to,
			out object result,
			string format)
		{
			result = GetDefaultValue(to.Category);

			switch(to.Category) {
				case TypeCategory.Array:
					if(to.IsBytes) {
						result = value.ToBinary().To<byte[]>();
						return true;
					}
					return TryToArray(
						from,
						to,
						value,
						out result,
						format
					);
				case TypeCategory.DateTime:
					result = value;
					return true;

				case TypeCategory.String:
				case TypeCategory.Stream:
					var s = value.ToString(
						string.IsNullOrEmpty(format)
							? "yyyy-MM-dd HH:mm:ss.fffffff"
							: format
					);
					return TryFromString(
						s,
						XInfo.String.Value,
						to,
						out result,
						format
					);

				case TypeCategory.Char:
					result = Convert.ToChar(value);
					return true;

				case TypeCategory.Int64:
					result = value.ToBinary();
					return true;

				case TypeCategory.Decimal:
				case TypeCategory.Byte:
				case TypeCategory.SByte:
				case TypeCategory.Int16:
				case TypeCategory.UInt16:
				case TypeCategory.Int32:
				case TypeCategory.UInt32:
				case TypeCategory.UInt64:
				case TypeCategory.Single:
				case TypeCategory.Double:
					return false;

				case TypeCategory.Boolean:
				case TypeCategory.Enum:
				case TypeCategory.Color:
				case TypeCategory.Interface:
				case TypeCategory.Class:
				case TypeCategory.Struct:
				case TypeCategory.IntPtr:
				case TypeCategory.UIntPtr:
				case TypeCategory.Null:
				case TypeCategory.Others:
				default:
					return false;
			}
		}
	}
}
