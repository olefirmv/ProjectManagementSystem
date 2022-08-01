using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "ProjectRole")]
    public class ProjectRole
    {        
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int SectionNum { get; set; }

        [Column(IsPrimaryKey = true)]
        public int ComponentID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int RoleID { get; set; }

        [Column(Name = "MemberID")]
        public int MemberID { get; set; }

        public ProjectRole(int projectID, int sectionNum, int componentID, int roleID, int memberID)
        {
            ProjectID = projectID;
            SectionNum = sectionNum;
            ComponentID = componentID;
            RoleID = roleID;
            MemberID = memberID;
        }
        public ProjectRole()
        {

        }
    }
}
