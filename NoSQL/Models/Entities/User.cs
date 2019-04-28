using AspNetCore.Identity.Mongo.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NoSQL.Entities
{
    public class User : MongoUser
    {
        public override string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; } = new Collection<Note>();
    }
}
