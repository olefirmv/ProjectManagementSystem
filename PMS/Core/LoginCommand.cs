using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PMS.BL.Model;
using PMS.BL.Test;
using PMS.MVVM.ViewModel;
using PMS.BL.Core;

namespace PMS.Core
{
    public class LoginCommand : ICommand
    {
        public LoginCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is LoginViewModel)
            {
                LoginViewModel loginViewModel = parameter as LoginViewModel;

                if (loginViewModel == null || loginViewModel.Login == null || loginViewModel.Login == string.Empty || loginViewModel.Password == null ||  loginViewModel.Login == string.Empty)
                {
                    ret = false;
                    MessageBox.Show("Введите полностью все данные");
                }
                
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is LoginViewModel)
            {
                LoginViewModel loginVM = parameter as LoginViewModel;
                CredentialDB credentialDB = new CredentialDB();

                bool resAuth = false;

                try
                {
                    credentialDB.Auth(loginVM.Credential);
                    resAuth = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неверные имя пользователя или пароль");
                }


                if (resAuth)
                {
                    EmployeeDB employeeDB = new EmployeeDB();
                   
                    var tempEmployee = employeeDB.SelectMethod<Employee>().First(e => e.Login == loginVM.Login);

                    MainViewModel mainViewModel = loginVM.ParentMain;
                    AllProjectsViewModel allProjectsViewModel = new AllProjectsViewModel(tempEmployee)
                    {
                        Parent = mainViewModel
                    };

                    mainViewModel.Employee = tempEmployee;
                    mainViewModel.OnChecked = true;
                    mainViewModel.AllProjectsVM = allProjectsViewModel;
                    mainViewModel.CurrentView = mainViewModel.AllProjectsVM;
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
