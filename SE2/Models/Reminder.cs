using System;
using System.Collections.Generic;
using System.Text;

namespace SE2
{
    public class Reminder
    {
        private string name;
        private DateTime dateTime;

        public Reminder(string n, DateTime d)
        {
            name = n;
            dateTime = d;
        }

        public string getName()
        {
            return name;
        }

        public DateTime getTime()
        {
            return dateTime;
        }
    }
}
