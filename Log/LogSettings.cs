using System.Configuration;

namespace Ykse.Partner
{
	public class LogSettings
	{
		public static string GetPath()
		{
			string path = "";
			path = ConfigurationManager.AppSettings["LogSettingPath"];
			return path;
		}
	}
}
