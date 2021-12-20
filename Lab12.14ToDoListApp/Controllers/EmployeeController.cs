using Lab12._14ToDoListApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL db = new EmployeeDAL();
        public IActionResult Index()
        {
            List<Employee> eList = db.GetEmployees();
            return View(eList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.CreateEmployee(emp);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View(emp);
            }
        }
        public IActionResult Details(int ID)
        {
            Employee emp = db.GetEmployee(ID);
            return View(emp);
        }
        public IActionResult Update(int ID)
        {
            Employee e = db.GetEmployee(ID);
            return View(e);

        }
        [HttpPost]
        public IActionResult Update(Employee e)
        {
            if (ModelState.IsValid)
            {
                db.UpdateEmployee(e);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View(e);
            }
        }
        public IActionResult Delete(int ID)
        {
            //This will show full details of what we intend to delete
            //We can ask the user if they are sure do remove before we do that
            Employee e = db.GetEmployee(ID);
            return View(e);
        }

        public IActionResult DeleteEmpFromDb(int ID)
        {
            db.DeleteEmployee(ID);
            return RedirectToAction("Index", "Employee");
        }
    }
}
