using System.Data;
using System.Data.Common;
using System.Configuration;
using System;

namespace Ykse.Partner.Provider
{
	public abstract class DataApi : IDataApi
	{
		private const string _Title = "DataApi";

		public ProviderSettings Settings { get; private set; }
		public DbProviderFactory Factory { get; private set; }
		public DbProviderFactory _Factory { get; private set; }
		public DbConnection _Connection { get; private set; }
		public DbCommandBuilder _CommandBuilder { get; private set; }
		public DbConnectionStringBuilder _ConnectionStringBuilder
		{ get; private set; }

		protected abstract string _connectionName { get; }
		public CommandBehavior _ExecuteCommandBehavior { get; set; }

		private DataApi()
		{
		}
		public DataApi(bool withTransaction = false)
		{
			if(this._connectionName.Trim().Length == 0) {
				throw new Exception("無連線名稱");
			}
			string connectionString = ConfigurationManager.ConnectionStrings[this._connectionName].ConnectionString;
			string providerName = ConfigurationManager.ConnectionStrings[this._connectionName].ProviderName;
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

			this._ExecuteCommandBehavior =
					DataRunTime.InTransaction
						? CommandBehavior.CloseConnection
						: CommandBehavior.Default;
		}

		#region Dispose
		public void Dispose()
		{
			if(null != _Connection) {
				CloseConnection();
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

		public DataResult ExecuteSql(string sql)
		{
			return ExecuteMain(
				CommandType.Text,
				sql
			);
		}
		public DataResult ExecuteSql(
			string sql,
			params DbParameter[] parameters)
		{
			return ExecuteMain(
				CommandType.Text,
				sql,
				parameters
			);
		}

		private DataResult ExecuteMain(
			CommandType type,
			string sqlOrSpName,
			params DbParameter[] parameters)
		{
			var result = new DataResult();
			using(var il = new ILogger("DataApi.ExecuteMain", result)) {
				try {

					using(var command = _Connection.CreateCommand()) {
						command.CommandType = type;
						command.CommandText = sqlOrSpName;
						AddParameters(command, parameters);
						OpenConnection();

						var count = command.ExecuteNonQuery();
						result.AffectedCount = count;

					}
				} catch(Exception ex) {
					result.Success = false;
					result.Message = ex.Message;
				} finally {
					CloseConnection();
				}
			}

			return result;
		}
		public DbDataReader ExecuteSqlByReader(string sql)
		{
			return ExecuteByReaderMain(
				CommandType.Text,
				sql
			);
		}
		public DbDataReader ExecuteSqlByReader(string sql, DbParameter[] parameters)
		{
			return ExecuteByReaderMain(
				CommandType.Text,
				sql,
				parameters
			);
		}
		#region "private"


		private DbDataReader ExecuteByReaderMain(
			CommandType type,
			string sqlOrSpName,
			params DbParameter[] parameters
			)
		{
			using(var il = new ILogger(_Title)) {
				il.Record.Message = string.Format("Execute {0} by reader"
					, CommandTypeToString(type));
				try {
					using(var command = _Connection.CreateCommand()) {
						command.CommandType = type;
						command.CommandText = sqlOrSpName;
						AddParameters(command, parameters);
						OpenConnection();
						var reader = command.ExecuteReader(
							_ExecuteCommandBehavior
						);

						return reader;
					}
				} catch(Exception ex) {
					//il.SetError(ex);
					throw;
				} finally {
				}
			}


		}
		private string CommandTypeToString(CommandType type)
		{
			return type == CommandType.Text ? "SQL" : "SP ";
		}

		private void AddParameters(
			DbCommand command,
			params DbParameter[] parameters)
		{
			if(null == command) {
				throw new ArgumentNullException("command");
			}
			if(null != parameters) {
				foreach(var parameter in parameters) {
					// skip null
					if(null == parameter) { continue; }
					if(parameter.ParameterName.NullOrEmpty()) {
						continue;
					}
					command.Parameters.Add(parameter);
				}
			}
		}
		#endregion
	}
}
