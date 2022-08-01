using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using PMS.BL.Core;

namespace PMS.BL.Model
{
    [Table(Name = "Project")]
    public class Project
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ProjectID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Column(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Column(Name = "CreateDateTime", IsDbGenerated = true)]
        public DateTime CreateDateTime { get; set; }

        [Column(Name = "ModifyDateTime")]
        public DateTime ModifyDateTime { get; set; }

        [Column(Name = "Status")]
        public int Status { get; set; }

        public Project(string name, DateTime startDate, DateTime endDate, int projectStatus)
        {            
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Status = projectStatus;
        }
        public Project()
        {

        }

        public Section AddSection(SectionEnum sectionEnum, string sectionCipher)
        {
            Section section = new Section(ProjectID, (int)sectionEnum, sectionCipher);

            if (section.ValidateWrite())
            {
                DataBase dataBase = new DataBase();

                if (!dataBase.InsertMethod(section))
                {
                    return null;
                }
            }
            else
            {
                throw new Exception("Заполните шифр секции");
            }

            return section;
        }

        public ProjectRole AddProjectMemberRole(int sectionId, int componentId, int roleId, int memberId)
        {
            ProjectRole role = new ProjectRole(ProjectID, sectionId, componentId, roleId, memberId);
            DataBase dataBase = new DataBase();

            if (!dataBase.InsertMethod(role))
            {
                return null;
            }
            return role;
        }

        public bool IsSectionExist(int sectionNum)
        {
            bool result = false;

            if (this.GetSectionByNumber(sectionNum) != null)
            {
                result = true;
            }

            return result;
        }

        public Section GetSectionByNumber(int sectionNum)
        {
            DataBase dataBase = new DataBase();

            Section section = dataBase.SelectMethod<Section>().FirstOrDefault(s => s.ProjectID == this.ProjectID && s.SectionNum == sectionNum);
            return section;
            
        }
    }
}