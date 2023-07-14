using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Py
{
    public class Variable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Variable(string name=null, string desc=null)
        {
            Name = name;
            Description = desc;
        }
    }
}
