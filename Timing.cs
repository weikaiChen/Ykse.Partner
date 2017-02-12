using System;
using System.Diagnostics;

namespace Ykse.Partner
{
	public class Timing : IDisposable
	{
		private Stopwatch _Stopwatch;

		public Timing()
		{
			this._Stopwatch = new Stopwatch();
			this._Stopwatch.Start();
		}

		public double Elapsed { get; private set; }

		public virtual void BeforeDispose() { }
		public virtual void AfterDispose() { }

		public Action<Timing> BeforeDisposeAction { get; set; }
		public Action<Timing> AfterDisposeAction { get; set; }

		#region IDisposable
		public void Dispose()
		{
			BeforeDispose();
			BeforeDisposeAction?.Invoke(this);

			_Stopwatch.Stop();
			Elapsed = _Stopwatch.Elapsed.TotalMilliseconds / 1000d;

			AfterDisposeAction?.Invoke(this);
			AfterDispose();
		}
		#endregion
	}
}
