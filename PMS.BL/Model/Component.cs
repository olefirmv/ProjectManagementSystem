using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using PMS.BL.Core;

namespace PMS.BL.Model
{
    [Table(Name = "Component")]
    public class Component
    {  
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int SectionNum { get; set; }

        [Column(IsPrimaryKey = true)]
        public int ComponentID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Cipher")]
        public string Cipher { get; set; }

        public string SectionName
        {
            get
            {
                return GetSectionName();
            }
        }

        public Component(int projectID, int sectionNum, int componentID, string name, string cipher)
        {
            ProjectID = projectID;
            SectionNum = sectionNum;
            ComponentID = componentID;
            Name = name;
            Cipher = cipher;
        }

        public Component()
        { 

        }

        public string GetSectionName()
        {
            SectionEnum sectionEnum = (SectionEnum)SectionNum;

            return PMSBLHelper.GetDescriptionByEnumType(sectionEnum);
        }
    }
}
