using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Models
{
    public class ToDoDAL
    {
        public List<ToDo> GetToDos() //for index to pass in list of all empls and fields
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string toDoSql = $"select * from todos";

                connect.Open();
                List<ToDo> toDos = connect.Query<ToDo>(toDoSql).ToList();
                connect.Close();

                return toDos;
            }
        }
        public ToDo GetToDo(int ID)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string singleToDoSql = $"select * from todos where id={ID}";

                connect.Open();
                ToDo toDo = connect.Query<ToDo>(singleToDoSql).First();
                connect.Close();
                return toDo;
            }
        }
        public void CreateToDo(ToDo tD) //void, since you already have an empl object at this point
                                                 //and are just adding to the DB
                                                 //NOTE: passing 0 for IsCompleted, since why would we create an item that is done.
        {
            string createTDSQL;
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                if (tD.Employee != null)

                {
                    createTDSQL = "insert into todos " +
                    $"VALUES(0, '{tD.Name}', '{tD.Description}', {tD.AssignedTo}, {tD.Duration}, 0)";
                }
                else
                {
                    createTDSQL = "insert into todos " +
                    $"VALUES(0, '{tD.Name}', '{tD.Description}', null, {tD.Duration}, 0)";
                }

                //NOTE: Passing in an assignedTo int, so we need to allow users to select an employee when working with a new ToDo item.
                //Consider using a ViewModel to pull in that information/foreign keys as needed.
                connect.Open();
                connect.Query<ToDo>(createTDSQL);
                connect.Close();
            }
        }
        public void UpdateToDo(ToDo tD)
        {
            //Need to add in functionality if the task is marked as complete (here and in the view)
            
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string updateSql ;

                if(tD.Employee != null)
                
                {
                    updateSql = "update todos " +
                    $"set `name`='{tD.Name}', description='{tD.Description}', assignedto={tD.Employee.ID}, duration={tD.Duration}, iscompleted = {tD.IsCompleted} " +
                    $"where id={tD.ID}";
                }
                else
                {
                    updateSql = "update todos " +
                    $"set `name`='{tD.Name}', description='{tD.Description}', duration={tD.Duration}, iscompleted = {tD.IsCompleted} " +
                    $"where id={tD.ID}";
                }

                connect.Open();
                connect.Query<ToDo>(updateSql);
                connect.Close();
            }
        }
        public void DeleteToDo(int ID) //void because we dont need anything to return
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string tDDelSql = "delete from employees where id=" + ID; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                connect.Query<Employee>(tDDelSql);
                connect.Close();

            }
        }
    }
}
