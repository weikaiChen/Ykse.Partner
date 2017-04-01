using System;
using System.IO;
using System.Diagnostics;
using System.Web.Hosting;
using System.Web;
namespace Ykse.Partner
{
	public class RunTime
	{
		protected static object _Lock = new object();

		#region property
		private static string _BinFolder;
		public static string BinFolder
		{
			get
			{
				if(null == _BinFolder) {
					lock(_Lock) {
						if(null == _BinFolder) {
							_BinFolder = IsWebApp
								? Path.Combine(
									AppDomain.CurrentDomain.BaseDirectory,
									Constants.Folder.Bin
								)
								: AppDomain.CurrentDomain.BaseDirectory;
						}
					}
				}
				return _BinFolder;
			}
		}

		private static bool _IsWebApp = false;
		public static bool IsWebApp
		{
			get
			{
				if(_IsWebApp) {
					return _IsWebApp;
				} else {
					if(null == HttpContext.Current) {
						return HostingEnvironment.IsHosted;
					} else {
						return true;
					}
				}
			}
		}


	

		#endregion
		public static StackFrame CalleeStackFrameBeyond(string partialName)
		{
			var st = new StackTrace();
			var found = false;
			foreach(var frame in st.GetFrames()) {
				var method = frame.GetMethod();
				var type = method.ReflectedType;
				if(null == type) {
					continue;
				} else {
					var assemblyName = type.Assembly.GetName().Name;
					var className = type.Name;
					var methodName = method.Name;

					var fullName = string.Format(
						"{0}.{1}.{2}",
						assemblyName,
						className,
						methodName
					);
					if(fullName.Contains(partialName)) {
						if(found) { continue; }
						found = true;
					} else {
						if(!found) { continue; }
						return frame;
					}
				}
			}
			return null;
		}
	}
}
