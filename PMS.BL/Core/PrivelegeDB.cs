using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public class PrivelegeDB : DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Privelege)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Privelege obj = record as Privelege;
                        var tempObj = dC.GetTable<Privelege>().First(x => x.PrivelegeID == obj.PrivelegeID);
                        if (tempObj != null)
                        {
                            tempObj.ReadOne = obj.ReadOne;
                            tempObj.ReadFull = obj.ReadFull;
                            tempObj.EditOne = obj.EditOne;
                            tempObj.EditFull = obj.EditFull;
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

        public Privelege FindOrCreate(Privelege privelege)
        {
            Privelege GetPrivelege(DataContext _dC, Privelege _privelege)
            {
                Table<Privelege> priveleges = _dC.GetTable<Privelege>();

                if (priveleges != null && priveleges.Count() > 0)
                {
                    return priveleges.FirstOrDefault(p =>
                        p.ReadOne == _privelege.ReadOne
                        && p.EditOne == _privelege.EditOne
                        && p.ReadFull == _privelege.ReadFull
                        && p.EditFull == _privelege.EditFull);
                }
                else
                {
                    return null;
                }
            }

            Privelege resPrivelege = new Privelege();

            try
            {
                using (DataContext dC = new DataContext(ConnectionString))
                {
                    Privelege tmpPrivelege = GetPrivelege(dC, privelege);        

                    if (tmpPrivelege != null)
                    {
                        resPrivelege = tmpPrivelege;
                    }
                    else
                    {
                        dC.GetTable<Privelege>().InsertOnSubmit(privelege);
                        dC.SubmitChanges();

                        resPrivelege = GetPrivelege(dC, privelege);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resPrivelege;
        }

        public override bool InsertMethod<TEntity>(TEntity obj)
        {
            return false;
        }

        public Privelege GetPrivelegeByEmployeeAndProject(Employee employee, Project project)
        {

            Privelege resPrivelege = new Privelege();
            try
            {
                using (DataContext dC = new DataContext(ConnectionString))
                {
                    resPrivelege = (from pR in dC.GetTable<Privelege>()
                                    join pE in dC.GetTable<ProjectEmployee>() on pR.PrivelegeID equals pE.PrivelegeID
                                    where pE.EmployeeID == employee.EmployeeID && pE.ProjectID == project.ProjectID
                                    select pR).First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resPrivelege;

        }
    }
}
