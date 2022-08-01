using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "Role")]
    public class Role
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int RoleID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        public Role(string name)
        {
            Name = name;
        }
        public Role()
        {

        }
    }
}
