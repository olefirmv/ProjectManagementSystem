using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class StageLineDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is StageLine)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        StageLine obj = record as StageLine;
                        var tempObj = dC.GetTable<StageLine>().First(x => x.ProjectID == obj.ProjectID && x.SectionNum == obj.SectionNum && x.StageNum == obj.StageNum && x.StageLineID == obj.StageLineID);
                        if (tempObj != null)
                        {
                            tempObj.Status = obj.Status;
                            tempObj.Name = obj.Name;
                            tempObj.DocumentID = obj.DocumentID;
                            tempObj.ComponentID = obj.ComponentID;
                            tempObj.StartDate = obj.StartDate;
                            tempObj.EndDate = obj.EndDate;
                            tempObj.EmployeeID = obj.EmployeeID;
                            tempObj.ModifyDateTime = DateTime.Now;
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
