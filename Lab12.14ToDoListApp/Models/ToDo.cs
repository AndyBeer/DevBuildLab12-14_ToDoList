using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12._14ToDoListApp.Models
{
    public class ToDo
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        public Employee Employee { get; set; }
    }
}
