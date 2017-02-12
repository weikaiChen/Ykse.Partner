using System.Data;
using System.Data.Common;
using System;

namespace Ykse.Partner.Provider
{
	/// <summary>
	/// 提供資料庫連線
	/// </summary>
	public interface IDataApi: IDisposable
	{
		DbProviderFactory _Factory { get; }
		DbConnection _Connection { get; }
		DbCommandBuilder _CommandBuilder { get; }
		DbConnectionStringBuilder _ConnectionStringBuilder { get; }
		CommandBehavior _ExecuteCommandBehavior { get; set; }


	}
}
