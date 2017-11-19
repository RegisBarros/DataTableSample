using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataTablesSample.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }

        public void increasySalary()
        {
            Salary += Math.Round(Salary * 0.05M, 2);
        }

        public void decreaseSalary()
        {
            Salary -= Math.Round(Salary * 0.05M, 2);
        }

        public static EmployeeModel Create(int id, string name, string position, string office, int age, DateTime startDate, decimal salary)
        {
            return new EmployeeModel()
            {
                Id = id,
                Name = name,
                Position = position,
                Office = office,
                Age = age,
                StartDate = startDate,
                Salary = salary
            };
        }
    }

    public static class EmployeeRepository
    {
        private static List<EmployeeModel> Employees { get; set; }

        public static ResultList<EmployeeModel> GetAll(int page, int pageSize, string orderBy, string orderDirection)
        {
            var result = new ResultList<EmployeeModel>();


            if (orderDirection == "asc")
            {
                result.Items = Employees
                    .AsQueryable()
                    .OrderBy(orderBy)
                    .Skip(page)
                    .Take(pageSize);
            }
            else
            {
                result.Items = Employees
                    .AsQueryable()
                    .OrderByDescending(orderBy)
                    .Skip(page)
                    .Take(pageSize);
            }

            result.Total = Employees.Count;

            // Get random employee
            Random rnd = new Random();
            int r = rnd.Next(result.Items.Count());
            var employee = result.Items.ElementAt(r);
            employee.increasySalary();

            // Get an new one for decrease salary
            r = rnd.Next(result.Items.Count());
            employee = result.Items.ElementAt(r);
            employee.decreaseSalary();

            return result;
        }

        public static void Load()
        {
            Employees = new List<EmployeeModel>();
            Employees.Add(EmployeeModel.Create(1, "Airi Satou", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(2, "Angelica Ramos", "Chief Executive Officer (CEO)", "London", 47, DateTime.Parse("2009/10/09"), 162.700M));
            Employees.Add(EmployeeModel.Create(3, "Ashton Cox", "Junior Technical Author", "San Francisco", 66, DateTime.Parse("2012/10/13"), 162.700M));
            Employees.Add(EmployeeModel.Create(4, "Bradley Greer", "Software Engineer", "London", 41, DateTime.Parse("2011/06/07"), 162.700M));
            Employees.Add(EmployeeModel.Create(5, "Brenden Wagner", "Software Engineer", "San Francisco", 28, DateTime.Parse("2012/12/02"), 162.700M));
            Employees.Add(EmployeeModel.Create(6, "Brielle Williamson", "Integration Specialist", "New York", 61, DateTime.Parse("2011/05/03"), 162.700M));
            Employees.Add(EmployeeModel.Create(7, "Bruno Nash", "Software Engineer", "London", 38, DateTime.Parse("2011/12/12"), 162.700M));
            Employees.Add(EmployeeModel.Create(8, "Caesar Vance", "Pre-Sales Support", "New York", 21, DateTime.Parse("2011/12/06"), 162.700M));
            Employees.Add(EmployeeModel.Create(9, "Cara Stevens", "Sales Assistant", "New York", 46, DateTime.Parse("2012/03/29"), 162.700M));
            Employees.Add(EmployeeModel.Create(10, "Cedric Kelly", "Senior Javascript Developer", "Edinburgh", 22, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(11, "Charde Marshall", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(12, "Colleen Hurst", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(13, "Dai Rios", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(14, "Donna Snider", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(15, "Doris Wilder", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(16, "Finn Camacho", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(17, "Fiona Green", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(18, "Garrett Winters", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(19, "Gavin Cortez", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(20, "Gavin Joyce", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(21, "Gloria Little", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(22, "Haley Kennedy", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(23, "Hermione Butler", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(24, "Herrod Chandler", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(25, "Hope Fuentes", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(26, "Howard Hatfield", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(27, "Jackson Bradshaw", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(28, "Jena Gaines", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(29, "Jenette Caldwell", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(30, "Jennifer Acosta", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create(31, "Jennifer Chang", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
        }
    }

    public class ResultList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Total { get; set; }
    }

    public static class ListHelper
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> entities, string propertyName)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            return entities.OrderBy(e => propertyInfo.GetValue(e, null));
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> entities, string propertyName)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));
        }
    }
}