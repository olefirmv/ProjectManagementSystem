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
    public class DeleteTmpComponentProjectRoleCommand: ICommand
    {
        public DeleteTmpComponentProjectRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is ComponentProjectRoleViewModel)
            {
                ComponentProjectRoleViewModel componentProjectRoleVM = parameter as ComponentProjectRoleViewModel;

                if (componentProjectRoleVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is ComponentProjectRoleViewModel)
            {
                ComponentProjectRoleViewModel componentProjectRoleVM = parameter as ComponentProjectRoleViewModel;

                if (componentProjectRoleVM != null)
                {
                    BindingList<TmpProjectRole> tmpComponentProjectRoles = componentProjectRoleVM.TmpComponentProjectRoles as BindingList<TmpProjectRole>;
                    TmpProjectRole tmpProjectRole = componentProjectRoleVM.SelectedTmpComponentProjectRole as TmpProjectRole;

                    tmpComponentProjectRoles.Remove(tmpProjectRole);
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
