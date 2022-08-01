using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class RoleDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Role)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Role obj = record as Role;
                        var tempObj = dC.GetTable<Role>().First(x => x.RoleID == obj.RoleID);
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
