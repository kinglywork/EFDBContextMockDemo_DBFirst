using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDBContextMockTest.Utility.Async;
using Moq;
using Moq.Language;
using Moq.Language.Flow;

namespace EFDBContextMockTest.Utility
{
    public static class Extensions
    {
        public static IReturnsResult<TMock> ReturnDbSet<TMock, TResult, TEntity>(
            this IReturns<TMock, TResult> fluent,
            IList<TEntity> sourceList)
            where TMock : class
            where TResult: DbSet<TEntity>
            where TEntity : class
        {
            var data = sourceList.AsQueryable();

            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            return fluent.Returns((TResult) mockSet.Object);
        }

        public static IReturnsResult<TMock> ReturnDbSetAsync<TMock, TResult, TEntity>(
            this IReturns<TMock, TResult> fluent,
            IList<TEntity> sourceList)
            where TMock : class
            where TResult: DbSet<TEntity>
            where TEntity : class
        {
            var data = sourceList.AsQueryable();

            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));

            mockSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(data.Provider));

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return fluent.Returns((TResult) mockSet.Object);
        }
    }
}