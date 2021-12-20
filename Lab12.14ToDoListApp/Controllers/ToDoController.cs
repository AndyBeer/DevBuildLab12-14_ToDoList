using Lab12._14ToDoListApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Controllers
{
    public class ToDoController : Controller
    {
        ToDoDAL db = new ToDoDAL();
        EmployeeDAL empDB = new EmployeeDAL();
        public IActionResult Index()
        {
            EmployeeToDoViewModel eTDVM = new EmployeeToDoViewModel(); //passing in an instance of this viewModel to access in view
            return View(eTDVM);
        }
        public IActionResult Create()
        {
            EmployeeToDoViewModel eTDVM = new EmployeeToDoViewModel(); //passing in an instance of this viewModel to access in view
            return View(eTDVM);
        }
        [HttpPost]
        public IActionResult Create(string tDName, string tDDesc, int tDDuration, int employeeID)
        {
            ToDo td = new ToDo();
            td.Name = tDName;
            td.Description = tDDesc;
            td.AssignedTo = employeeID;
            td.Duration = tDDuration;
            td.Employee = empDB.GetEmployee(employeeID); //this line is looking up the empl object for our TD model
        
            if (ModelState.IsValid)
            {
                db.CreateToDo(td);
                return RedirectToAction("Index", "ToDo");
            }
            else
            {
                return View(td);
            }
        }
        public IActionResult Details(int ID)
        {
            ToDo tD = db.GetToDo(ID);
            tD.Employee = empDB.GetEmployee(tD.AssignedTo);
            return View(tD);
        }
        public IActionResult Update(int ID)
        {
            ViewData["Employees"] = empDB.GetEmployees();
            ToDo tD = db.GetToDo(ID);
            return View(tD);

        }
        [HttpPost]
        public IActionResult Update(ToDo tD, int emplID)
        {
            tD.AssignedTo = emplID;
            tD.Employee = empDB.GetEmployee(tD.AssignedTo); //this does the same thing as Create, but passes in an existing ToDo model.
                                                            //Still updates the associated Empl as it does above.
            if (ModelState.IsValid)
            {
                db.UpdateToDo(tD);
                return RedirectToAction("Index", "ToDo");
            }
            else
            {
                return View(tD);
            }
        }
        public IActionResult Delete(int ID)
        {
            ToDo tD = db.GetToDo(ID);
            return View(tD);
        }

        public IActionResult DeleteToDoFromDb(int ID)
        {
            db.DeleteToDo(ID);
            return RedirectToAction("Index", "ToDo");
        }
    }
}
