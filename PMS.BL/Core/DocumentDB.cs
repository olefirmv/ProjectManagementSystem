using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;


namespace PMS.BL.Core
{
    public class DocumentDB: DataBase
    {
        public override bool UpdateRecord<TEntity>(TEntity record)
        {
            bool result = true;

            if (record != null && record is Document)
            {
                try
                {
                    using (DataContext dC = new DataContext(ConnectionString))
                    {
                        Document obj = record as Document;
                        var tempObj = dC.GetTable<Document>().First(x => x.DocumentID == obj.DocumentID);
                        if (tempObj != null)
                        {                            
                            tempObj.Name = obj.Name;
                            tempObj.Agrees = obj.Agrees;
                            tempObj.Claims = obj.Claims;
                            tempObj.Develops = obj.Develops;
                            tempObj.Comment = obj.Comment;
                            tempObj.FormID = obj.FormID;
                            tempObj.SectionNum = obj.SectionNum;
                            tempObj.StageNum = obj.StageNum;
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
