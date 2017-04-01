
using System.Transactions;

namespace Ykse.Partner
{
	public class DataRunTime : RunTime
	{
		public static bool InTransaction
		{
			get
			{
				return null != Transaction.Current;
			}
		}
	}
}
