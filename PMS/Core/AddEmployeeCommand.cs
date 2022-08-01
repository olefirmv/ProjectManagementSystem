using PMS.BL.Core;
using PMS.BL.Model;
using PMS.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace PMS.Core
{
    public class AddEmployeeCommand: ICommand
    {
        public AddEmployeeCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is EmployeeListPageViewModel)
            {
                EmployeeListPageViewModel employeeListPageVM = parameter as EmployeeListPageViewModel;

                if (employeeListPageVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is EmployeeListPageViewModel)
            {
                EmployeeListPageViewModel employeeListPageVM = parameter as EmployeeListPageViewModel;

                if (employeeListPageVM != null)
                {
                    TmpEmployeePrivelege tmpEmployeePrivelege = new TmpEmployeePrivelege();

                    tmpEmployeePrivelege.EmployeeID = (int) employeeListPageVM.EmployeeID;

                    EmployeeDB employeeDB = new EmployeeDB();
                    Employee employee = employeeDB.SelectMethod<Employee>().First(e => e.EmployeeID == tmpEmployeePrivelege.EmployeeID);

                    if (employee != null)
                    {
                        PrivelegeDB privelegeDB = new PrivelegeDB();
                        Privelege privelege = privelegeDB.FindOrCreate(new Privelege(employeeListPageVM.ReadOne, employeeListPageVM.ReadFull, employeeListPageVM.EditOne, employeeListPageVM.EditFull));

                        ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();
                        ProjectEmployee projectEmployee = new ProjectEmployee(employeeListPageVM.Project.ProjectID, employee.EmployeeID, privelege.PrivelegeID);
                        projectEmployeeDB.InsertMethod(projectEmployee);
                        employeeListPageVM.SetTmpEmployeePrivelegeList(employeeListPageVM.InitProjectEmployeeList());
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
