using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class Word
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Word(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Word;
            if(other == null)
            {
                return false;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return (Name).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} {Type}";
        }
    }
}
