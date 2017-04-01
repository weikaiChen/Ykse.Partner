using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
namespace Ykse.Partner.Provider
{
	public interface ISimpleDataApi
	{
		 IQueryable<T> Query<T>(string sql,	params DbParameter[] parameters)where T:new();

	}
}
