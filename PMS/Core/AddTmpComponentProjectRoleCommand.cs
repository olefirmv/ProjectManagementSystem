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
    public class AddTmpComponentProjectRoleCommand : ICommand
    {

        public AddTmpComponentProjectRoleCommand()
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
                    TmpProjectRole tmpProjectRole = new TmpProjectRole();

                    tmpProjectRole.MemberID = (int)componentProjectRoleVM.SelectedMember;
                    tmpProjectRole.RoleID = (int)componentProjectRoleVM.SelectedRole;

                    (componentProjectRoleVM.TmpComponentProjectRoles as BindingList<TmpProjectRole>).Add(tmpProjectRole);
                }
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
