﻿
using System;
using System.IO;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromDouble(
			double value,
			XInfo from,
			XInfo to,
			out object result,
			string format)
		{
			result = GetDefaultValue(to.Category);

			switch(to.Category) {
				case TypeCategory.Array:
					if(to.IsBytes) {
						result = BitConverter.GetBytes(value);
						return true;
					}
					return TryToArray(
						from,
						to,
						value,
						out result,
						format
					);
				case TypeCategory.Double:
					result = value;
					return true;

				case TypeCategory.Stream:
					var bytes = BitConverter.GetBytes(value);
					result = new MemoryStream(bytes);
					return true;

				case TypeCategory.Enum:
				case TypeCategory.String:
					var s = value.ToString();
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

				case TypeCategory.Boolean:
					result = value > 0;
					return true;

				case TypeCategory.Color:
					var i = (Int32)value;
					if(!NumericCompare(value, result)) {
						return false;
					}
					return TryFromInt32(
						i,
						XInfo.Int32.Value,
						to,
						out result,
						format
					);

				case TypeCategory.Decimal:
					result = (Decimal)value;
					return NumericCompare(value, result);
				case TypeCategory.Byte:
					checked { result = (Byte)value; }
					return NumericCompare(value, result);
				case TypeCategory.SByte:
					checked { result = (SByte)value; }
					return NumericCompare(value, result);
				case TypeCategory.Int16:
					checked { result = (Int16)value; }
					return NumericCompare(value, result);
				case TypeCategory.UInt16:
					checked { result = (UInt16)value; }
					return NumericCompare(value, result);
				case TypeCategory.Int32:
					checked { result = (Int32)value; }
					return NumericCompare(value, result);
				case TypeCategory.UInt32:
					checked { result = (UInt32)value; }
					return NumericCompare(value, result);
				case TypeCategory.Int64:
					checked { result = (Int64)value; }
					return NumericCompare(value, result);
				case TypeCategory.UInt64:
					checked { result = (UInt64)value; }
					return NumericCompare(value, result);
				case TypeCategory.Single:
					checked { result = (Single)value; }
					return NumericCompare(value, result);

				case TypeCategory.Interface:
				case TypeCategory.Class:
				case TypeCategory.Struct:
				case TypeCategory.DateTime:
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
