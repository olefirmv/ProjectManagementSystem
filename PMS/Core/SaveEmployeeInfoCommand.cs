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
    public class SaveEmployeeInfoCommand: ICommand
    {
        public SaveEmployeeInfoCommand()
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
                    employeeInfoVM.Employee.Name = employeeInfoVM.Name;
                    employeeInfoVM.Employee.SecondName = employeeInfoVM.SecondName;
                    employeeInfoVM.Employee.Patronymic = employeeInfoVM.Patronymic;
                    employeeInfoVM.Employee.Mail = employeeInfoVM.Email;

                    EmployeeDB employeeDB = new EmployeeDB();

                    employeeDB.UpdateRecord<Employee>(employeeInfoVM.Employee);

                    employeeInfoVM.Parent.Employee = employeeInfoVM.Employee;
                    employeeInfoVM.Parent.Parent.Employee = employeeInfoVM.Employee;

                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
