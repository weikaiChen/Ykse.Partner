using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ykse.Partner
{
	public class IntervalLogger : Timing
	{
		public LogRecord Record { get; private set; }
		public MethodBase Method { get; private set; }

		public IntervalLogger()
			: this(string.Empty)
		{
		}

		public IntervalLogger(object title)
			: this(title, null)
		{
		}

		public IntervalLogger(IMainElapsedAction action)
			: this(string.Empty, action)
		{
		}

		public IntervalLogger(object title, IMainElapsedAction action)
			: base()
		{
			this.Record = new LogRecord();
			//var t = title.ToStringX();
			//if(!t.NullOrEmpty()) { this.Record.Title = t; }
			//if(null != action) { this.Action = action; }

			// Tid
			//ScopeContext.Current.Scope.Tid = this.Record.Tid;

			// MethodBase
			var sf = RunTime.CalleeStackFrameBeyond("Log");
			if(null != sf) {
				this.Method = sf.GetMethod();
				this.Record.Path = this.Method.Path();
				this.Record.Title =
					this.Record.Title.NullOrEmpty()
					||
					this.Record.Title.Contains("IntervalLogger")
						? this.Method.Description()
						: this.Record.Title;
				this.Record.Method = this.Method;
			}
		}


		public override void AfterDispose()
		{
			//if(null != Action) { Action.MainAccumulating(Elapsed); }
			Record.ExecutionTime = Elapsed;
			Record.Write();
		
			GC.SuppressFinalize(this);
		}
	}


	public class ILogger : IntervalLogger
	{
		public ILogger()
			: this(string.Empty)
		{
		}

		public ILogger(object title)
			: this(title, null)
		{
		}

		public ILogger(IMainElapsedAction action)
			: this(null, action)
		{
		}

		public ILogger(object title, IMainElapsedAction action)
			: base(title, action)
		{
		}

		public static ILogger Create(params string[] titleParts)
		{
			return new ILogger(
				titleParts.Join(Symbol.Period)
			);
		}

		public static ILogger Create(
			IMainElapsedAction action,
			params string[] titleParts)
		{
			return new ILogger(
				titleParts.Join(Symbol.Period),
				action
			);
		}


	}

}
