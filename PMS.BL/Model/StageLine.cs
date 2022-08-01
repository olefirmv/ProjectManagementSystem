using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using PMS.BL.Core;

namespace PMS.BL.Model
{
    [Table(Name = "StageLine")]
    public class StageLine
    {       
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int SectionNum { get; set; }

        [Column(IsPrimaryKey = true)]
        public int StageNum { get; set; }

        [Column(IsPrimaryKey = true)]
        public int StageLineID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Status")]
        public int Status { get; set; }
        
        [Column(Name = "DocumentID")]
        public int DocumentID { get; set; }

        [Column(Name = "ComponentID")]
        public int ComponentID { get; set; }

        [Column(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Column(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Column(Name = "EmployeeID")]
        public int EmployeeID { get; set; }

        [Column(Name = "ModifyDateTime")]
        public DateTime ModifyDateTime { get; set; }

        public StageLine(int projectID, int sectionNum, int stageNum, int stageLineID, string name, int status, int documentID,  
            DateTime startDate, DateTime endDate, int employeeID)
        {
            ProjectID = projectID;
            SectionNum = sectionNum;
            StageNum = stageNum;
            StageLineID = stageLineID;
            Name = name;
            Status = status;
            DocumentID = documentID;
            StartDate = startDate;
            EndDate = endDate;
            EmployeeID = employeeID;
            ModifyDateTime = DateTime.Now;
        }

        public StageLine()
        {

        }

        public bool ValidateWrite()
        {
            return true;
        }
       
    }
}
