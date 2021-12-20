using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Lab12._14ToDoListApp.Models
{
    public class EmployeeDAL
    {
        public List<Employee> GetEmployees() //for index to pass in list of all empls and fields
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string emplSql = $"select * from employees";

                connect.Open();
                List<Employee> empls = connect.Query<Employee>(emplSql).ToList();
                connect.Close();

                return empls;
            }
        }
        public Employee GetEmployee(int ID)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string singleEmpSql = $"select * from employees where id={ID}";

                connect.Open();
                Employee emp = connect.Query<Employee>(singleEmpSql).First();
                connect.Close();
                return emp;
            }
        }
        public void CreateEmployee(Employee emp) //void, since you already have an empl object at this point
                                                 //and are just adding to the DB.
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string createEmplSQL = "insert into employees " +
                        $"VALUES(0, '{emp.Name}', {emp.Hours}, '{emp.Title}')";
                connect.Open();
                connect.Query<Employee>(createEmplSQL);
                connect.Close();
            }
        }
        public void UpdateEmployee(Employee emp)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string updateSql = "update employees " +
                    $"set `name`='{emp.Name}', hours={emp.Hours}, title='{emp.Title}' " +
                    $"where id={emp.ID}";
                connect.Open();
                connect.Query<Employee>(updateSql);
                connect.Close();
            }
        }
        public void DeleteEmployee(int ID) //void because we dont need anything to return
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string EmpDelSql = "delete from employees where id=" + ID; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                connect.Query<Employee>(EmpDelSql); 
                connect.Close();

            }
        }
    }
}
