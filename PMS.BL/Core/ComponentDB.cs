using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class ComponentDB: DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Component)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Component obj = record as Component;
                        var tempObj = dC.GetTable<Component>().First(x => x.ProjectID == obj.ProjectID && x.SectionNum == obj.SectionNum && x.ComponentID == obj.ComponentID);
                        if (tempObj != null)
                        {                            
                            tempObj.Name = obj.Name;
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
