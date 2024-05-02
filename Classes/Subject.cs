using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Classes
{
    internal class Subject
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
