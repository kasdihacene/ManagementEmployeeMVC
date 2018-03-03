using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
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
            using (DBModelConnnection dataBase = new DBModelConnnection())
            {
                List<EmployeeTable> employeeList = dataBase.EmployeeTable.ToList<EmployeeTable>();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int? id)
        {
            if (id == null)
                return View(new EmployeeTable());
            else
            {
                using (DBModelConnnection dataBase = new DBModelConnnection())
                {
                    return View(dataBase.EmployeeTable.Where(elem => elem.EmployeeId == id).SingleOrDefault());
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(EmployeeTable emp)
        {

            using (DBModelConnnection dataBase = new DBModelConnnection())
            {
                if (emp.EmployeeId == 0)
                {
                    dataBase.EmployeeTable.Add(emp);
                    dataBase.SaveChanges();
                    return Json(new { success = true, message = "Saved successfuly" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dataBase.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    dataBase.SaveChanges();
                    return Json(new { success = true, message = "Updated successfuly" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (DBModelConnnection db = new DBModelConnnection())
            {
                EmployeeTable employee = db.EmployeeTable.Where(x => x.EmployeeId == id).SingleOrDefault<EmployeeTable>();
                db.EmployeeTable.Remove(employee);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted successfuly" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}