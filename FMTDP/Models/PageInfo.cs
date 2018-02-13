using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models
{
    public class PageInfo
    {
        private int _Page = 1;
        public int Page
        {
            get
            {
                return _Page;
            }
            set
            {
                _Page = value;
            }
        }

        private int _PageSize = 20;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }

        public int TotalCount { get; set; }

        private string _SortValue = null;
        public string SortValue
        {
            get
            {
                return _SortValue;
            }
            set
            {
                _SortValue = value;
            }
        }
 
        private int _pageCount;
        public int PageCount
        {
            get
            {
                if (_pageCount == 0)
                {
                    _pageCount = TotalCount / PageSize;
                    if (TotalCount % PageSize > 0)
                    {
                        _pageCount++;
                    }
                }
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }

        private int _rowStart;
        public int RowStart
        {
            get
            {
                return _rowStart == 0 ? (Page - 1) * PageSize + 1 : _rowStart;
            }
            set
            {
                _rowStart = value;
            }
        }

        private int _rowEnd;
        public int RowEnd
        {
            get { return _rowEnd == 0 ? Page * PageSize : _rowEnd; }
            set { _rowEnd = value; }
        }
    }
}
