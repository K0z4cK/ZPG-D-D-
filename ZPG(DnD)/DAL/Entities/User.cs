using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public virtual ICollection<Character> Characters {get;set;}
    }
}