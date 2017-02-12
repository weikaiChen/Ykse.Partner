using System.ComponentModel;

namespace Ykse.Partner
{
	[DefaultValue(LogLevel.Info)]
	public enum LogLevel
	{
		[FinalValue("Debug")]
		All = 1,
		[FinalValue("Debug")]
		Debug = 2,
		[FinalValue("Info")]
		Info = 3,
		[FinalValue("Warn")]
		Warn = 4,
		[FinalValue("Error")]
		Error = 5,
		[FinalValue("Fatal")]
		Fatal = 6
	}
}
