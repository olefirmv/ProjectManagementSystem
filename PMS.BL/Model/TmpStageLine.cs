using PMS.BL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BL.Model
{
    public class TmpStageLine
    {
        public int Counter { get; set; }

        public int ProjectID { get; set; }


        public int SectionNum { get; set; }


        public int StageNum { get; set; }


        public int StageLineID { get; set; }


        public string Name { get; set; }


        public string Status { get; set; }


        public string DocumentName { get; set; }
        public int  DocumentID { get; set; }


        public int ComponentID { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


        public string EmployeeInfo { get; set; }

        public int EmployeeID { get; set; }


        public DateTime ModifyDateTime { get; set; }

        public TmpStageLine(StageLine stageLine)
        {
            Counter = stageLine.StageLineID;
            ProjectID = stageLine.ProjectID;
            SectionNum = stageLine.SectionNum;
            StageNum = stageLine.StageNum;
            StageLineID = stageLine.StageLineID;
            Name = stageLine.Name;
            Status = StatusIntToString(stageLine.Status);
            DocumentName = SetDocumentName(stageLine.DocumentID);
            DocumentID = stageLine.DocumentID;
            StartDate = stageLine.StartDate;
            EndDate = stageLine.EndDate;
            EmployeeInfo = SetEmployeeInfo(stageLine.EmployeeID);
            EmployeeID = stageLine.EmployeeID;
            ModifyDateTime = stageLine.ModifyDateTime;
            ComponentID = stageLine.ComponentID;
        }

        public TmpStageLine()
        {

        }

        public bool ValidateWrite()
        {
            return true;
        }

        public string SetEmployeeInfo(int employeeId)
        {
            EmployeeDB employeeDB = new EmployeeDB();
            Employee employee = employeeDB.SelectMethod<Employee>().First(e => e.EmployeeID == employeeId);

            return employee.SecondName + " " + employee.Name + " " + employee.Patronymic;
        }

        public string StatusIntToString(int value)
        {
            switch(value)
            {
                case 1:
                    return "Ожидает выполнения";
                case 2:
                    return "В процессе";
                case 3:
                    return "Выполнен";
                default:
                    return "ошибка";
            }
        }
        public int StatusStringToInt(string value)
        {
            switch (value)
            {
                case "Ожидает выполнения":
                    return 1;
                case "В процессе":
                    return 2;
                case "Выполнен":
                    return 3;
                default:
                    return 4;
            }
        }

        public string SetDocumentName(int documentId)
        {
            DocumentDB employeeDB = new DocumentDB();
            Document document = employeeDB.SelectMethod<Document>().First(d => d.DocumentID == documentId);

            return document.Name;
        }

    }
}
