using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Base.Models
{
    public class BaseList<T> where T : class
    {

        public long TotalCount { get; private set; }
        public int PageIndex { get; set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Result { get; private set; }
        public bool HasNext => PageIndex * PageSize < TotalCount;

        public BaseList(long totalCount, int pageIndex, int pageSize, IEnumerable<T> result)
        {
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Result = result;
        }
    }
}
