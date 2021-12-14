using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [Range (0,40)]
        public int Hours { get; set; }
        public string Title { get; set; }
        public List<ToDo> ToDoList { get; set; }

    }
}
