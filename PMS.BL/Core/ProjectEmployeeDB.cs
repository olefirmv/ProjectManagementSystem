using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class ProjectEmployeeDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is ProjectEmployee)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        ProjectEmployee obj = record as ProjectEmployee;
                        var tempObj = dC.GetTable<ProjectEmployee>().First(x => x.ProjectID == obj.ProjectID && x.EmployeeID == obj.EmployeeID);
                        if (tempObj != null)
                        {
                            tempObj.PrivelegeID = obj.PrivelegeID;
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
    }
}