using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using PMS.BL.Model;
using System.Threading.Tasks;

namespace PMS.BL.Core
{
    public class DataBase
    {
        protected string ConnectionString { get; set; }
        public virtual bool UpdateRecord<TEntity>(TEntity record) where TEntity : class
        {
            if (record == null)
            {
                return false;
            }
            return true;
        }
        
        public List<TEntity> SelectMethod<TEntity>() where TEntity : class
        {
            List<TEntity> objectList;

            try
            {
                using (var dC = new DataContext(ConnectionString))
                {                    
                    objectList = dC.GetTable<TEntity>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objectList;
        }

        public virtual bool InsertMethod<TEntity>(TEntity obj) where TEntity : class
        {
            bool result = true;
            if (obj != null)
            {
                try
                {
                    using (var dC = new DataContext(ConnectionString))
                    {
                        dC.GetTable<TEntity>().InsertOnSubmit(obj);
                        dC.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                result = false;
            }           

            return result;
        }

        public virtual bool InsertMethod<TEntity>(List<TEntity> _objectsList) where TEntity : class
        {
            bool result = true;
            if (_objectsList != null && _objectsList.Count != 0)
            {
                try
                {
                    using (var dC = new DataContext(ConnectionString))
                    {
                        dC.GetTable<TEntity>().InsertAllOnSubmit(_objectsList);
                        dC.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool DeleteRecord<TEntity>(TEntity record) where TEntity : class
        {
            bool result = true;

            if (record != null)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        dC.GetTable<TEntity>().Attach(record);
                        dC.GetTable<TEntity>().DeleteOnSubmit(record);
                        dC.SubmitChanges();
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

        
        public DataBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public DataBase()
        {
            GetDefaultConnectionString();
        }

        private string GetDefaultConnectionString()
        {
            //localhost // TESTVM
            return ConnectionString = "Data Source=localhost; Initial Catalog = PMS; Persist Security Info=True; User ID=test;Password=test;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";            
        }

        
    }
}
