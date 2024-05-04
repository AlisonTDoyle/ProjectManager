using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Classes
{
    internal class Subject : IComparable<Subject>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Color { get; set; }

        public int CompareTo(Subject other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
