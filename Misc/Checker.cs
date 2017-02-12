using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public class Checker
	{
		public static Result NotNullArgument(
			object argument,
			string argumentName)
		{
			argumentName = argumentName.Airbag(Symbol.Question);
			if(null == argument) {
				return Result.BuildFailure(
					new ArgumentNullException(
						argumentName,
						"Argument(%s) is null reference"
						.Printf(argumentName)
					)
				);
			}

			return Result.BuildSuccess(
				"Argument(%s) doesn't null reference"
				.Printf(argumentName)
			);
		}

	}
}
