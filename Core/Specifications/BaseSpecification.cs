using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
#nullable disable
   public class BaseSpecification<TEntity> : ISpecification<TEntity>
   {
      public Expression<Func<TEntity, bool>> Criteria { get; }
      public BaseSpecification()
      {
      }

      public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
      {
         Criteria = criteria;
      }

      public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();

      protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
      {
         Includes.Add(includeExpression);
      }

      public Expression<Func<TEntity, object>> OrderBy { get; private set; }

      public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

      protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
      {
        OrderBy = orderByExpression;
      }
      protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
      {
        OrderByDescending = orderByDescExpression;
      }
   }
}