using EmployeeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.UnitTests
{
    [TestClass]
    public class EmployeesTest
    {

        [TestMethod]
        //Tests if there are employees that are duplicate
        public void TestForDuplicatesAndInEmployee()
        {
            var stringBuilder = new StringBuilder()
               .AppendLine("Employee4,Employee2,500")
               .AppendLine("Employee4,Employee2,1200")
               .AppendLine("Employee2,Employee3,1200")
               .AppendLine("Employee3,Employee4,1200");
           Employees employee = new Employees(stringBuilder.ToString());
           Assert.AreEqual(3, employee.GetEmployees.Count);
        }
        [TestMethod]
        //Tests to ascertain that an employee is not reporting to more than one manager
        public void TestForAnEmployeeReportingNotReportingToMoreThanOneManager()
        {
            var stringBuilder = new StringBuilder()
                          .AppendLine("Employee4,Employee2,500")
                          .AppendLine("Employee4,Employee1,1200")
                          .AppendLine("Employee2,Employee1,1200")
                          .AppendLine("Employee3,Employee1,1200")
                          .AppendLine("Employee1,Employee4,1200");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(4, employee.GetEmployees.Count);
        }
        [TestMethod]
        public void TestForOnlyOneCeoPresent()
        {
            var stringBuilder = new StringBuilder()
                         .AppendLine("Employee4,,500")
                         .AppendLine("Employee2,,1200")
                         .AppendLine("Employee1,Employee3,1200");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(2, employee.GetEmployees.Count());

        }
        [TestMethod]
        //Tests to check if there is no circular reference
        public void TestForNoCircularReference()
        {
            var stringBuilder = new StringBuilder()
                          .AppendLine("Employee1,,1200")
                          .AppendLine("Employee5,Employee1,1200")
                          .AppendLine("Employee2,Employee4,1200")
                          .AppendLine("Employee3,Employee5,1200")
                          .AppendLine("Employee4,Employee2,500");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(3, employee.GetEmployees.Count());
        }
        [TestMethod]
        //Tests for a manager who is not an employee
        public void TestForManagerNotEmployee()
        {
            var stringBuilder = new StringBuilder()
                          .AppendLine("Employee1,,1200")
                          .AppendLine("Employee5,Employee6,1200");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(1, employee.GetEmployees.Count());
        }
        [TestMethod]
        //Test for an employee manager with all the child salaries
        public void TestForSalary ()
        {
            var stringBuilder = new StringBuilder()
                       .AppendLine("Employee1,,1000")
                       .AppendLine("Employee2,Employee1,800")
                       .AppendLine("Employee3,Employee1,500")
                       .AppendLine("Employee5,Employee1,500")
                       .AppendLine("Employee6,Employee2,500")
                       .AppendLine("Employee4,Employee2,500");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(3800, employee.SalaryBudgetOfManager("Employee1"));
        }
       

    }
}
