﻿

using System.IO;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromStream(
			Stream value,
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
				case TypeCategory.Stream:
					result = value;
					return true;

				case TypeCategory.Enum:
				case TypeCategory.Color:
				case TypeCategory.Class:
				case TypeCategory.Struct:
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
				case TypeCategory.Int64:
				case TypeCategory.UInt64:
				case TypeCategory.Single:
				case TypeCategory.Double:
					using(var memoryStream = new MemoryStream()) {
						value.CopyTo(memoryStream);
						var bytes = memoryStream.ToArray();
						return TryFromArray(
							bytes,
							ToCache.Get(typeof(byte[])),
							to,
							out result,
							format
						);
					}

				case TypeCategory.Interface:
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
