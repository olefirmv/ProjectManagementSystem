using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class ProjectRoleDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is ProjectRole)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        ProjectRole obj = record as ProjectRole;
                        var tempObj = dC.GetTable<ProjectRole>().First(x => x.ProjectID == obj.ProjectID && x.SectionNum == obj.SectionNum && x.ComponentID == obj.ComponentID && x.RoleID == obj.RoleID);
                        if (tempObj != null)
                        {
                            tempObj.MemberID = obj.MemberID;
                            dC.SubmitChanges();
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //
                    result = false;
                }

            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool Write(List<ProjectRole> projectRolesList)
        {
            ProjectRole GetProjectRole(DataContext _dC, ProjectRole _projectRole)
            {
                Table<ProjectRole> projectRolesT = _dC.GetTable<ProjectRole>();

                return projectRolesT.FirstOrDefault( x => 
                x.ProjectID == _projectRole.ProjectID && 
                x.SectionNum == _projectRole.SectionNum && 
                x.ComponentID == _projectRole.ComponentID && 
                x.RoleID == _projectRole.RoleID);
                
            }
            
            bool result = true;

            if (projectRolesList != null && projectRolesList.Count != 0)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        foreach (var item in projectRolesList)
                        {
                            var tmppR = GetProjectRole(dC, item);
                            if (tmppR == null)
                            {
                                dC.GetTable<ProjectRole>().InsertOnSubmit(item);
                                dC.SubmitChanges();
                            }
                            else
                            { 
                                tmppR.MemberID = item.MemberID;
                                dC.SubmitChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //
                    result = false;
                }

            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
