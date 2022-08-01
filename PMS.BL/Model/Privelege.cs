using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "Privelege")]
    public class Privelege
    {
        
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int PrivelegeID { get; set; }

        [Column(Name = "ReadOne")]
        public bool ReadOne { get; set; }
        
        [Column(Name = "ReadFull")]
        public bool ReadFull { get; set; }
        
        [Column(Name = "EditOne")]
        public bool EditOne { get; set; }
        
        [Column(Name = "EditFull")]
        public bool EditFull { get; set; }

        public Privelege(bool readOne, bool readFull, bool editOne, bool editFull)
        {
            ReadOne = readOne;
            ReadFull = readFull;
            EditOne = editOne;
            EditFull = editFull;
        }
        public Privelege()
        {

        }
    }
}
