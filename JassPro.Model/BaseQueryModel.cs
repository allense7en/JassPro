using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.Model
{
    public class BaseQueryModel
    {
        private int _page = 1;
        private string _defaultSortField = "Id";
        private string _defaultSort = "asc";
        private int _pageSize = 0;//30;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value > 0)
                    _page = value;
            }
        }

        public int StartRecord
        {
            get
            {
                return (Page - 1) * PageSize;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        public long TotalRecord { get; set; }
        public string Sidx
        {
            get
            {
                return _defaultSortField;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _defaultSortField = value;
            }
        }

        public string Sord
        {
            get
            {
                return _defaultSort;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _defaultSort = value;
            }
        }
    }
}
