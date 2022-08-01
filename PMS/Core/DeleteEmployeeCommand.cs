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
    public class DeleteEmployeeCommand: ICommand
    {
        public DeleteEmployeeCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is EmployeeListPageViewModel)
            {
                EmployeeListPageViewModel employeeListPageVM = parameter as EmployeeListPageViewModel;

                if (employeeListPageVM.SelectedMember != null)
                {
                    BindingList<TmpEmployeePrivelege> collection = employeeListPageVM.ProjectEmployeeList as BindingList<TmpEmployeePrivelege>;

                    TmpEmployeePrivelege tmpEmployeePrivelege = employeeListPageVM.SelectedMember as TmpEmployeePrivelege;

                    ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();
                    ProjectEmployee projectEmployee = projectEmployeeDB.SelectMethod<ProjectEmployee>().First(pE => pE.ProjectID == employeeListPageVM.Project.ProjectID && pE.EmployeeID == tmpEmployeePrivelege.EmployeeID);

                    projectEmployeeDB.DeleteRecord<ProjectEmployee>(projectEmployee);

                    collection.Remove(tmpEmployeePrivelege);
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
