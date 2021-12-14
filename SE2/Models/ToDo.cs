using System;
using System.Collections.Generic;
using System.Text;

namespace SE2
{
    [Serializable]
    class ToDo
    {
        private string name;
        public ToDo(string n)
        {
            name = n;
        }

        public string getName()
        {
            return name;
        }
    }
}
