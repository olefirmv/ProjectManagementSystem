using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class MemberDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Member)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Member obj = record as Member;
                        var tempObj = dC.GetTable<Member>().First(x => x.MemberID == obj.MemberID);
                        if (tempObj != null)
                        {
                            tempObj.Name = obj.Name;
                            tempObj.ProjectID = obj.ProjectID;
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
