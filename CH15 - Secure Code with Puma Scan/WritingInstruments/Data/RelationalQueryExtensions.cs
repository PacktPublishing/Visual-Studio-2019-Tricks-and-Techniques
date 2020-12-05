using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace WritingExample.Data
{
    public static class RelationalQueryExtensions
    {
        public static IQueryable<TEntity> FromSqlWriting<TEntity>([NotNullAttribute] this DbSet<TEntity> source, [NotNullAttribute][NotParameterized] string sql, [NotNullAttribute] params object[] parameters) where TEntity : class
        {
            return source.FromSqlRaw<TEntity>(sql, parameters);
        }
    }
}