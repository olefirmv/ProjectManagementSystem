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
    public class EditTmpProjectRoleCommand : ICommand
    {
        public EditTmpProjectRoleCommand()
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
                    var tmpProjectRole = projectInfoVM.SelectedTmpProjectRole as TmpProjectRole;

                    projectInfoVM.SelectedMember = tmpProjectRole.MemberID;
                    projectInfoVM.SelectedRole = tmpProjectRole.RoleID;
                    projectInfoVM.SelectedSection = tmpProjectRole.SectionNum;
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
