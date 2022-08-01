using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class FormDB: DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Form)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Form obj = record as Form;
                        var tempObj = dC.GetTable<Form>().First(x => x.FormID == obj.FormID);
                        if (tempObj != null)
                        {
                            tempObj.Name = obj.Name;                            
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
