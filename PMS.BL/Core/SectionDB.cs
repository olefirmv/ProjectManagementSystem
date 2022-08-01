using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class SectionDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Section)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Section obj = record as Section;
                        var tempObj = dC.GetTable<Section>().First(x => x.ProjectID == obj.ProjectID && x.SectionNum == obj.SectionNum);
                        if (tempObj != null)
                        {
                            tempObj.Cipher = obj.Cipher;
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
