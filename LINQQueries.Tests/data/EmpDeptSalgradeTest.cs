using System.Linq;
using Xunit;
using LINQQueries.Models;

namespace LINQQueries.Tests
{
    public class EmpDeptSalgradeTests
    {
        [Fact]
        public void Should_Return_Employees_From_Dept10()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var result = emps.Where(e => e.DeptNo == 10).ToList();

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Contains(result, e => e.EName == "Jan");
        }
        [Fact]
        public void Should_Join_Employees_With_Their_Departments()
        {
            // Arrange
            var emps = TestData.Emps;
            var depts = TestData.Depts;

            // Act
            var result = emps
                .Join(depts,
                    e => e.DeptNo,
                    d => d.DeptNo,
                    (e, d) => new { e.EName, d.DName })
                .ToList();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(result, x => x.EName == "Jan" && x.DName == "ACCOUNTING");
        }
        [Fact]
        public void Should_Calculate_Average_Salary_Per_Department()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var result = emps
                .GroupBy(e => e.DeptNo)
                .Select(g => new
                {
                    DeptNo = g.Key,
                    AvgSal = g.Average(e => e.Sal)
                })
                .ToList();

            // Assert
            Assert.Equal(3, result.Count); // mamy 3 działy
            var dept10 = result.FirstOrDefault(r => r.DeptNo == 10);
            Assert.NotNull(dept10);
            Assert.Equal(800, dept10.AvgSal);
        }
        [Fact]
        public void Should_Return_Top2_Highest_Paid_Employees()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var result = emps
                .OrderByDescending(e => e.Sal)
                .Take(2)
                .ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Ewa", result[0].EName); // Ewa ma 3000
            Assert.Equal("Piotr", result[1].EName); // Piotr ma 2900
        }
        [Fact]
        public void Should_Join_Employees_With_Their_Salary_Grade()
        {
            // Arrange
            var emps = TestData.Emps;
            var salgrades = TestData.Salgrades;

            // Act
            var result = emps
                .Select(e => new
                {
                    Employee = e,
                    Grade = salgrades.FirstOrDefault(s => e.Sal >= s.LoSal && e.Sal <= s.HiSal)?.Grade
                })
                .ToList();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(result, r => r.Employee.EName == "Jan" && r.Grade == 1);
            Assert.Contains(result, r => r.Employee.EName == "Ewa" && r.Grade == 4);
        }
        [Fact]
        public void Should_Count_Employees_Without_Commission()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var count = emps.Count(e => e.Comm == null);

            // Assert
            Assert.Equal(3, count);
        }
        [Fact]
        public void Should_Calculate_Average_Salary_Per_Job()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var result = emps
                .GroupBy(e => e.Job)
                .Select(g => new
                {
                    Job = g.Key,
                    AvgSalary = g.Average(e => e.Sal)
                })
                .ToList();

            // Assert
            Assert.Equal(4, result.Count); // mamy 4 różne stanowiska
            var analyst = result.FirstOrDefault(r => r.Job == "ANALYST");
            Assert.NotNull(analyst);
            Assert.Equal(3000, analyst.AvgSalary);
        }
        [Fact]
        public void Should_Check_If_All_Employees_Earn_More_Than_700()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var allAbove700 = emps.All(e => e.Sal > 700);

            // Assert
            Assert.True(allAbove700);
        }
        [Fact]
        public void Should_Check_If_Any_Employee_Earns_More_Than_2500()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var someoneAbove2500 = emps.Any(e => e.Sal > 2500);

            // Assert
            Assert.True(someoneAbove2500);
        }
        [Fact]
        public void Should_Return_Employee_With_Lowest_Salary()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var lowestPaid = emps
                .OrderBy(e => e.Sal)
                .First();

            // Assert
            Assert.Equal("Jan", lowestPaid.EName);
            Assert.Equal(800, lowestPaid.Sal);
        }
        [Fact]
        public void Should_Return_Employee_With_Highest_Salary()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var highestPaid = emps
                .OrderByDescending(e => e.Sal)
                .First();

            // Assert
            Assert.Equal("Ewa", highestPaid.EName);
            Assert.Equal(3000, highestPaid.Sal);
        }
        [Fact]
        public void Should_Return_Unique_Jobs()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var uniqueJobs = emps
                .Select(e => e.Job)
                .Distinct()
                .ToList();

            // Assert
            Assert.Equal(4, uniqueJobs.Count);
            Assert.Contains("MANAGER", uniqueJobs);
        }
        [Fact]
        public void Should_Flatten_All_Letters_From_Employee_Names()
        {
            // Arrange
            var emps = TestData.Emps;

            // Act
            var letters = emps
                .SelectMany(e => e.EName.ToCharArray())
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            // Assert
            Assert.Contains('J', letters);
            Assert.Contains('A', letters);
        }
    }
}