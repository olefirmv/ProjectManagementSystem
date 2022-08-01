using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Core;
using PMS.BL.Core;
using PMS.BL.Model;
using System.Windows.Input;

namespace PMS.MVVM.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        public ICommand LoginCommand { get; private set; }
        
        public MainViewModel ParentMain { get; set; }

        public Credential Credential { get; }
        
        public string Login
        {
            get
            {
                return Credential.Login;
            }
            set
            {
                Credential.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get
            {
                return Credential.Password;
            }
            set
            {
                Credential.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginViewModel()
        {
            Credential = new Credential();
            LoginCommand = new LoginCommand();
        }

    }
}
