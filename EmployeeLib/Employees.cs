using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
namespace EmployeeLib
{
    public class Employees 
    {
        public string Id { get; set; }
        public string Manger_Id { get; set; }
        public int Salary { get; set; }
        public List<Employees> GetEmployees { get; set; }
        //Returns salary budget for a specified manager
        //Defined as sum of all employees reporting directly or indirectly
        public long SalaryBudgetOfManager(string manager_Id)
        {
            long salary = 0;
            var emp = GetEmployees.FirstOrDefault(i => i.Id.Trim() == manager_Id.Trim());
            if (emp == null)
            {
                return 0;
            }
            salary += emp.Salary;

            foreach (var item in GetEmployees.Where(i => i.Manger_Id == manager_Id))
            {
                salary += SalaryBudgetOfManager(item.Id);
            }
            return salary;

        }
        private readonly string csvInput;
        //Sets employees list
        private void SetEmployees()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
            CsvEmployeesMapping csvMapper = new CsvEmployeesMapping();
            CsvParser<Employees> csvParser = new CsvParser<Employees>(csvParserOptions, csvMapper);
            GetEmployees = csvParser
                .ReadFromString(csvReaderOptions, csvInput)
                .Select(x => x.Result).ToList();

        }
        //General Method for validation
        private void ValidateEmployee()
        {
            GetEmployees = GetEmployees.OrderBy(i => i.Id).Distinct(new EmployeeComparer()).ToList();
            ValidateCircularReferenceAndManager();
        }
        //Validates for circular reference and a manager who is not an employee
        //Removes circular reference
        private void ValidateCircularReferenceAndManager()
        {
            for (int i = GetEmployees.Count - 1; i >= 0; i--)
            {
                var item = GetEmployees[i];
                GetEmployees.RemoveAll(m => m.Manger_Id == item.Id && m.Id == item.Manger_Id);
                if (!string.IsNullOrEmpty(item.Manger_Id))
                {
                    ValidateManagerWithNoEmployee(item.Manger_Id);
                }
            }

        }
       //Validates Manager with No employee
        private void ValidateManagerWithNoEmployee(string manager_Id)
        {
            if (GetEmployees.FirstOrDefault(i => i.Id.Trim() == manager_Id.Trim()) == null)
            {
                GetEmployees.RemoveAll(i => i.Manger_Id.Trim() == manager_Id.Trim());
            }

        }
        //Constructor To pass Csv string
        public Employees(string csv)
        {

            csvInput = csv;
            SetEmployees();
            ValidateEmployee();
        }
        //Constructor to help in TinyCsvParser
        public Employees()
        {

        }
        private class CsvEmployeesMapping : CsvMapping<Employees>
        {
            public CsvEmployeesMapping()
                : base()
            {
                MapProperty(0, x => x.Id);
                MapProperty(1, x => x.Manger_Id);
                MapProperty(2, x => x.Salary);
            }

        }
    }
}
