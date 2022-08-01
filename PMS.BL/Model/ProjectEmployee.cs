using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "ProjectEmployee")]
    public class ProjectEmployee
    {        
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int EmployeeID { get; set; }

        [Column(Name = "PrivelegeID")]
        public int PrivelegeID { get; set; }

        public ProjectEmployee(int projectID, int employeeID, int privelegeID)
        {
            ProjectID = projectID;
            EmployeeID = employeeID;
            PrivelegeID = privelegeID;
        }
        public ProjectEmployee()
        {

        }

    }
}
