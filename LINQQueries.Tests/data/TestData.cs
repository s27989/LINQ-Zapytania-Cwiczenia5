using System;
using System.Collections.Generic;
using LINQQueries.Models;

namespace LINQQueries.Tests
{
    public static class TestData
    {
        public static List<Emp> Emps => new()
        {
            new Emp { EmpNo = 1, EName = "Jan", Job = "CLERK", Mgr = null, HireDate = new DateTime(2020, 1, 15), Sal = 800, Comm = null, DeptNo = 10 },
            new Emp { EmpNo = 2, EName = "Anna", Job = "SALESMAN", Mgr = 1, HireDate = new DateTime(2019, 3, 5), Sal = 1600, Comm = 300, DeptNo = 30 },
            new Emp { EmpNo = 3, EName = "Piotr", Job = "MANAGER", Mgr = null, HireDate = new DateTime(2018, 6, 20), Sal = 2900, Comm = null, DeptNo = 20 },
            new Emp { EmpNo = 4, EName = "Ewa", Job = "ANALYST", Mgr = 3, HireDate = new DateTime(2021, 11, 1), Sal = 3000, Comm = null, DeptNo = 20 },
        };

        public static List<Dept> Depts => new()
        {
            new Dept { DeptNo = 10, DName = "ACCOUNTING", Loc = "NEW YORK" },
            new Dept { DeptNo = 20, DName = "RESEARCH", Loc = "DALLAS" },
            new Dept { DeptNo = 30, DName = "SALES", Loc = "CHICAGO" },
        };

        public static List<Salgrade> Salgrades => new()
        {
            new Salgrade { Grade = 1, LoSal = 700, HiSal = 1200 },
            new Salgrade { Grade = 2, LoSal = 1201, HiSal = 1400 },
            new Salgrade { Grade = 3, LoSal = 1401, HiSal = 2000 },
            new Salgrade { Grade = 4, LoSal = 2001, HiSal = 3000 },
            new Salgrade { Grade = 5, LoSal = 3001, HiSal = 9999 },
        };
    }
}