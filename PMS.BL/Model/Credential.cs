using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace PMS.BL.Model
{
    [Table(Name = "Credential")]
    public class Credential
    {        
        [Column(IsPrimaryKey = true)]
        public string Login { get; set; }

        [Column(Name = "Password")]
        public string Password { get; set; }

        public Credential(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public Credential()
        {

        }

    }
}
