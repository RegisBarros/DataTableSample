using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataTablesSample.Models
{
    public class EmployeeModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }

        public static EmployeeModel Create(string name, string position, string office, int age, DateTime startDate, decimal salary)
        {
            return new EmployeeModel()
            {
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

            return result;
        }

        public static void Load()
        {
            Employees = new List<EmployeeModel>();
            Employees.Add(EmployeeModel.Create("Airi Satou", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Angelica Ramos", "Chief Executive Officer (CEO)", "London", 47, DateTime.Parse("2009/10/09"), 162.700M));
            Employees.Add(EmployeeModel.Create("Ashton Cox", "Junior Technical Author", "San Francisco", 66, DateTime.Parse("2012/10/13"), 162.700M));
            Employees.Add(EmployeeModel.Create("Bradley Greer", "Software Engineer", "London", 41, DateTime.Parse("2011/06/07"), 162.700M));
            Employees.Add(EmployeeModel.Create("Brenden Wagner", "Software Engineer", "San Francisco", 28, DateTime.Parse("2012/12/02"), 162.700M));
            Employees.Add(EmployeeModel.Create("Brielle Williamson", "Integration Specialist", "New York", 61, DateTime.Parse("2011/05/03"), 162.700M));
            Employees.Add(EmployeeModel.Create("Bruno Nash", "Software Engineer", "London", 38, DateTime.Parse("2011/12/12"), 162.700M));
            Employees.Add(EmployeeModel.Create("Caesar Vance", "Pre-Sales Support", "New York", 21, DateTime.Parse("2011/12/06"), 162.700M));
            Employees.Add(EmployeeModel.Create("Cara Stevens", "Sales Assistant", "New York", 46, DateTime.Parse("2012/03/29"), 162.700M));
            Employees.Add(EmployeeModel.Create("Cedric Kelly", "Senior Javascript Developer", "Edinburgh", 22, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Charde Marshall", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Colleen Hurst", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Dai Rios", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Donna Snider", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Doris Wilder", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Finn Camacho", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Fiona Green", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Garrett Winters", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Gavin Cortez", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Gavin Joyce", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Gloria Little", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Haley Kennedy", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Hermione Butler", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Herrod Chandler", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Hope Fuentes", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Howard Hatfield", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Jackson Bradshaw", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Jena Gaines", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Jenette Caldwell", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Jennifer Acosta", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
            Employees.Add(EmployeeModel.Create("Jennifer Chang", "Accountant", "Tokyo", 33, DateTime.Parse("2008/11/28"), 162.700M));
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