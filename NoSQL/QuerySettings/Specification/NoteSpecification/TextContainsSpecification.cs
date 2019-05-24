using NoSQL.Entities;

namespace NoSQL.Specification.NoteSpecification
{
    public class TextContainsSpecification : ISpecification<Note>
    {
        private readonly string text;

        public TextContainsSpecification(string text)
        {
            this.text = text;
        }

        public bool IsSatisfiedBy(Note item)
        {
            return item.Text.Contains(text);
        }

    }
}
