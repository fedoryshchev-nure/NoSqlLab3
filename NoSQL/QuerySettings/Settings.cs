using NoSQL.QuerySettings.FilteringParams;
using NoSQL.QuerySettings.Sort;
using NoSQL.Specification;

namespace NoSQL.QuerySettings
{
    public class Settings<T>
    {
        public NoteFilteringParams FilteringParams { get; set; }
        public ISpecification<T> Filtering { get; set; }
        public PaginationSettings Pagination { get; set; }
        public OrderSettings Sorting { get; set; }
    }
}
