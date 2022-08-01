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
    public class DeleteTmpProjectRoleCommand : ICommand
    {
        public DeleteTmpProjectRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is ProjectInfoViewModel)
            {
                ProjectInfoViewModel projectInfoVM = parameter as ProjectInfoViewModel;

                if (projectInfoVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is ProjectInfoViewModel)
            {
                ProjectInfoViewModel projectInfoVM = parameter as ProjectInfoViewModel;

                if (projectInfoVM != null)
                {
                    BindingList<TmpProjectRole> tmpProjectRoles = projectInfoVM.TmpProjectRoles as BindingList<TmpProjectRole>;
                    TmpProjectRole tmpProjectRole = projectInfoVM.SelectedTmpProjectRole as TmpProjectRole;

                    tmpProjectRoles.Remove(tmpProjectRole);
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
