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
    public class SaveComponentProjectRoleCommand: ICommand
    {
        public SaveComponentProjectRoleCommand()
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
                    List<ProjectRole> resList = new List<ProjectRole>();

                    foreach (var item in tmpComponentProjectRoles)
                    {
                        ProjectRole projectRole = new ProjectRole();
                        projectRole.ProjectID = componentProjectRoleVM.Project.ProjectID;
                        projectRole.SectionNum = componentProjectRoleVM.Component.SectionNum;
                        projectRole.MemberID = item.MemberID;
                        projectRole.RoleID = item.RoleID;
                        projectRole.ComponentID = componentProjectRoleVM.Component.ComponentID;
                        resList.Add(projectRole);
                    }

                    ProjectRoleDB projectRoleDB = new ProjectRoleDB();
                    projectRoleDB.Write(resList);

                    ProjectDB projectDB = new ProjectDB();
                    projectDB.UpdateRecord<Project>(componentProjectRoleVM.Project);

                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
