using System.Linq;
using System.Data.Common;
using System.Collections.Generic;

namespace Ykse.Partner.Provider
{
	/// <summary>
	/// 簡單的新刪查修功能
	/// </summary>
	public abstract class SimpleData: DataApi,ISimpleDataApi
	{
		public IQueryable<T> Query<T>(string sql, params DbParameter[] parameters) where T : new()
		{		
			
			var dbReader = this.ExecuteSqlByReader(sql, parameters);
			var list=dbReader.MappingItem<T>();
			return list;
		}

	}
}
