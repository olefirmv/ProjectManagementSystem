using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;


namespace PMS.BL.Core
{
    public class ProjectDB : DataBase
    {        
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Project)
            {
                
                Project obj = record as Project;
                DateTime tmp = obj.ModifyDateTime;
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        obj.ModifyDateTime = DateTime.Now;

                        var tempObj = dC.GetTable<Project>().First(x => x.ProjectID == obj.ProjectID);

                        if (tempObj != null)
                        {
                            tempObj.ProjectID = obj.ProjectID;
                            tempObj.Name = obj.Name;
                            tempObj.StartDate = obj.StartDate;
                            tempObj.EndDate = obj.EndDate;
                            tempObj.Status = obj.Status;
                            tempObj.ModifyDateTime = obj.ModifyDateTime;
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
                    obj.ModifyDateTime = tmp;
                    result = false;
                }

            }
            else
            {
                result = false;
            }

            return result;
        }
        public List<Project> GetProjectsByEmployeeAndPrivelege(Employee employee, int privelegeID, bool privelegeEqual)
        {
           
            List<Project> resProjList = new List<Project>();
            try
            {
                using (DataContext dC = new DataContext(ConnectionString))
                {
                    if (privelegeEqual)
                    {
                        resProjList = (from p in dC.GetTable<Project>()
                                        join pE in dC.GetTable<ProjectEmployee>() on p.ProjectID equals pE.ProjectID
                                        where pE.EmployeeID == employee.EmployeeID && pE.PrivelegeID == privelegeID
                                        select p).ToList();
                    }
                    else 
                    {
                        resProjList = (from p in dC.GetTable<Project>()
                                        join pE in dC.GetTable<ProjectEmployee>() on p.ProjectID equals pE.ProjectID
                                        where pE.EmployeeID == employee.EmployeeID && pE.PrivelegeID != privelegeID
                                        select p).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resProjList;

        }


        public bool DeleteProject(Project project)
        {
            bool result = true;

            if (project != null)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        //projectrole
                        try
                        {
                            Table<ProjectRole> projectRoleCollection = dC.GetTable<ProjectRole>();
                            IQueryable<ProjectRole> projectRoleCollectionByProjectID = projectRoleCollection.Where(pR => pR.ProjectID == project.ProjectID);
                            projectRoleCollection.DeleteAllOnSubmit(projectRoleCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch(Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы ProjectRole");
                            throw;
                        }
                        //member
                        try
                        {
                            Table<Member> memberCollection = dC.GetTable<Member>();
                            IQueryable<Member> memberCollectionByProjectID = memberCollection.Where(m => m.ProjectID == project.ProjectID);
                            memberCollection.DeleteAllOnSubmit(memberCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы Member");
                            throw;
                        }
                        //stageline
                        try
                        {
                            Table<StageLine> stagelineCollection = dC.GetTable<StageLine>();
                            IQueryable<StageLine> stagelineCollectionByProjectID = stagelineCollection.Where(m => m.ProjectID == project.ProjectID);
                            stagelineCollection.DeleteAllOnSubmit(stagelineCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }

                        //stage
                        try
                        {
                            Table<Stage> stageCollection = dC.GetTable<Stage>();
                            IQueryable<Stage> stageCollectionByProjectID = stageCollection.Where(m => m.ProjectID == project.ProjectID);
                            stageCollection.DeleteAllOnSubmit(stageCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }

                        //Component
                        try
                        {
                            Table<Component> componentCollection = dC.GetTable<Component>();
                            IQueryable<Component> componentCollectionByProjectID = componentCollection.Where(m => m.ProjectID == project.ProjectID);
                            componentCollection.DeleteAllOnSubmit(componentCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }

                        //Section
                        try
                        {
                            Table<Section> sectionCollection = dC.GetTable<Section>();
                            IQueryable<Section> sectionCollectionByProjectID = sectionCollection.Where(m => m.ProjectID == project.ProjectID);
                            sectionCollection.DeleteAllOnSubmit(sectionCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }
                        //ProjectEmployee
                        try
                        {
                            Table<ProjectEmployee> projectEmployeeCollection = dC.GetTable<ProjectEmployee>();
                            IQueryable<ProjectEmployee> projectEmployeeCollectionByProjectID = projectEmployeeCollection.Where(m => m.ProjectID == project.ProjectID);
                            projectEmployeeCollection.DeleteAllOnSubmit(projectEmployeeCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }
                        //Project
                        try
                        {
                            Table<Project> projectCollection = dC.GetTable<Project>();
                            IQueryable<Project> projectCollectionByProjectID = projectCollection.Where(m => m.ProjectID == project.ProjectID);
                            projectCollection.DeleteAllOnSubmit(projectCollectionByProjectID);
                            dC.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            new Exception("Не удалось удалить данные из таблицы StageLine");
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
