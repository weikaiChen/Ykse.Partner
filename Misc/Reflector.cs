using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Web.Hosting;
using System.IO;
using System.Web.Compilation;
namespace Ykse.Partner
{
	using DIC_T = IDictionary<string, Type>;
	using ROC_A = ReadOnlyCollection<Assembly>;
	public class Reflector
	{

		#region Assembly

		public static DateTime _AssemblyCollectedTime;
		private static ROC_A _Assemblies;
		private static object _AssemblyLock = new object();
		public static ROC_A Assemblies
		{
			get
			{
				if(null != _Assemblies) { return _Assemblies; }

				lock(_AssemblyLock) {
					if(null != _Assemblies) { return _Assemblies; }
					var assemblies = new List<Assembly>();

					using(var il = new ILogger("Reflector")) {
						il.Record.Message = "Collect Assemblies";
						try {
							// load from folder
							//il.Record.Add("Section", "load from folder");
							var files = Directory
								.GetFiles(RunTime.BinFolder)
								.Where(x =>
									!x.EndsX(".vshost.exe")
									&&
									x.EndsX(".dll", ".exe")
								);
							foreach(var file in files) {
								if(SkipAssembly(file)) { continue; }
								try {
									assemblies.Add(Assembly.LoadFrom(file));
								} catch {
									// swallow it
								}
							}

							// AppCode
							//il.Record.Add("Section", "AppCode");
							assemblies.AddRange(
								AppCodeAssemblies
							);

							// log
							//il.Record.Add(
							//	"Assemblies",
							//	assemblies
							//		.Select(x => x.FullName)
							//		.Join(Environment.NewLine)
							//);
						} catch(Exception ex) {
							//il.Record.Add(ex);
						}
					}

					_Assemblies = assemblies.AsReadOnly();
					_AssemblyCollectedTime = DateTime.Now;
				}

				return _Assemblies;
			}
		}

		public static bool SkipAssembly(string assemblyFile)
		{
			var name = Path.GetFileNameWithoutExtension(
				assemblyFile
			);


			// default skip
			if(name.StartsX(Constants.SkipAssemblyPrefixes)) {
				return true;
			}

		
			return false;
		}

		private static List<Assembly> _AppCodeAssemblies;
		public static List<Assembly> AppCodeAssemblies
		{
			get
			{
				if(
					null == _AppCodeAssemblies ||
					!_AppCodeAssemblies.Any()) {
					try {
						_AppCodeAssemblies = new List<Assembly>();

						if(HostingEnvironment.IsHosted) {
							if(null != BuildManager.CodeAssemblies) {
								_AppCodeAssemblies.AddRange(
									BuildManager
										.CodeAssemblies
										.OfType<Assembly>()
										.ToArray()
								);
							}
						}
					} catch { }
				}
				return _AppCodeAssemblies;
			}
		}

		#endregion
		#region Collect
		public static IDictionary<string, T> CollectImplementedObject<T>()
		where T : class
		{
			var list = new Dictionary<string, T>();
			foreach(var asm in Reflector.Assemblies) {
				var objects = CollectImplementedObject<T>(asm);
				if(objects.Count == 0) { continue; }
				foreach(var one in objects) {
					list.SafeAdd(one.Key, one.Value);
				}
			}
			return list;
		}

		public static IDictionary<string, T> CollectImplementedObject<T>(
			Assembly asm)
			where T : class
		{
			Checker.NotNullArgument(asm, "asm").Throw();

			var list = new Dictionary<string, T>();
			try {
				var types = asm.GatherByInterface<T>();
				if(types.Any()) {
					foreach(var type in types) {
						if(type.IsAbstract) { continue; }
						var one = Activator.CreateInstance(type);
						T t = one as T;
						if(null == t) { continue; }
						list.Add(type.FullName, t);
					}
				}
			} catch(Exception ex) {
				LogRecord
					.Create()
					//.Add("Assembly", asm.FullName)
					//.Add("ObjectType", typeof(T).Name)
					//.Add(ex)
					.Error();
			}
			return list;
		}
		#endregion
		#region type
		public static Type[] GetTypes(Assembly assembly)
		{
			try {
				var types = assembly.GetTypes();
				return types;
			} catch(ReflectionTypeLoadException lex) {
				LogRecord
					.Create()
					//.Add("Assembly", assembly.FullName)
					//.Add(lex)
					//.Add(lex.LoaderExceptions)
					.Error();
			} catch(Exception ex) {
				LogRecord
					.Create()
					//.Add("Assembly", assembly.FullName)
					//.Add(ex)
					//.Add(ex.InnerException)
					.Error();
			}
			return new Type[0];
		}
		#endregion
	}
}
