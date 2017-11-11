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
            var result = EmployeeRepository.GetAll(model.length, model.start);

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