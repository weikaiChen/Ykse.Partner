

using System.ComponentModel;
using System.Xml.Serialization;

namespace Ykse.Partner
{
	[DefaultValue(DataFormat.Unknown)]
	[XmlType(Namespace = Constants.Xml.Namespace)]
	public enum DataFormat
	{
		Unknown,

		[Description("string")]
		String,

		[Description("DateTime")]
		DateTime,


		[Description("bool")]
		Boolean,

		[Description("char")]
		Char,

		[Description("byte")]
		Byte,

		[Description("short")]
		Short,

		[Description("int")]
		Integer,

		[Description("long")]
		Long,

		[Description("float")]
		Float,

		[Description("double")]
		Double,

		[Description("decimal")]
		Decimal,


		[Description("Enum")]
		Enum,


		[Description("object")]
		Object,

		[Description("object[]")]
		Objects,

		[Description("byte[]")]
		ByteArray,

		[Description("Color")]
		Color,

		[Description("Guid")]
		Guid,

		[Description("TimeSpan")]
		TimeSpan,
	}
}
