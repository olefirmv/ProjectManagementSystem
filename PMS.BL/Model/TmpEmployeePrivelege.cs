using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BL.Model
{
    public class TmpEmployeePrivelege
    {
        public int EmployeeID { get; set; }
        public string EmployeeInfo { get; set; }
        public string ReadOne { get; set; }
        public string ReadFull { get; set; }
        public string EditOne { get; set; }
        public string EditFull { get; set; }

        public TmpEmployeePrivelege(Employee employee, Privelege privelege)
        {

        }
        public TmpEmployeePrivelege()
        {

        }

        public string SetEmployeeInfo (string name, string secondName, string patronymic)
        {
            return name + " " + secondName + " " + patronymic;
        }
        public string SetEmployeeInfo (Employee employee)
        {
            return employee.SecondName + " " + employee.Name + " " + employee.Patronymic;
        }
        
        public void SetRights(Privelege privelege)
        {
            ReadOne = BoolToString(privelege.ReadOne);
            ReadFull = BoolToString(privelege.ReadFull);
            EditOne = BoolToString(privelege.EditOne);
            EditFull = BoolToString(privelege.EditFull);
        }
        
        public string BoolToString(bool tmp)
        {
            if (tmp)
            {
                return "да";
            }
            else return "нет";
        }

        public bool StringToBool(string tmp)
        {
            if (tmp == "да")
            {
                return true;
            }
            else return false;
        }


    }
}
