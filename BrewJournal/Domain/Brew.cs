using System;

namespace BrewJournal.Domain
{
    public class Brew : Entity<Guid>
    {
        public string Name { get; protected set; }

        protected Brew() { }

        public Brew(string name)
        {
            Name = name;
        }
    }
}