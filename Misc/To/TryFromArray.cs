﻿

using System;
using System.Linq;
using System.Text;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromArray(
			Array value,
			XInfo from,
			XInfo to,
			out object result,
			string format)
		{
			result = GetDefaultValue(to.Category);

			var bytes = from.IsBytes
				? value.Cast<byte>().ToArray()
				: new byte[0];

			switch(to.Category) {
				case TypeCategory.Array:
					return TryToArray(
						from,
						to,
						value,
						out result,
						format
					);

				case TypeCategory.DateTime:
					if(from.IsBytes) {
						if(bytes.Length == 8) {
							result = bytes.To<Int64>().To<DateTime>();
							return true;
						} else {
							var s = IsToConfig.Encoding.GetString(bytes);
							return TryFromString(
								s,
								XInfo.String.Value,
								to,
								out result,
								format
							);
						}
					}
					return false;

				case TypeCategory.Enum:
				case TypeCategory.Class:
				case TypeCategory.Struct:
				case TypeCategory.Stream:
				case TypeCategory.Color:
				case TypeCategory.String:
					if(from.IsBytes) {
						var str = string.Empty;
						switch(format.ToUpper()) {
							case "DEFAULT":
								str = Encoding.Default.GetString(bytes);
								break;
							case "ASCII":
								str = Encoding.ASCII.GetString(bytes);
								break;
							case "UNICODE":
								str = Encoding.Unicode.GetString(bytes);
								break;
							case "UTF7":
								str = Encoding.UTF7.GetString(bytes);
								break;
							case "UTF8":
								str = Encoding.UTF8.GetString(bytes);
								break;
							case "UTF32":
								str = Encoding.UTF32.GetString(bytes);
								break;
							default:
								str = IsToConfig.Encoding.GetString(bytes);
								break;
						}

						return TryFromString(
							str,
							XInfo.String.Value,
							to,
							out result,
							format
						);
					}
					return false;

				case TypeCategory.Byte:
					if(from.IsBytes) {
						if(bytes.Length == 0) {
							result = bytes[0];
							return true;
						}
					}
					return false;

				case TypeCategory.SByte:
					if(from.IsBytes) {
						if(bytes.Length == 0) {
							result = (SByte)bytes[0];
							return NumericCompare(value, result);
						}
					}
					return false;

				case TypeCategory.Decimal:
					if(bytes.Length != 16) {
						// A decimal must be created from exactly
						// 128 bits / 16 bytes
						return false;
					}
					var bits = new Int32[4];
					for(int i = 0; i <= 15; i += 4) {
						bits[i / 4] = BitConverter.ToInt32(bytes, i);
					}
					result = new decimal(bits);
					return true;

				case TypeCategory.Boolean:
					if(from.IsBytes) {
						result = BitConverter.ToBoolean(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Char:
					if(from.IsBytes) {
						result = BitConverter.ToChar(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Int16:
					if(from.IsBytes) {
						result = BitConverter.ToInt16(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.UInt16:
					if(from.IsBytes) {
						result = BitConverter.ToUInt16(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Int32:
					if(from.IsBytes) {
						result = BitConverter.ToInt32(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.UInt32:
					if(from.IsBytes) {
						result = BitConverter.ToUInt32(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Int64:
					if(from.IsBytes) {
						result = BitConverter.ToInt64(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.UInt64:
					if(from.IsBytes) {
						result = BitConverter.ToUInt64(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Single:
					if(from.IsBytes) {
						result = BitConverter.ToSingle(bytes, 0);
						return true;
					}
					return false;
				case TypeCategory.Double:
					if(from.IsBytes) {
						result = BitConverter.ToDouble(bytes, 0);
						return true;
					}
					return false;

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
