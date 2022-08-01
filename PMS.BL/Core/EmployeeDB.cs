using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class EmployeeDB: DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Employee)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Employee obj = record as Employee;
                        var tempObj = dC.GetTable<Employee>().First(x => x.EmployeeID == obj.EmployeeID);
                        if (tempObj != null)
                        {
                            tempObj.Name = obj.Name;
                            tempObj.SecondName = obj.SecondName;
                            tempObj.Patronymic = obj.Patronymic;
                            tempObj.Mail = obj.Mail;
                            tempObj.Login = obj.Login;
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