using System.Collections.Generic;
using ShopService.Common.Interfaces;

namespace ShopService.Common.Infrastructure
{
    public abstract class QueryResult : IQueryResult
    {
        protected QueryResult(long numberOfPages, long numberOfRecords, long pageIndex)
        {
            Paging = new Paging
            {
                NumberOfPages = numberOfPages,
                NumberOfRecords = numberOfRecords,
                PageIndex = pageIndex
            };
        }

        /// <summary>
        /// Constructor for full query with data on one page
        /// </summary>
        /// <param name="numberOfRecords">number of records</param>
        protected QueryResult(long numberOfRecords)
        {
            Paging = new Paging
            {
                NumberOfRecords = numberOfRecords,
                NumberOfPages = 1,
                PageIndex = 0
            };
        }

        protected QueryResult()
        {
           
        }

        public IPaging Paging { get; set; }

        public abstract object GetData();

    }

    public class QueryResult<T> : QueryResult
    {
        public QueryResult(ICollection<T> data, long numberOfPages, long numberOfRecords, long pageIndex)
            : base(numberOfPages, numberOfRecords, pageIndex)
        {
            Result = data;
        }

        /// <summary>
        /// Constructor for query with data on one page with count all records
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countOfAllRecords"></param>
        public QueryResult(ICollection<T> data) : base(0, data.Count, 0)
        {
            Result = data;
        }

        /// <summary>
        /// Constructor for query with empty result
        /// </summary>
        public QueryResult()
        {
            Result = new List<T>();
            Paging = new Paging();
        }

        /// <summary>
        /// Constructor for query with paging result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="paging"></param>
        public QueryResult(ICollection<T> data, Paging paging)
        {
            Result = data;
            Paging = paging;
        }

        public ICollection<T> Result { get; }

        public override object GetData() => Result ?? new List<T>();
    }
}