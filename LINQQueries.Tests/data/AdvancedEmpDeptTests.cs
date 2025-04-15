using System.Linq;
using Xunit;
using LINQQueries.Models;

namespace LINQQueries.Tests
{
    public class AdvancedEmpDeptTests
    {
        [Fact]
        public void Should_Group_Employees_By_Department()
        {
            var emps = TestData.Emps;
            var depts = TestData.Depts;

            var result = depts
                .GroupJoin(emps,
                    d => d.DeptNo,
                    e => e.DeptNo,
                    (d, es) => new
                    {
                        Department = d.DName,
                        Employees = es.Select(e => e.EName).ToList()
                    })
                .ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(result, r => r.Department == "ACCOUNTING" && r.Employees.Contains("Jan"));
        }
        [Fact]
        public void Should_Return_Employees_With_Embedded_Department_Object()
        {
            var emps = TestData.Emps;
            var depts = TestData.Depts;

            var result = emps
                .Select(e => new
                {
                    Employee = e.EName,
                    Salary = e.Sal,
                    Department = depts.FirstOrDefault(d => d.DeptNo == e.DeptNo)
                })
                .ToList();

            Assert.Contains(result, r => r.Employee == "Jan" && r.Department.DName == "ACCOUNTING");
        }
        [Fact]
        public void Should_Return_Employees_With_Total_Income()
        {
            var emps = TestData.Emps;

            var result = emps
                .Select(e => new
                {
                    e.EName,
                    TotalIncome = e.Sal + (e.Comm ?? 0)
                })
                .ToList();

            Assert.Contains(result, r => r.EName == "Anna" && r.TotalIncome == 1900);
        }
        [Fact]
        public void Should_Handle_Employees_With_No_Matching_Department()
        {
            var emps = TestData.Emps;
            var depts = TestData.Depts;

            var result = emps
                .GroupJoin(depts,
                    e => e.DeptNo,
                    d => d.DeptNo,
                    (e, dGroup) => new
                    {
                        e.EName,
                        DepartmentName = dGroup.Select(d => d.DName).FirstOrDefault() ?? "UNKNOWN"
                    })
                .ToList();

            Assert.Contains(result, r => r.EName == "Jan" && r.DepartmentName == "ACCOUNTING");
        }
        [Fact]
        public void Should_Count_Employees_Per_Job()
        {
            var emps = TestData.Emps;

            var result = emps
                .GroupBy(e => e.Job)
                .Select(g => new
                {
                    Job = g.Key,
                    Count = g.Count()
                })
                .ToList();

            Assert.Contains(result, r => r.Job == "CLERK" && r.Count == 1);
        }
        [Fact]
        public void Should_Find_Departments_With_High_Salaries()
        {
            var emps = TestData.Emps;

            var result = emps
                .Where(e => e.Sal > 2000)
                .Select(e => e.DeptNo)
                .Distinct()
                .ToList();

            Assert.Contains(20, result);
            Assert.DoesNotContain(10, result);
        }
        [Fact]
        public void Should_Find_Departments_Without_Employees()
        {
            var emps = TestData.Emps;
            var depts = TestData.Depts;

            var result = depts
                .Where(d => !emps.Any(e => e.DeptNo == d.DeptNo))
                .Select(d => d.DName)
                .ToList();

            Assert.DoesNotContain("ACCOUNTING", result);
            // Dodaj pusty dział do TestData, jeśli chcesz pełny test
        }

    }
}