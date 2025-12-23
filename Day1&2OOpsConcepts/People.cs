using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsSessions
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
    }

    public class Man : Person
    {
        public string Playing { get; set; }
    }

    public class Woman : Person
    {
        public string PlayMange { get; set; }
    }


    public class Child : Person
    {
        public string WatchingCartoon;
    }
}