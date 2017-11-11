using System.Linq;
using DataTablesSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataTablesSample.Controllers
{
    public class EmployeesController : Controller
    {
        public EmployeesController()
        {
            EmployeeRepository.Load();
        }

        [HttpPost]
        public IActionResult Index(DataTableModel model)
        {

            var orderBy = model.Columns[model.Order[0].Column].Data;
            var orderDirection = model.Order[0].Dir;

            var result = EmployeeRepository.GetAll(model.Start, model.Length, orderBy, orderDirection);

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = result.Total,
                recordsFiltered = result.Total,
                data = result.Items
            });
        }
    }
}