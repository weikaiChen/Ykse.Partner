namespace Ykse.Partner
{
	public class IsToConstants
	{
		public class Pattern
		{
			public const string Numeric =
				@"^-?\d*.*\d+$";
			public const string HtmlColor =
				@"^#(?:[0-9a-fA-F]{3,4}){1,2}$";
			public const string RGBColor =
				@"^rgb\((?<r>\d+),\s*(?<g>\d+),\s*(?<b>\d+)\)$";
			public const string ARGBColor =
				@"^rgba\((?<r>\d+),\s*(?<g>\d+),\s*(?<b>\d+),\s*(?<a>\d*.*\d+%?)\)$";
		}
	}
}
