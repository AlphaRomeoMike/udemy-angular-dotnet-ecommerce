using API.DTOs;

namespace API.Helpers
{
   public class Pagination<TEntity> where TEntity : class
   {
      public Pagination(int pageIndex, int pageSize, int totalItems, List<TEntity> data)
      {
         PageIndex = pageIndex;
         PageSize = pageSize;
         Count = totalItems;
         Data = data;
      }

      public int PageIndex { get; set; }
      public int PageSize { get; set; }
      public int Count { get; set; }
      public IEnumerable<TEntity> Data { get; set; }
   }
}