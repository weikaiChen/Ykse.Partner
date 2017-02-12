using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public interface IMainElapsedAction
	{
		void MainAccumulating(double executionTime);
		void SetError(Exception ex);
		void SetError(string message);
		void SetError(string message, Exception ex);
		void SetSuccess(string message);
	}
}
