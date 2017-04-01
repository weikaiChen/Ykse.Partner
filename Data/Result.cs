

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Ykse.Partner
{
	[Serializable, DataContract]
	public class Result
		:  EventArgs, IMainElapsedAction
	{
		protected object _Lock = new object();
		private Exception _Exception;

		public Result()
		{
			this.Success = true;
			this.Message = string.Empty;
			this.Level = LogLevel.Info;
			this.Datas = new SafeList<Any>();
			this.InnerResults = new ConcurrentQueue<Result>();
		}

		#region property
		[DataMember]
		public string _Name
		{
			get
			{
				return "TrueTypeIs" + this.GetType().Name;
			}
			set
			{
			}
		}

		[DataMember]
		public bool Success { get; set; }
		[DataMember]
		public string Message { get; set; }
		[DataMember]
		public LogLevel Level { get; set; }
		[DataMember]
		public double TotalExecutionTime { get; set; }
		[DataMember]
		public double DbExecutionTime { get; set; }
		[DataMember]
		public SafeList<Any> Datas { get; set; }
		public ConcurrentQueue<Result> InnerResults { get; private set; }
		public Exception Exception
		{
			get
			{
				if(null != _Exception) { return _Exception; }
				foreach(var result in InnerResults) {
					if(null != result.Exception) {
						return result.Exception;
					}
				}
				return null;
			}
			set
			{
				_Exception = value;
				if(null != _Exception) {
					Success = false;
				}
			}
		}

		public bool FullSuccess
		{
			get
			{
				lock(_Lock) {
					return FullSuccessMain(this);
				}
			}
		}

		private bool FullSuccessMain(Result result)
		{
			if(!result.Success) { return false; }
			if(null != result.Exception) { return false; }
			foreach(var innerResult in result.InnerResults) {
				if(!FullSuccessMain(innerResult)) { return false; }
			}
			return result.Success;
		}

		public List<string> FullMessage
		{
			get
			{
				lock(_Lock) {
					var list = new List<string>();
					FullMessageMain(this, list);
					return list;
				}
			}
		}

		private void FullMessageMain(Result result, List<string> list)
		{
			var msg = result.Datas.NullOrEmpty()
				? result.Message
				: string.Concat(
					result.Message,
					Environment.NewLine,
					result.Datas.ToStringX()
				);
			list.Add(msg);
			if(!msg.NullOrEmpty()) { list.Add(msg); }
			foreach(var innerResult in result.InnerResults) {
				FullMessageMain(innerResult, list);
			}
		}
		#endregion

		#region public
		//public Result AddResult(Result innerResult)
		//{
		//	InnerResults.Add(innerResult);
		//	return this;
		//}

		public void Throw()
		{
			var ex = Exception;
			if(null == ex) {
				if(this) {
					//
				} else {
					throw new Exception(Message);
				}
			} else {
				throw ex;
			}
		}

		//public bool Try(out string message)
		//{
		//	message = Message.Airbag(
		//		null == Exception ? string.Empty : Exception.Message
		//	);

		//	return Success;
		//}
		#endregion

		#region To
		public override string ToString()
		{
			var value = Success ? "Success" : "Failure";
			if(Message.NullOrEmpty()) { return value; }
			value = value + ": " + Message;
			return value;
		}

		//public Result ToConsole()
		//{
		//	return ToConsole(false);
		//}

		//public Result ToConsole(bool onlyFailure = false)
		//{
		//	if(onlyFailure && Success) { return this; }
		//	if(RunTime.IsWebApp) { return this; }

		//	lock(_Lock) {
		//		Console.ForegroundColor = Success
		//			? ConsoleColor.White
		//			: ConsoleColor.Red;
		//		Console.WriteLine(
		//			"%s -- %s ".Printf(
		//				DateTime.Now.HH_mm_ss_fff(),
		//				ToString()
		//			)
		//		);
		//		Console.ResetColor();

		//		// Beep
		//		if(!Success) {
		//			Console.Beep();
		//		}
		//	}

		//	return this;
		//}
		#endregion

		#region Delegate
		public void MainAccumulating(double executionTime)
		{
			TotalExecutionTime += executionTime;
		}

		public void SetError(Exception ex)
		{
			Success = false;
			Level = LogLevel.Error;
			if(null == ex) { return; }
			Exception = ex;
			if(!Message.NullOrEmpty()) {
				Message += Environment.NewLine;
			}
			Message += ex.Message;
		}

		public void SetError(string message = "")
		{
			Success = false;
			Level = LogLevel.Error;
			if(!Message.NullOrEmpty() && !message.NullOrEmpty()) {
				Message += Environment.NewLine;
			}
			Message += message;
		}

		public void SetError(string message, Exception ex)
		{
			Success = false;
			Level = LogLevel.Error;
			if(!Message.NullOrEmpty() && !message.NullOrEmpty()) {
				Message += Environment.NewLine;
			}
			Message += message;

			if(null == ex) { return; }
			Exception = ex;
			if(
				!Message.Trim().NullOrEmpty()
				&&
				ex.Message.NullOrEmpty()) {
				Message += Environment.NewLine;
			}
			Message += ex.Message;
		}

		public void SetSuccess(string message = "")
		{
			Success = true;
			Level = LogLevel.Info;
			if(!Message.NullOrEmpty() && !message.NullOrEmpty()) {
				Message += Environment.NewLine;
			}
			Message += message;
		}

		public void SubAccumulating(double executionTime)
		{
			DbExecutionTime += executionTime;
		}
		#endregion

		#region static
		public static Result BuildSuccess(string message = "")
		{
			return new Result() {
				Success = true,
				Message = message,
			};
		}

		//public static Result BuildFailure(
		//	params string[] messages)
		//{
		//	if(messages.NullOrEmpty()) {
		//		return new Result() {
		//			Success = false,
		//			Message = string.Empty,
		//		};
		//	} else {
		//		return new Result() {
		//			Success = false,
		//			Message = messages.Join(),
		//		};
		//	}
		//}

		public static Result BuildFailure(Exception ex)
		{
			return new Result() {
				Success = false,
				Message = ex.Message,
				Exception = ex,
			};
		}

		public static Result BuildFailure(string message, Exception ex)
		{
			return new Result() {
				Success = false,
				Message = message,
				Exception = ex,
			};
		}

		// operator
		public static implicit operator bool(Result result)
		{
			return result.Success;
		}

		public static bool operator !(Result result)
		{
			return !result.Success;
		}
		#endregion
	}

	[Serializable, DataContract]
	public class Result<T>
		: Result
		where T : Result<T>, new()
	{
		public Result()
			: base()
		{
			this.InnerResults = new ConcurrentQueue<T>();
		}

		public new ConcurrentQueue<T> InnerResults { get; private set; }

		public new bool FullSuccess
		{
			get
			{
				lock(_Lock) {
					if(!Success) { return false; }
					foreach(var result in InnerResults) {
						if(null == result) { continue; }
						if(!result.Success) { return false; }
					}
					return Success;
				}
			}
		}

		public new List<string> FullMessage
		{
			get
			{
				lock(_Lock) {
					var list = new List<string>();
					if(!Message.NullOrEmpty()) {
						list.Add(Message);
					}
					foreach(var result in InnerResults) {
						if(!result.Message.NullOrEmpty()) {
							list.Add(result.Message);
						}
					}
					return list;
				}
			}
		}

		//public new T ToConsole()
		//{
		//	base.ToConsole();
		//	return this as T;
		//}

		public new T MainAccumulating(double executionTime)
		{
			base.MainAccumulating(executionTime);
			return this as T;
		}

		public new T SetError(Exception ex)
		{
			base.SetError(ex);
			return this as T;
		}

		public new T SetError(string message = "")
		{
			base.SetError(message);
			return this as T;
		}

		public new T SetError(string message, Exception ex)
		{
			base.SetError(message, ex);
			return this as T;
		}

		public new T SetSuccess(string message)
		{
			base.SetSuccess(message);
			return this as T;
		}

		public new T SubAccumulating(double executionTime)
		{
			base.SubAccumulating(executionTime);
			return this as T;
		}

		public T AddResult(T innerResult)
		{
			InnerResults.Enqueue(innerResult);
			return (T)this;
		}

		#region static
		public new static T BuildSuccess(string message = "")
		{
			return new T() {
				Success = true,
				Message = message,
			};
		}

		public static T BuildFailure(string message = "")
		{
			return new T() {
				Success = false,
				Message = message,
			};
		}

		public new static T BuildFailure(Exception ex)
		{
			return new T() {
				Success = false,
				Message = ex.Message,
				Exception = ex,
			};
		}
		#endregion
	}
}
