using System;

namespace CountingApp.Models
{
    public class Person : IEquatable<Person>
    {
        public string Id { get; }

        public string DisplayName { get; set; }

        public Person(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public bool Equals(Person other)
        {
            return other != null && Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * 397;
            }
        }
    }
}
