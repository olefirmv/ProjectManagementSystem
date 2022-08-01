using PMS.BL.Core;
using PMS.BL.Model;
using PMS.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PMS.Core
{
    public class ChangeEmployeePasswordCommand : ICommand
    {
        public ChangeEmployeePasswordCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is EmployeeInfoViewModel)
            {
                EmployeeInfoViewModel employeeInfoVM = parameter as EmployeeInfoViewModel;

                if (employeeInfoVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is EmployeeInfoViewModel)
            {
                EmployeeInfoViewModel employeeInfoVM = parameter as EmployeeInfoViewModel;

                if (employeeInfoVM != null)
                {
                    CredentialDB credentialDB = new CredentialDB();
                    Credential credential = credentialDB.SelectMethod<Credential>().First(c => c.Login == employeeInfoVM.Employee.Login);

                    if (credential.Password == PMSBLHelper.EncryptPassword(employeeInfoVM.Password))
                    {
                        credential.Password = PMSBLHelper.EncryptPassword(employeeInfoVM.NewPassword);

                        credentialDB.UpdateRecord<Credential>(credential);

                        employeeInfoVM.Password = null;
                        employeeInfoVM.NewPassword = null;
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

    }
}
