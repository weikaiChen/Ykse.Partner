using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public class FinalValueAttribute : Attribute
	{
		public FinalValueAttribute(object value)
		{
			this.Value = value.ToString();
		}

		public string Value { get; set; }
	}
}
