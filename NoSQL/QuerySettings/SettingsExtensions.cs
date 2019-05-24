using NoSQL.QuerySettings.Sort;
using NoSQL.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace NoSQL.QuerySettings
{
    public static class SettingsExtensions
    {
        public static IEnumerable<T> ApplySettinigs<T>(this IEnumerable<T> input, Settings<T> settings)
        {
            return input.ApplyFiltering<T>(settings.Filtering)
                .AsQueryable()
                .ApplySorting(settings.Sorting)
                .ApplyPagination(settings.Pagination);
        }

        private static IEnumerable<T> ApplyFiltering<T>(
            this IEnumerable<T> input, 
            ISpecification<T> filter) => 
            input.Where(x => filter.IsSatisfiedBy(x));

        private static IEnumerable<T> ApplyPagination<T>(
            this IEnumerable<T> input, 
            PaginationSettings pagination) => 
            input.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

        private static IEnumerable<T> ApplySorting<T>(this IQueryable<T> input, OrderSettings sort)
        {
            switch (sort.Order)
            {
                case Order.Ascending:
                    return input.OrderBy(sort.SortBy);
                case Order.Descending:
                    return input.OrderBy(sort.SortBy).Reverse();
                default:
                    throw new Exception();
            }
        }
    }
}
