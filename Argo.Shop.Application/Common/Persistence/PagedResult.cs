//namespace Argo.Shop.Application.Common.Persistence
//{
//    // see https://www.codingame.com/playgrounds/5363/paging-with-entity-framework-core

//    public class PagedResult<T> : PagedResultBase where T : class
//    {
//        public IList<T> Items { get; set; }

//        public PagedResult()
//        {
//            Items = new List<T>();
//        }
//    }

//    public abstract class PagedResultBase
//    {
//        public int CurrentPage { get; set; }
//        public int PageCount { get; set; }
//        public int PageSize { get; set; }
//        public int TotalItemCount { get; set; }

//        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

//        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalItemCount);

//        public bool HasPreviousPage => CurrentPage > 1;

//        public bool HasNextPage => CurrentPage < PageCount;
//    }

//}
