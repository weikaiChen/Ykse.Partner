using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Threading;
using System.ComponentModel;
using log4net.Config;
using log4net;
using System.Reflection;

namespace Ykse.Partner
{
	[DataContract]
	public class LogRecord
	{

		#region const
		public const string ID = "ID";
		public const string TIME = "TIME";
		public const string AREA = "AREA";
		public const string LOG = "LOG";
		public const string EQUITY = "EQUITY";
		public const string DATAS = "DATAS";

		private static readonly ILog log = LogManager.GetLogger(typeof(LogRecord));
		#endregion


		#region instance count
		private static int _InstanceCount = 0;
		

		public static int InstanceCount
		{
			get
			{
				return _InstanceCount;
			}
		}

		~LogRecord()
		{
			Interlocked.Decrement(ref _InstanceCount);
		}
		#endregion

		#region property

		[Category(LOG)]
		[DataMember]
		[Description("標題")]
		public string Title { get; set; }

		[Category(AREA)]
		[DataMember]
		[Description("程式碼路徑")]
		public string Path { get; set; }

		[Category(LOG)]
		[DataMember]
		[Description("訊息")]
		public string Message { get; set; }

		[Category(LOG)]
		[DataMember]
		[Description("等級")]
		public LogLevel Level { get; set; }


		[Category(TIME)]
		[DataMember]
		[Description("執行時間長度")]
		public double ExecutionTime { get; set; }


		[Category(DATAS)]
		[DataMember]
		[Description("額外資訊")]
		public SafeList<Any> Datas { get; set; }

		public MethodBase Method { get; internal set; }

		#endregion

		#region constructor
		public LogRecord()
			: this(true)
		{
		}

		public LogRecord(bool init)
		{
			Interlocked.Increment(ref _InstanceCount);
			if(init) {
				Init();
			} 
		}
		#endregion

		#region static
		public static LogRecord Create()
		{
			return new LogRecord();
		}

		public static LogRecord Create(string title)
		{
			var record = new LogRecord();
			if(!title.NullOrEmpty()) {
				record.Title = title;
			}
			return record;
		}
		#endregion


		#region private
		private void Init()
		{

			Title = string.Empty;

			Level = LogLevel.Info;

			ExecutionTime = 0L;

			Message = string.Empty;
			Datas = new SafeList<Any>();


			// MethodBase
			var sf = RunTime.CalleeStackFrameBeyond("LogRecord");
			if(null != sf) {
				this.Method = sf.GetMethod();
				Path = this.Method.Path();
				Title = Title.NullOrEmpty()
					? this.Method.Description()
					: Title;
			}

		}
		#endregion

		public LogRecord SetMessage(string message)
		{
			if(message.NullOrEmpty()) { return this; }
			Message = message;
			return this;

		}

		public LogRecord Write()
		{
			return Write(Level);
		}


		public LogRecord Error()
		{
			Level = LogLevel.Error;
			return Write();
		}

		public LogRecord Write(LogLevel level)
		{
			try {
				var json = this.ToJson(Formatting.Indented);
				var path = LogSettings.GetPath();
				XmlConfigurator.Configure(new System.IO.FileInfo(path));
				//XmlConfigurator.Configure(file);
				Level = level;
				switch(Level) {
					case LogLevel.Fatal:
						log.Fatal(json);
						break;
					case LogLevel.Error:
						log.Error(json);
						break;
					case LogLevel.Warn:
						log.Warn(json);
						break;
					case LogLevel.Info:
						log.Info(json);
						break;
					case LogLevel.Debug:
						log.Debug(json);
						break;
					default:
						throw new NotImplementedException(
							"LogLevel = " + Level
						);
				}
			} catch(Exception) {

			}
			return this;
		}

	}
}
