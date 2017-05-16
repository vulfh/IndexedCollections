using IndexedCollections.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    internal class Person
    {
        [Key(Immutable =true)]
        public int Id { get; private set; }

        [Index]
        public string LastName { get; set; }

        [Index]
        public string FirstName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1},{2}", Id, LastName, FirstName);
        }

        public Person(int id,string firstName,string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Person(int id)
        {
            Id = id;
        }

    }
}
