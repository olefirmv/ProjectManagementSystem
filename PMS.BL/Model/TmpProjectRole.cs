using PMS.BL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BL.Model
{
    public class TmpProjectRole
    {
        public int MemberID { get; set; }
        public int RoleID { get; set; }
        public int SectionNum { get; set; }

        public string MemberName
        {
            get
            {
                return GetMemberName();
            }
        }
        
        public string RoleName
        {
            get
            {
                return GetRoleName();
            }
        }
        
        public string SectionName
        {
            get
            {
                return GetSectionName();
            }
        }



        public TmpProjectRole()
        {

        }

        public string GetMemberName()
        {
            MemberDB memberDB = new MemberDB();

            return memberDB.SelectMethod<Member>().First(x => x.MemberID == MemberID).Name;
        }
        
        public string GetRoleName()
        {
            RoleDB roleDB = new RoleDB();

            return roleDB.SelectMethod<Role>().First(x => x.RoleID == RoleID).Name;
        }
        
        public string GetSectionName()
        {
            SectionEnum sectionEnum = (SectionEnum)SectionNum;
            return PMSBLHelper.GetDescriptionByEnumType(sectionEnum);
        }


    }
}
