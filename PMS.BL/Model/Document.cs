using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using PMS.BL.Core;

namespace PMS.BL.Model
{
    [Table(Name = "Document")]
    public class Document
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int DocumentID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Agrees")]
        public string Agrees { get; set; }

        [Column(Name = "Claims")]
        public string Claims { get; set; }

        [Column(Name = "Develops")]
        public string Develops { get; set; }

        [Column(Name = "Comment")]
        public string Comment { get; set; }

        [Column(Name = "FormID")]
        public int FormID { get; set; }

        [Column(Name = "SectionNum")]
        public int SectionNum { get; set; }

        [Column(Name = "StageNum")]
        public int StageNum { get; set; }

        public string FormName
        {
            get
            {
                return GetFormName();
            }
        }
        public string SectionName
        {
            get
            {
                return GetSectionName();
            }
        }

        public string StageName
        {
            get
            {
                return GetStageName();
            }
        }

        public Document(string name, string agrees, string claims, string develops, string comment, int formID, int sectionNum, int stageNum)
        {
            Name = name;
            Agrees = agrees;
            Claims = claims;
            Develops = develops;
            Comment = comment;
            FormID = formID;
            SectionNum = sectionNum;
            StageNum = stageNum;
        }
        public Document()
        {

        }

        public string GetDevelops()
        {
            string ret = "";

            if (Develops != null && Develops != string.Empty)
            {
                List<string> roles = Develops.Split(';').ToList();

                RoleDB roleDB = new RoleDB();

                foreach (string role in roles)
                {
                    Role localRole = roleDB.SelectMethod<Role>().First(x => x.RoleID == int.Parse(role));

                    ret += localRole.Name + ", ";
                }

                ret = ret.Remove(ret.Length - 2);
            }

            return ret;
        }

        public string GetAgrees()
        {
            string ret = "";

            if (Agrees != null && Agrees != string.Empty)
            {
                List<string> roles = Agrees.Split(';').ToList();

                RoleDB roleDB = new RoleDB();

                foreach (string role in roles)
                {
                    Role localRole = roleDB.SelectMethod<Role>().First(x => x.RoleID == int.Parse(role));

                    ret += localRole.Name + ", ";
                }

                ret = ret.Remove(ret.Length - 2);
            }

            return ret;
        }

        public string GetClaims()
        {
            string ret = "";

            if (Claims != null && Claims != string.Empty)
            {
                List<string> roles = Claims.Split(';').ToList();

                RoleDB roleDB = new RoleDB();

                foreach (string role in roles)
                {
                    Role localRole = roleDB.SelectMethod<Role>().First(x => x.RoleID == int.Parse(role));

                    ret += localRole.Name + ", ";
                }

                ret = ret.Remove(ret.Length - 2);
            }

            return ret;
        }

        public string GetFormName()
        {
            FormDB formDB = new FormDB();

            return formDB.SelectMethod<Form>().First(x => x.FormID == FormID).Name;
        }

        public string GetSectionName()
        {
            SectionEnum sectionEnum = (SectionEnum)SectionNum;

            return PMSBLHelper.GetDescriptionByEnumType(sectionEnum);
        }

        public string GetStageName()
        {
            string stageName = "";

            SectionEnum sectionEnum = (SectionEnum)SectionNum;

            switch (sectionEnum)
            {
                case SectionEnum.SRW:
                    SRWStagesEnum srwStagesEnum = (SRWStagesEnum)StageNum;
                    stageName = PMSBLHelper.GetDescriptionByEnumType(srwStagesEnum);
                    break;
                case SectionEnum.EDW:
                    EDWStagesEnum edwStagesEnum = (EDWStagesEnum)StageNum;
                    stageName = PMSBLHelper.GetDescriptionByEnumType(edwStagesEnum);
                    break;
            }

            return stageName;
        }

        public List<int> GetDevelopsRoleIDList()
        {
            

            List<int> roles = Develops.Split(';').Select(r => int.Parse(r)).ToList();

            return roles;
        }

        public List<int> GetAgreesRoleIDList()
        {
            List<int> roles = Agrees.Split(';').Select(r => int.Parse(r)).ToList();

            return roles;
        }

        public List<int> GetClaimsRoleIDList()
        {
            List<int> roles = Claims.Split(';').Select(r => int.Parse(r)).ToList();

            return roles;
        }
    }
}