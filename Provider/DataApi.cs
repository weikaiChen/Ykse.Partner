using System.Data;
using System.Data.Common;
using System.Configuration;
using System;

namespace Ykse.Partner.Provider
{
	public abstract class DataApi : IDataApi
	{
		public DbProviderFactory _Factory { get; private set; }
		public DbConnection _Connection { get; private set; }
		public DbCommandBuilder _CommandBuilder { get; private set; }
		public DbConnectionStringBuilder _ConnectionStringBuilder
		{ get; private set; }

		protected abstract string _connectionName { get; }
		public CommandBehavior _ExecuteCommandBehavior { get; set; }

		public DataApi()
		{
			if(this._connectionName.Trim().Length == 0) {
				throw new Exception("無連線名稱");
			}
			string connectionString = ConfigurationManager.ConnectionStrings[this._connectionName].ConnectionString;
			string providerName= ConfigurationManager.ConnectionStrings[this._connectionName].ProviderName;
			//MySqlConnection conn = new MySqlConnection(connectionString);
			this._Factory = DbProviderFactories.GetFactory(
					providerName
				);
			this._CommandBuilder = this._Factory.CreateCommandBuilder();
			var connBuilder = this
				._Factory
				.CreateConnectionStringBuilder();
			connBuilder.ConnectionString = connectionString;
			this._ConnectionStringBuilder = connBuilder;
			var connection = this._Factory.CreateConnection();
			connection.ConnectionString = connectionString;
			this._Connection = connection;
		}

		#region Dispose
		public void Dispose()
		{
			if(null != _Connection) {
			}
		}
		#endregion

		private void CloseConnection()
		{
			if(null == _Connection) { return; }
		

			try {
				if(_Connection.State.In(
					ConnectionState.Broken,
					ConnectionState.Closed)) {
					return;
				}
				_Connection.Close();
			} catch(Exception ex) {
				
			}
		}


		private void OpenConnection()
		{
			if(null == this._Connection) {
					//TODO:log+錯誤訊息
			}

			if(ConnectionState.Closed != this._Connection.State) { return; }

			try {
				this._Connection.Open();
			} catch(Exception ex) {
				//TODO:log+錯誤訊息
				throw;
			}
		}

		private DataResult ExecuteMain()
		{
			var result = new DataResult();
			try {

			} catch(Exception) {
				//todo:log
				throw;
			}

			return result;
		}
	}
}
