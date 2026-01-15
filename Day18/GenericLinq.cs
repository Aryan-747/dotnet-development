using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class EmployeeRe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
    }
    public class GenericLinq
    {
        public static List<EmployeeRe> EmployeeReList = new List<EmployeeRe>();

        public GenericLinq()
        {
            
        }
        public static void Main(string[] args)
        {
            var localEmpList = GetData();
            var filterList = from emp in localEmpList
                             select emp;

            foreach (EmployeeRe emp in filterList)
            {
                 
            }
        }

        public static List<EmployeeRe> GetData()
        {
            EmployeeReList.Add(new EmployeeRe() { Id=1, Name ="A", Rank=1 });
            EmployeeReList.Add(new EmployeeRe() { Id=2, Name ="B", Rank=3 });
            EmployeeReList.Add(new EmployeeRe() { Id=3, Name ="C", Rank=2 });

            return EmployeeReList;
        }
    }
