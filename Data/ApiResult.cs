using System;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Ykse.Partner;
using System.Collections.Generic;
namespace Ykse.Partner
{
	public class ApiResult : ActionResult
	{
		[DataMember]
		public bool Success { get; set; }
		[DataMember]
		public string Message { get; set; }
		[DataMember]
		public LogLevel Level { get; set; }
		[DataMember]
		public double TotalExecutionTime { get; set; }
		[DataMember]
		public double DbExecutionTime { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{

			var response = context.HttpContext.Response;
			response.ContentType = Constants.ContentType.JSON;
			response.Write(this.ToJson());
		}


	}

	public class ApiResult<T>
	: ApiResult
	{
		public ApiResult()
			: base()
		{
			if(
				typeof(T).Is<IDynamicMetaObjectProvider>()
				&&
				!typeof(T).Is<IDomain>()) {
				dynamic d = new ExpandoObject();
				this.Data = d;
			} else {
				this.Data = new List<T>();
			}
		}

		[DataMember]
		public List<T> Data { get; set; }

		public PageSetting PageSet { get; set; } = new PageSetting();



	}
}
