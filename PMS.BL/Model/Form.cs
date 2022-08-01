using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "Form")]
    public class Form
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int FormID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        public Form(string _name)
        {
            Name = _name;
        }

        public Form()
        {

        }
    }
}
