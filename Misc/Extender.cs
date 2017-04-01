using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Text.RegularExpressions;
using System.IO;
namespace Ykse.Partner
{
	public static class Extender
	{
		private static object _Lock = new object();

		public static bool In<T>(this T value, params T[] values)
		{
			if(null == value) { return false; }
			if(values.NullOrEmpty()) { return false; }
			foreach(T x in values) {
				if(value.Equals(x)) { return true; }
			}
			return false;
		}

		public static bool NullOrEmpty<T>(this T[] objs)
		{
			if(null == objs || objs.Length == 0) { return true; }

			foreach(var one in objs) {
				if(null != one) { return false; }
			}
			return true;
		}

		public static string ToJson(
			this object obj,
			Newtonsoft.Json.Formatting formatting)
		{
			return JsonConvert.SerializeObject(obj, formatting);
		}

		public static string ToJson(
		this object obj)
		{
			return obj.ToJson(Newtonsoft.Json.Formatting.None);
		}
		#region MethodBase
		public static string GetFriendlyName(this Type type)
		{
			try {
				if(null == type) { return string.Empty; }
				if(type.IsGenericParameter) { return type.Name; }
				if(!type.IsGenericType) { return type.FullName; }

				var sb = new StringBuilder();
				var name = type.Name;
				var index = name.IndexOf(Symbol.BackQuoter);
				sb.AppendFormat(
					"{0}.{1}",
					type.Namespace,
					name.Substring(0, index)
				);
				sb.Append(Symbol.LessThan);
				var first = true;
				foreach(var arg in type.GetGenericArguments()) {
					if(!first) {
						sb.Append(Symbol.CommaSpace);
					}
					sb.Append(arg.GetFriendlyName());
					first = false;
				}
				sb.Append(Symbol.GreaterThan);
				return sb.ToString();
			} catch(Exception ex) {
				LogRecord
					.Create("Extender")
					.SetMessage("Type.GetFriendlyName()")
					//.Add(ex)
					.Error();
				return string.Empty;
			}
		}

		public static string Path(this MethodBase method)
		{
			if(null == method) {
				LogRecord
					.Create("Extender")
					.SetMessage("MethodBase.Path(), MethodBase is null.")
					.Error();
				return string.Empty;
			}

			var type = method.ReflectedType;
			var methodName = method.Name;
			if(null == type) { return methodName; }

			var friendlyName = type.GetFriendlyName();
			var path = string.Format("{0}.{1}", friendlyName, methodName);
			return path;
		}
		public static string Description(this MethodBase method)
		{
			var desc = method
				.GetCustomAttribute<DescriptionAttribute>();
			if(null != desc) { return desc.Description; }
			if(null == method.ReflectedType) {
				return method.Name;
			}

			var parts = new List<string>();
			var classType = method.ReflectedType;
			desc = classType
				.GetCustomAttribute<DescriptionAttribute>();
			if(null == desc) {
				parts.Add(classType.Name);
			} else {
				parts.Add(desc.Description);
			}
			parts.Add(method.Name);

			return parts.Join(Symbol.Period);
		}
		#endregion

		#region Type
		public static DataFormat GetFormat(this Type type)
		{
			if(null == type) { return DataFormat.Unknown; }

			if(type.IsArray) {
				if(type.GetElementType().IsByte()) {
					return DataFormat.ByteArray;
				} else {
					return DataFormat.Objects;
				}
			}

			if(type.IsString()) { return DataFormat.String; }
			if(type.IsDateTime()) { return DataFormat.DateTime; }
			if(type.IsBoolean()) { return DataFormat.Boolean; }
			if(type.IsChar()) { return DataFormat.Char; }
			if(type.IsByte()) { return DataFormat.Byte; }
			if(type.IsShort()) { return DataFormat.Short; }
			if(type.IsInteger()) { return DataFormat.Integer; }
			if(type.IsLong()) { return DataFormat.Long; }
			if(type.IsFloat()) { return DataFormat.Float; }
			if(type.IsDouble()) { return DataFormat.Double; }
			if(type.IsDecimal()) { return DataFormat.Decimal; }
			if(type.IsEnum) { return DataFormat.Enum; }
			if(type.IsGuid()) { return DataFormat.Guid; }

			return DataFormat.Object;
		}

		public static bool IsChar(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(char));
		}

		public static bool IsBoolean(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Boolean));
		}

		public static bool IsDateTime(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(DateTime));
		}
		public static bool IsString(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(string));
		}

		public static bool IsByte(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Byte));
		}

		public static bool IsShort(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Int16));
		}

		public static bool IsInteger(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Int32));
		}

		public static bool IsLong(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Int64));
		}

		public static bool IsFloat(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Single));
		}

		public static bool IsDouble(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Double));
		}

		public static bool IsDecimal(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Decimal));
		}

		public static bool IsBytes(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(byte[]));
		}

		public static bool IsEnum(this Type type)
		{
			if(null == type) { return false; }
			return type.IsEnum;
		}

		public static bool IsStream(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Stream));
		}

		public static bool IsGuid(this Type type)
		{
			if(null == type) { return false; }
			return type.Equals(typeof(Guid));
		}

		public static List<Type> GatherByInterface<T>(
			this Assembly assembly)
			where T : class
		{
			return assembly.GatherByInterface(typeof(T));
		}

		public static List<Type> GatherByInterface(
			this Assembly assembly,
			Type type)
		{
			if(null == assembly) { return new List<Type>(); }

			var types = new Type[0];
			try {
				types = Reflector.GetTypes(assembly);
			} catch(Exception ex) {
				var record = LogRecord.Create("GatherByInterface");
				//record.Add("AssemblyFullName", assembly.FullName);
				var e = ex as ReflectionTypeLoadException;
				if(null != e) {
					foreach(var x in e.LoaderExceptions) {
						//record.Add("LoadExceptions", x.Message);
					}
				}
				//record.Add(ex).Error();
			}

			if(null == types || types.Length == 0) {
				return new List<Type>();
			}

			var interfaceName = type.Name;
			var list = new List<Type>();
			foreach(var x in types) {
				if(!x.IsClass || x.IsAbstract) { continue; }
				if(!x.IsDerived(type)) { continue; }
				list.Add(x);
			}
			return list;
		}

		public static bool IsDerived(this Type type, Type baseType)
		{
			if(null == type) { return false; }
			if(null == baseType) { return false; }
			if(baseType.FullName == type.FullName) { return true; }

			if(type.IsClass) {
				return baseType.IsClass
					? type.IsSubclassOf(baseType)
					: baseType.IsInterface
						? type.IsImplemented(baseType)
						: false;
			} else if(type.IsInterface && baseType.IsInterface) {
				return type.IsImplemented(baseType);
			}
			return false;
		}

		public static bool IsImplemented(this Type type, Type baseType)
		{
			if(null == type) { return false; }
			if(null == baseType) { return false; }
			if(!baseType.IsInterface) { return false; }

			var faces = type.GetInterfaces();
			foreach(var x in faces) {
				if(baseType.FullName.Equals(x.FullName)) {
					return true;
				}
			}
			return false;
		}


		#endregion
		#region IDictionary
		public static bool SafeAdd<TKey, TValue>(
			this IDictionary<TKey, TValue> dict,
			TKey key, TValue value)
		{
			if(null == dict) { return false; }
			lock(_Lock) {
				bool containsKey = dict.ContainsKey(key);
				if(containsKey) {
					dict[key] = value;
				} else {
					dict.Add(key, value);
				}
				return containsKey;
			}
		}
		#endregion
		#region IEnumerable
		public static bool NullOrEmpty<T>(
			this IEnumerable<T> enumerable)
		{
			try {
				return enumerable == null || enumerable.Count() == 0;
			} catch {
				return true;
			}
		}

		public static string Join(this string[] cols, string separator)
		{
			return cols.Join(separator, string.Empty);
		}

		public static string Join(
			this string[] cols,
			string separator,
			string quoter)
		{
			return cols.Join(separator, quoter, quoter);
		}

		public static string Join(
			this string[] cols,
			string separator,
			string quoter0,
			string quoter9)
		{
			return cols.Join(null, separator, quoter0, quoter9);
		}

		public static string Join(
			this object[] cols,
			Func<object, string> transfer,
			string separator,
			string quoter0,
			string quoter9)
		{
			if(cols.NullOrEmpty()) { return string.Empty; }
			if(separator.NullOrEmpty()) { separator = string.Empty; }
			if(quoter0.NullOrEmpty()) { quoter0 = string.Empty; }
			if(quoter9.NullOrEmpty()) { quoter9 = string.Empty; }

			var sb = new StringBuilder();
			foreach(var x in cols) {
				var val = string.Empty;
				if(null == transfer) {
					val = x.ToStringX();
				} else {
					try {
						val = transfer(x);
					} catch(Exception ex) {
						LogRecord
							.Create("Extender")
							.Error();
					}
				}
				sb.Append(quoter0);
				sb.Append(val);
				sb.Append(quoter9);
				sb.Append(separator);
			}

			return sb.ToString().TrimEnd(separator);
		}


		public static string Join(
		this IEnumerable<string> list,
		string separator)
		{
			if(list.NullOrEmpty()) { return string.Empty; }
			return list.ToArray().Join(separator);
		}
		#endregion

		#region object
		public static string ToStringX(this object value)
		{
			return value.ToStringX(string.Empty);
		}

		public static string ToStringX(
				this object value, string airbag)
		{
			if(null == value) { return airbag; }
			Type type = value.GetType();

			//if(type.IsEnum()) {
			//	return EnumCache.Get(type).Get(value).FinalValue;
			//} else 
			if(type.Is<DateTime>()) {
				return value.To<DateTime>().ToString("yyyy-MM-dd HH:mm:ss.fff");
				//return value.To<DateTime>().yyyy_MM_dd_HH_mm_ss_fff();
			} else if(type.Is<bool>()) {
				return value.To<bool>() ? "True" : "False";
			} else if(type.Is<string>()) {
				return value as string;
			} else if(type.Is<char>()) {
				return value.ToString();
			} else if(type.Is<byte>()) {
				return string.Format(
					"{0:#0}",
					value.To<byte>()
				);
			} else if(type.Is<short>()) {
				return string.Format(
					"{0:#0}",
					value.ToString()
				);
			} else if(type.Is<int>()) {
				return string.Format(
					"{0:#0}",
					value.ToString()
				);
			} else if(type.Is<long>()) {
				return string.Format(
					value.ToString()
				);
			} else if(type.Is<float>()) {
				return string.Format(
					"{0:N7}",
					value.ToString()
				);
			} else if(type.Is<double>()) {
				return string.Format(
					"{0:N7}",
					value.ToString()
				);
			} else if(type.Is<decimal>()) {
				return string.Format(
					"{0:N7}",
					value.ToString()
				);
			//} else if(type.Is<Stream>()) {
			//	return value.To<Stream>().ToString();
			//} else if(type.Is<Guid>()) {
			//	return value.To<Guid>().ToString();
			//} else if(type.Is<Color>()) {
			//	return value.To<Color>().ToHtml();
			} else if(type.IsArray) {
				var ary = value as Array;
				if(null == ary) { return value.ToString(); }
				var list = new List<string>();
				foreach(var one in ary) {
					list.Add(one.ToStringX());
				}
				return list.Join(", ");
			} else {
				return value.ToString();
			}
		}
		#endregion

		#region string

		public static bool StartsX(
			this string value,
			StringComparison comparison,
			params string[] values)
		{
			if(null == values) { return false; }

			foreach(string x in values) {
				if(null == x && null == value) { return true; }
				if(null == x || null == value) { return false; }
				if(value.StartsWith(x, comparison)) {
					return true;
				}
			}
			return false;
		}

		public static bool StartsX(
			this string value, params string[] values)
		{
			return value.StartsX(
				StringComparison.OrdinalIgnoreCase, values
			);
		}

		public static string TrimEnd(
			this string value,
			params string[] trimStrings)
		{
			return value.TrimEnd(
				StringComparison.OrdinalIgnoreCase, trimStrings
			);
		}

		public static string TrimEnd(
			this string value,
			StringComparison comparison,
			params string[] trimStrings)
		{
			if(value.NullOrEmpty()) { return value; }
			foreach(string trimString in trimStrings) {
				if(trimString.Length > value.Length) { continue; }
				if(value.EndsX(comparison, trimString)) {
					return value.Substring(
						0,
						value.Length - trimString.Length
					);
				}
			}
			return value;
		}

		public static bool EndsX(
			this string value,
			StringComparison comparison,
			params string[] values)
		{
			if(null == values) { return false; }

			foreach(string x in values) {
				if(null == x && null == value) { return true; }
				if(null == x || null == value) { return false; }
				if(value.EndsWith(x, comparison)) {
					return true;
				}
			}
			return false;
		}

		public static bool EndsX(
			this string value, params string[] values)
		{
			return value.EndsX(
				StringComparison.OrdinalIgnoreCase, values
			);
		}

		public static bool NullOrEmpty(this string str)
		{
			return null == str || string.IsNullOrEmpty(str);
		}

		public static bool NullOrWhiteSpace(this string txt)
		{
			return string.IsNullOrWhiteSpace(txt);
		}

		public static string Left(this string input, int length)
		{
			if(input.NullOrEmpty()) { return string.Empty; }
			if(length < 0) { return string.Empty; }
			string left = input.Substring(
				0,
				Math.Min(length, input.Length)
			);
			return left;
		}

		public static string Right(this string input, int length)
		{
			if(input.NullOrEmpty()) { return string.Empty; }
			if(length >= input.Length) { return input; }
			if(length < 0) { return string.Empty; }
			return input.Substring(input.Length - length, length);
		}

		public static string[] Split(
			this string input, string pattern, RegexOptions options)
		{
			if(input.NullOrEmpty()) { return new string[0]; }
			var parts = Regex.Split(input, pattern, options);
			List<string> list = new List<string>();
			foreach(string x in parts) {
				list.Add(x);
			}
			return list.ToArray();
		}

		public static string[] Split(
			this string input, params string[] symbols)
		{
			if(input.NullOrEmpty()) { return new string[0]; }
			var parts = input.Split(
				symbols,
				StringSplitOptions.None
			);
			List<string> list = new List<string>();
			foreach(string x in parts) {
				list.Add(x);
			}
			return list.ToArray();
		}

		public static string[] SplitAndTrim(
			this string input, string pattern, RegexOptions options)
		{
			if(input.NullOrEmpty()) { return new string[0]; }
			var parts = Regex.Split(input, pattern, options);
			List<string> list = new List<string>();
			foreach(string x in parts) {
				list.Add(x.Trim());
			}
			return list.ToArray();
		}

		public static string[] SplitAndTrim(
			this string input, params string[] symbols)
		{
			return input.SplitAndTrim(false, symbols);
		}

		public static string[] SplitAndTrim(
			this string input,
			bool ignorEmpty,
			params string[] symbols)
		{
			if(input.NullOrEmpty()) { return new string[0]; }
			var parts = input.Split(
				symbols,
				StringSplitOptions.RemoveEmptyEntries
			);
			List<string> list = new List<string>();
			foreach(string x in parts) {
				if(ignorEmpty && x.NullOrEmpty()) { continue; }
				list.Add(x.Trim());
			}
			return list.ToArray();
		}

		public static string Airbag(this string value)
		{
			return Airbag(value, string.Empty);
		}

		public static string Airbag(this string value, string airbag)
		{
			if(value.NullOrEmpty()) {
				if(airbag.NullOrEmpty()) {
					return string.Empty;
				} else {
					return airbag;
				}
			} else {
				return value;
			}
		}

		public static string Printf(
			this string format,
			params object[] parameters)
		{
			var pattern = @"%(?<Spec>\d+(\.\d+)?)?(?<Type>d|f|s)";
			var count = parameters.NullOrEmpty()
				? 0
				: parameters.Length;
			var index = 0;
			var result = Regex.Replace(
				format,
				pattern,
				m => {
					var spec = m.Groups["Spec"].Value.Airbag();
					var type = m.Groups["Type"].Value.Airbag();
					if(type.In("d", "f", "s")) {
						if(index < count) {
							return parameters[index++].To<string>();
						} else {
							return "{?}";
						}
					} else {
						return string.Concat("%", spec, type);
					}
				}
			);

			return result;
		}
		#endregion

		#region DateTime

		public static string yyyy_MM_dd_HH_mm_ss(this DateTime date)
		{
			return date.ToString("yyyy-MM-dd HH:mm:ss");
		}

		#endregion
	}
}
