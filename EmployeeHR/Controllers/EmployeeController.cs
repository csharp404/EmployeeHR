using EmployeeHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
namespace EmployeeHR.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<EmployeeModel> Employees = new List<EmployeeModel>
        {
         new EmployeeModel{
         Id =  1,
         FirstName = "Diya",
         LastName = "Ashqar",
         HiringDate =  Convert.ToDateTime("01/25/2024"),//new DateTime(2024,01,25),
         DOB =  new DateTime(1989,01,25),
         Salary = 500,
         IsActive = true,
         DepartmentId = 1,
         Email = "diya@gmail.com"},

         new EmployeeModel{
         Id = 2,
         FirstName = "Ahmad",
         LastName = "Ali",
         HiringDate = new DateTime(2023,05,30),
         DOB = new DateTime(1990,10,07),
         Salary = 400,
         IsActive = false,
         DepartmentId = 2,
         Email = "ahmad@gmail.com"}
        };

        private static List<DepartmentModel> Departments = new List<DepartmentModel>
        {
            new DepartmentModel
            {
                Id = 1,
                Name = "Development",
                Abbreviation = "DEV"
            },
            new DepartmentModel
            {
                Id = 2,
                Name = "Finance",
                Abbreviation = "FIN"
            }
        };

        public IActionResult Index()
        {
            List<EmployeeModel> employees = (from emp in Employees
                                             join dep in Departments
                                             on emp.DepartmentId equals dep.Id
                                             select new EmployeeModel
                                             {
                                                 Id = emp.Id,
                                                 FirstName = emp.FirstName,
                                                 LastName = emp.LastName,
                                                 HiringDate = emp.HiringDate,
                                                 IsActive = emp.IsActive,
                                                 Department = dep
                                             }
                                             ).ToList();
            //var list = employees.Include(x => x.Department);
            return View(employees);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentList = Departments;
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            if (employee != null)
            {
                Employees.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.DepartmentList = Departments;
            var employeeModel = Employees.FirstOrDefault(x => x.Id == id);
            return View("Create", employeeModel);
        }
        [HttpPost]
        public ActionResult Edit(int id, EmployeeModel employee)
        {
            var employeeModel = Employees.FirstOrDefault(x => x.Id == id);
            if (employeeModel != null)
            {
                employeeModel.FirstName = employee.FirstName;
                employeeModel.LastName = employee.LastName;
                employeeModel.HiringDate = employee.HiringDate;
                employeeModel.IsActive = employee.IsActive;
                employeeModel.DOB = employee.DOB;
                employeeModel.Salary = employee.Salary;
                employeeModel.Email = employee.Email;
                employeeModel.DepartmentId = employee.DepartmentId;
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var employeeModel = Employees.FirstOrDefault(x => x.Id == id);
            if (employeeModel != null)
            {
                Employees.Remove(employeeModel);
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
