
using System;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromUIntPtr(
			UIntPtr value,
			XInfo from,
			XInfo to,
			out object result,
			string format)
		{
			result = GetDefaultValue(to.Category);

			switch(to.Category) {
				case TypeCategory.Array:
					return TryToArray(
						from,
						to,
						value,
						out result,
						format
					);
				case TypeCategory.UIntPtr:
					result = value;
					return true;

				case TypeCategory.Enum:
				case TypeCategory.Interface:
				case TypeCategory.Class:
				case TypeCategory.Struct:
				case TypeCategory.Stream:
				case TypeCategory.Color:
				case TypeCategory.String:
				case TypeCategory.DateTime:
				case TypeCategory.Decimal:
				case TypeCategory.Boolean:
				case TypeCategory.Char:
				case TypeCategory.Byte:
				case TypeCategory.SByte:
				case TypeCategory.Int16:
				case TypeCategory.UInt16:
				case TypeCategory.Int32:
				case TypeCategory.UInt32:
				case TypeCategory.IntPtr:
				case TypeCategory.Int64:
				case TypeCategory.UInt64:
				case TypeCategory.Single:
				case TypeCategory.Double:
				case TypeCategory.Null:
				case TypeCategory.Others:
				default:
					return false;
			}
		}
	}
}
