using NoSQL.Specification;

namespace NoSQL.QuerySettings.Specification
{
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> left, right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public bool IsSatisfiedBy(T item)
        {
            return left.IsSatisfiedBy(item) && right.IsSatisfiedBy(item);
        }
    }
}
