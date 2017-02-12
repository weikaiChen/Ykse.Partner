

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Ykse.Partner
{
	public static partial class ToExtender
	{
		private static bool TryFromString(
			string value,
			XInfo from,
			XInfo to,
			out object result,
			string format)
		{
			result = GetDefaultValue(to.Category);

			decimal decimalVal;
			var decimalFlag = decimal.TryParse(value, out decimalVal);

			switch(to.Category) {
				case TypeCategory.Array:
					if(to.IsBytes) {
						result = IsToConfig.Encoding.GetBytes(value);
						return true;
					}
					var firstElement = to
						.ElementInfos
						.FirstOrDefault();
					if(
						null != firstElement
						&&
						firstElement.Category
						==
						TypeCategory.Class) {
						if(!IsJson(value)) { return false; }
						result = JsonConvert.DeserializeObject(
							value,
							to.Type
						);
						return true;
					}
					return TryToArray(
						from,
						to,
						value,
						out result,
						format
					);

				case TypeCategory.String:
					result = value;
					return true;

				case TypeCategory.Stream:
					result = new MemoryStream(value.To<byte[]>());
					return true;

				case TypeCategory.Enum:
					result = IsToConverter.FromString.ToEnum(
						to.Type,
						value
					);
					return true;

				case TypeCategory.Class:
				case TypeCategory.Struct:
					if(IsJson(value)) {
						result = JsonConvert.DeserializeObject(
							value,
							to.Type
						);
						return true;
					}
					return false;

				case TypeCategory.DateTime:
					DateTime val;
					if(DateTime.TryParse(value, out val)) {
						result = val;
						return true;
					} else {
						int[] points;
						if(TryParseDate(value, out points)) {
							try {
								var millisecond = points[6]
									.To<string>()
									.Left(3)
									.To<int>();
								result = new DateTime(
									ToExtender.Enclosed(points[0], 1, 9999),
									ToExtender.Enclosed(points[1], 1, 12),
									ToExtender.Enclosed(points[2], 1, 31),
									ToExtender.Enclosed(points[3], 0, 23),
									ToExtender.Enclosed(points[4], 0, 59),
									ToExtender.Enclosed(points[5], 0, 59),
									millisecond
								);
							} catch(Exception ex) {
								LogRecord
									.Create()
									//.Add("Value", value)
									//.Add("From", from.FullName)
									//.Add("To", to.FullName)
									//.Add(
									//	"Points",
									//	points
									//		.Select(x => x.ToString())
									//		.Join(Symbol.CommaSpace)
									//)
									//.Add(ex)
									.Error();
								return false;
							}
							return true;
						} else {
							return false;
						}
					}

				case TypeCategory.Boolean:
					if(IsNumeric(value)) {
						var i = value.To<int>();
						result = i > 0;
						return true;
					}

					foreach(var one in IsToConfig.TrueStringArray) {
						if(one.Equals(
							value,
							StringComparison.OrdinalIgnoreCase)) {
							result = true;
							break;
						}
					}
					return true;

				case TypeCategory.Char:
					result = Convert.ToChar(value);
					return true;

				case TypeCategory.Decimal:
					result = decimalVal;
					return decimalFlag;
				case TypeCategory.Byte:
					if(!decimalFlag) { return false; }
					byte byteVal;
					if(decimalVal.TryTo<byte>(out byteVal)) {
						result = byteVal;
						return true;
					} else {
						return false;
					}
				case TypeCategory.SByte:
					if(!decimalFlag) { return false; }
					sbyte sbyteVal;
					if(decimalVal.TryTo<sbyte>(out sbyteVal)) {
						result = sbyteVal;
						return true;
					} else {
						return false;
					}
				case TypeCategory.Int16:
					if(!decimalFlag) { return false; }
					Int16 int16Val;
					if(decimalVal.TryTo<Int16>(out int16Val)) {
						result = int16Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.UInt16:
					if(!decimalFlag) { return false; }
					UInt16 uint16Val;
					if(decimalVal.TryTo<UInt16>(out uint16Val)) {
						result = uint16Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.Int32:
					if(!decimalFlag) { return false; }
					Int32 int32Val;
					if(decimalVal.TryTo<Int32>(out int32Val)) {
						result = int32Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.UInt32:
					if(!decimalFlag) { return false; }
					UInt32 uint32Val;
					if(decimalVal.TryTo<UInt32>(out uint32Val)) {
						result = uint32Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.Int64:
					if(!decimalFlag) { return false; }
					Int64 int64Val;
					if(decimalVal.TryTo<Int64>(out int64Val)) {
						result = int64Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.UInt64:
					if(!decimalFlag) { return false; }
					UInt64 uint64Val;
					if(decimalVal.TryTo<UInt64>(out uint64Val)) {
						result = uint64Val;
						return true;
					} else {
						return false;
					}
				case TypeCategory.Single:
					if(!decimalFlag) { return false; }
					Single singleVal;
					if(decimalVal.TryTo<Single>(out singleVal)) {
						result = singleVal;
						return true;
					} else {
						return false;
					}
				case TypeCategory.Double:
					if(!decimalFlag) { return false; }
					Double doubleVal;
					if(decimalVal.TryTo<Double>(out doubleVal)) {
						result = doubleVal;
						return true;
					} else {
						return false;
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
