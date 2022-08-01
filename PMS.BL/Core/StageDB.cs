using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class StageDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Stage)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Stage obj = record as Stage;
                        var tempObj = dC.GetTable<Stage>().First(x => x.ProjectID == obj.ProjectID && x.SectionNum == obj.SectionNum && x.StageNum == obj.StageNum);
                        if (tempObj != null)
                        {
                            tempObj.Status = obj.Status;
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
        public int GetNewStageLineId(Stage stage)
        {
            int ret = 0;
            try
            {
                using (DataContext dC = new DataContext(ConnectionString))
                {
                    int count = dC.GetTable<StageLine>().Where(x => x.ProjectID == stage.ProjectID
                       && x.SectionNum == stage.SectionNum
                       && x.StageNum == stage.StageNum).Count();

                    if (count == 0)
                    {
                        ret = 1;
                    }
                    else
                    {
                        ret = dC.GetTable<StageLine>().Where(x => x.ProjectID == stage.ProjectID
                            && x.SectionNum == stage.SectionNum
                            && x.StageNum == stage.StageNum).Max(x => x.StageLineID) + 1;
                    }

                        
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
    }
   
}
