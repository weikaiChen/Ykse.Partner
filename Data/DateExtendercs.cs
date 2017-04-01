using System.Data.Common;
using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace Ykse.Partner
{
	public static class DateExtendercs
	{
		public static IQueryable<T> MappingItem<T>(this IDataReader reader) where T : new()
		{
			var results = new List<T>();
			var properties = typeof(T).GetProperties();

			while(reader.Read()) {
				var item = Activator.CreateInstance<T>();
				foreach(var property in typeof(T).GetProperties()) {
					if(!reader.IsDBNull(reader.GetOrdinal(property.Name))) {
						Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
						property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
					}
				}
				results.Add(item);
			}
			reader.Close();
			return results.AsQueryable();
		}

		public static IQueryable<T> MappingItem<T>(this IDataReader reader,
			ref int dataCount ) where T : new()
		{
			var results = new List<T>();
			var properties = typeof(T).GetProperties();

			while(reader.Read()) {
				var item = Activator.CreateInstance<T>();
				foreach(var property in typeof(T).GetProperties()) {
					if(!reader.IsDBNull(reader.GetOrdinal(property.Name))) {
						Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
						property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
					}
				}
				dataCount = reader["DataCount"].To<int>();
				results.Add(item);
			}
			reader.Close();
			return results.AsQueryable();
		}
	}
}
