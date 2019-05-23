using NoSQL.Entities;

namespace NoSQL.Specification.NoteSpecification
{
    public class NameContainsSpecification : ISpecification<Note>
    {
        private readonly string name;

        public NameContainsSpecification(string name)
        {
            this.name = name;
        }

        public bool IsSatisfiedBy(Note item)
        {
            return item.Name.Contains(name);
        }
    }
}
