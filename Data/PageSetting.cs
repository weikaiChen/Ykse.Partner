using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ykse.Partner
{
	public class PageSetting
	{
		/// <summary>
		/// 頁數
		/// </summary>
		public int PageIndex
		{
			get { return _PageIndex; }
			set { _PageIndex = value; }
		}
		/// <summary>
		/// 每筆筆數
		/// </summary>
		public int PageSize
		{
			get { return _PageSize; }
			set { _PageSize = value; }
		}
		/// <summary>
		/// 排序的主鍵
		/// </summary>
		public String Key { get; set; }

		public int PageCount { get; set; } = 0;
		public int DataCount { get; set; } = 0;

		public int PrePageCount { get; set; } = 2;
		public int PostPageCount { get; set; } = 2;

		public int LastPage { get; set; } = 1;
		public List<int> PageMenu { get; set; } = new List<int>();
		public Boolean TurnOnPage
		{
			get { return _TurnOnPage; }
			set { _TurnOnPage = value; }
		}

		private Boolean _TurnOnPage = true;
		private int _PageIndex = 1;
		private int _PageSize = 7;

		public PageSetting()
		{
			this.PageIndex = 1;
			this.PageSize = 7;
		}

		public PageSetting(int PageIndex, int pageSize)
		{
			SetParameter(PageIndex, pageSize, "");
		}

		public PageSetting(int PageIndex, int pageSize,int prePageCount,int postPageCount,string key)
		{
			SetParameter(PageIndex, pageSize, "");
			this.PrePageCount = PrePageCount;
			this.PostPageCount = postPageCount;
		}
		public void SetParameter(int PageIndex, int pageSize, String key)
		{
			this.PageIndex = PageIndex;
			this.PageSize = pageSize;
			this.Key = key;

		}


		public void SetParameter(PageSetting inputPageSeting)
		{
			this.PageIndex = inputPageSeting.PageIndex;
			this.PageSize = inputPageSeting.PageSize;
			this.Key = inputPageSeting.Key;
			TurnOnPage = true;
		}

		public void PageCaculate(int inputDataCount)
		{
			this.DataCount = inputDataCount;
			var size = this.PageSize != 0 ? this.PageSize : 1;
			var remainer = 0;
			var lvPageCount = 1;
			remainer = this.DataCount % size;

			lvPageCount = this.DataCount / size;
			if(remainer != 0) {
				lvPageCount = lvPageCount + 1;
			}
			this.PageCount = lvPageCount;
			this.LastPage = lvPageCount;

			var lvMenuList = new List<int>();
			var showFirstPage = this.PageIndex - this.PrePageCount;
			var showLastPage = this.PageIndex + this.PostPageCount;
			if(showFirstPage <= 0) {
				showFirstPage = 1;
			}
			if(showLastPage > lvPageCount) {
				showLastPage = lvPageCount;
			}
			for(int i = showFirstPage; i <=showLastPage; i++) {
				lvMenuList.Add(i);
			}

			this.PageMenu = lvMenuList;

		}
	}
}
