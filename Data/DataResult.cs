using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ykse.Partner
{
	public class DataResult: Result<DataResult>
	{
		private object _DataLock = new object();
		private int _AffectedCount;

		//[DataMember]
		//public string TransactionID { get; set; }
		[DataMember]
		public string ExecuteMethod { get; set; }
		[DataMember]
		public CommandType CommandType { get; set; }
		[DataMember]
		public object ScalarValue { get; set; }
		[DataMember]
		public string Command { get; set; }
		[NonSerialized]
		private List<DbParameter> _Parameters;
		[XmlIgnore, IgnoreDataMember]
		public List<DbParameter> Parameters
		{
			get
			{
				return _Parameters;
			}
			set
			{
				_Parameters = value;
			}
		}

		public DataResult()
			: base()
		{
		}

		[DataMember]
		public int AffectedCount
		{
			get
			{
				lock(_DataLock) {
					int count = _AffectedCount;
					foreach(var result in InnerResults) {
						if(null == result) { continue; }
						count += result.AffectedCount;
					}
					return count;
				}
			}
			set
			{
				_AffectedCount = value;
			}
		}
	}
}
