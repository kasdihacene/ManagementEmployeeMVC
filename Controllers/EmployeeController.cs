using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementEmployeeMVC.Models;

namespace ManagementEmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel dataBase = new DBModel())
            {
                List<Employee> employeeList = dataBase.Employee.ToList<Employee>();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);    
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using (DBModel dataBase = new DBModel())
            {
                dataBase.Employee.Add(emp);
                dataBase.SaveChanges();
                return Json(new { success=true,message="Saved successfuly"},JsonRequestBehavior.AllowGet);
            }
        }
    }
}