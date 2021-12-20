using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Models
{
    public class EmployeeToDoViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<ToDo> ToDos { get; set; }
        public EmployeeToDoViewModel()
        {
            ToDoDAL tdDAL = new ToDoDAL();
            EmployeeDAL eDAL = new EmployeeDAL();
            Employees = eDAL.GetEmployees();
            ToDos = tdDAL.GetToDos();

            foreach (ToDo td in ToDos)
            {
                if(td.AssignedTo != 0)
                {
                    td.Employee = eDAL.GetEmployee(td.AssignedTo);
                }
                else
                {
                    td.Employee = null;
                }
                
            }
        }
    }
}
