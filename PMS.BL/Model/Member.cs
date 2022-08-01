using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;


namespace PMS.BL.Model
{
    [Table(Name = "Member")]
    public class Member
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int MemberID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "ProjectID")]
        public int ProjectID { get; set; }

        public Member(string _name, int _projectID)
        {
            Name = _name;
            ProjectID = _projectID;
        }

        public Member()
        {

        }
    }
}
