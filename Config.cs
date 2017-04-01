using System.Configuration;

namespace Ykse.Partner
{
	public static class Config
	{
		private static string _TmpBannerYkseUpLoad = @"D:\Ykse\ykseWeb\TempYkseUpLoad\Banner\";
		private static string _BannerYkseUpLoad = @"D:\Ykse\ykseWeb\YkseUpLoad\Banner\";

		public static string TmpBannerYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["TmpBannerYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _TmpBannerYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string BannerYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["BannerYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _BannerYkseUpLoad;
				}

				return tempPath;
			}
		}

		private static string _TmpHotNewListYkseUpLoad = @"D:\Ykse\ykseWeb\TempYkseUpLoad\HotNews\List\";
		private static string _HotNewListYkseUpLoad = @"D:\Ykse\ykseWeb\YkseUpLoad\HotNews\List\";
		private static string _TmpHotNewContentYkseUpLoad = @"D:\Ykse\ykseWeb\TempYkseUpLoad\HotNews\List\Content\";
		private static string _HotNewContentYkseUpLoad = @"D:\Ykse\ykseWeb\YkseUpLoad\HotNew\HotNews\Content\";

		public static string TmpHotNewListYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["TmpHotNewListYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _TmpHotNewListYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string HotNewListYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["HotNewListYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _HotNewListYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string TmpHotNewContentYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["TmpHotNewContentYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _TmpHotNewContentYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string HotNewContentYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["HotNewContentYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _HotNewContentYkseUpLoad;
				}

				return tempPath;
			}
		}
		private static string _TmpNewestYkseUpLoad = @"D:\Ykse\ykseWeb\TempYkseUpLoad\Newest\";
		private static string _NewestYkseUpLoad = @"D:\Ykse\ykseWeb\YkseUpLoad\Newest\";

		public static string TmpNewestYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["TmpNewestYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _TmpNewestYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string NewestYkseUpLoad
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["NewestYkseUpLoad"];
				if(tempPath.NullOrEmpty()) {
					return _NewestYkseUpLoad;
				}

				return tempPath;
			}
		}

		public static string GetUpLoadPath(ConfigEnum configEnum)
		{
			string path = "";

			switch(configEnum) {
				case ConfigEnum.Banner:
					path = BannerYkseUpLoad;
					break;
				case ConfigEnum.HotNewsList:
					path = HotNewListYkseUpLoad;
					break;
				case ConfigEnum.HotNewContent:
					path = HotNewContentYkseUpLoad;
					break;
				case ConfigEnum.Newest:
					path = NewestYkseUpLoad;
					break;
				default:
					break;
			}
			return path;
		}

		public static string GetTmpUpLoadPath(ConfigEnum configEnum)
		{
			string path = "";

			switch(configEnum) {
				case ConfigEnum.Banner:
					path = TmpBannerYkseUpLoad;
					break;
				case ConfigEnum.HotNewsList:
					path = TmpHotNewListYkseUpLoad;
					break;
				case ConfigEnum.HotNewContent:
					path = TmpHotNewContentYkseUpLoad;
					break;
				case ConfigEnum.Newest:
					path = TmpNewestYkseUpLoad;
					break;
				default:
					break;
			}
			return path;

		}

		public static string _BannerSrc = "YkseUpLoad/Banner/";

		public static string BannerSrc
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["BannerSrc"];
				if(tempPath.NullOrEmpty()) {
					return _BannerSrc;
				}

				return tempPath;
			}
		}

		public static string _NewestSrc = "YkseUpLoad/Newest/";

		public static string NewestSrc
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["NewestSrc"];
				if(tempPath.NullOrEmpty()) {
					return _NewestSrc;
				}

				return tempPath;
			}
		}

		public static string _HotNewsListSrc = "YkseUpLoad/HotNews/List/";

		public static string HotNewsListSrc
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["HotNewsListSrc"];
				if(tempPath.NullOrEmpty()) {
					return _HotNewsListSrc;
				}

				return tempPath;
			}
		}

		public static string _HotNewsContentSrc = "YkseUpLoad/HotNews/Content/";

		public static string HotNewsContentSrc
		{
			get
			{
				var tempPath = ConfigurationManager.AppSettings["HotNewsContentSrc"];
				if(tempPath.NullOrEmpty()) {
					return _HotNewsContentSrc;
				}

				return tempPath;
			}
		}
	}

}
