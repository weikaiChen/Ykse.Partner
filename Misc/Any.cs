using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ykse.Partner
{
	[Serializable, DataContract]
	[DebuggerDisplay("{Name}({Format.ToString()}) = {Value}")]
	public class Any
	{
		#region constructor
		public Any()
			: this(string.Empty, string.Empty)
		{
		}

		public Any(string name)
			: this(name, string.Empty)
		{
		}

		public Any(string name, params object[] values)
		{
			this.Name = name;
			if(null == values) {
				this.Value = values;
			} else {
				if(values.Length == 1) {
					this.Value = values[0];
				} else {
					this.Value = values;
				}
			}
		}
		#endregion

		#region property
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public object Value { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public DataFormat Format
		{
			get
			{
				if(null != Value) {
					return Value.GetType().GetFormat();
				}
				return DataFormat.Unknown;
			}
		}
		#endregion

		#region ToXxx
		public override string ToString()
		{
			return ToString(string.Empty);
		}

		public string ToString(string airbag)
		{
			switch(Format) {
				case DataFormat.Unknown:
				case DataFormat.String:
					return null == Value ? airbag : Value.ToString();
				case DataFormat.DateTime:
					return Value.To<DateTime>().yyyy_MM_dd_HH_mm_ss();
				case DataFormat.Boolean:
					return Value.To<bool>().ToString().ToLower();
				case DataFormat.Char:
					return null == Value ? airbag : Value.ToString();
				case DataFormat.Byte:
					return Value.To<byte>().ToString();
				case DataFormat.Short:
					return Value.To<short>().ToString();
				case DataFormat.Integer:
					return Value.To<int>().ToString();
				case DataFormat.Long:
					return Value.To<long>().ToString();
				case DataFormat.Float:
					return Value.To<float>().ToString("N7");
				case DataFormat.Double:
					return Value.To<double>().ToString("N7");
				case DataFormat.Decimal:
					return Value.To<decimal>().ToString("N7");
				case DataFormat.Enum:
					return Value.ToString();
				case DataFormat.Object:
					return null == Value ? airbag : Value.ToString();
				case DataFormat.Objects:
					return Value.ToStringX();
				case DataFormat.ByteArray:
					return Value.To<byte[]>().ToString();
				//case DataFormat.Color:
				//	return Value.To<Color>().ToString();
				case DataFormat.Guid:
					return Value.To<Guid>().ToString();
				case DataFormat.TimeSpan:
					return Value.To<TimeSpan>().ToString();

				default:
					return null == Value ? airbag : Value.ToString();
			}
		}

		//public T ToEnum<T>()
		//{
		//	return ToEnum<T>(default(T));
		//}

		//public T ToEnum<T>(T airbag)
		//{
		//	var type = typeof(T);

		//	if(Format == DataFormat.Enum) {
		//		return (T)Enum.Parse(type, Value.ToString(), true);
		//	} else {
		//		if(type.IsEnum) {
		//			var ef = EnumCache.Get(type);
		//			var ei = ef.Get(ToString());

		//			return null == ei
		//				? ef.DefaultValue.To<T>(airbag)
		//				: ei.ToEnum<T>();
		//		}
		//		return airbag;
		//	}
		//}

		//public void DemoteToPrimitive()
		//{
		//	if(null == Value) { return; }
		//	var type = Value.GetType();
		//	if(type.IsPrimitive()) { return; }

		//	try {
		//		var json = Value.ToJson();
		//		Value = string.Concat(
		//			type.FullName,
		//			" = ",
		//			json
		//		);
		//	} catch(Exception ex) {
		//		Value = string.Concat(
		//			type.FullName,
		//			" = (Error occurred on converting to json) ",
		//			ex.Message
		//		);
		//	}
		//}

		public T To<T>()
		{
			return To<T>(default(T));
		}

		public T To<T>(T airbag)
		{
			if(null == Value) { return airbag; }
			return Value.To<T>();
		}

		//public Many ToMany(string group)
		//{
		//	var many = new Many(group, Name, Value);
		//	return many;
		//}

		//public Much ToMuch(string category, string group)
		//{
		//	var more = new Much(category, group, Name, Value);
		//	return more;
		//}

		//public More ToMore(string scope, string category, string group)
		//{
		//	var more = new More(scope, category, group, Name, Value);
		//	return more;
		//}
		//#endregion

		//#region public
		//public string Join(string connector = Symbol.Equal)
		//{
		//	var val = string.Concat(Name, connector, ToString());
		//	return val;
		//}
		#endregion

		#region Concat
		//public static List<Any> Concat(params Any[] anys)
		//{
		//	List<Any> list = new List<Any>();
		//	if(!anys.NullOrEmpty()) {
		//		foreach(var any in anys) {
		//			list.SafeAdd(any);
		//		}
		//	}
		//	return list;
		//}
		#endregion

		#region override
		public override bool Equals(object obj)
		{
			if(null == obj) { return false; }
			Any any = obj as Any;
			if(null == any) { return false; }
			return this.Name.Equals(
				any.Name, StringComparison.OrdinalIgnoreCase
			);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	[Serializable, DataContract]
	public class Any<T> : Any
	{
		[DataMember]
		public T Other { get; set; }
	}
}
