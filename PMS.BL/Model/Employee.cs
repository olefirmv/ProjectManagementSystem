using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "Employee")]
    public class Employee
    {        
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int EmployeeID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "SecondName")]
        public string SecondName { get; set; }

        [Column(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Column(Name = "Mail")]
        public string Mail { get; set; }

        [Column(Name = "Login")]
        public string Login { get; set; }

        public Employee(string name, string secondName, string patronymic, string mail, string login)
        {
            Name = name;
            SecondName = secondName;
            Patronymic = patronymic;
            Mail = mail;
            Login = login;
        }
        public Employee()
        {

        }
    }
}
