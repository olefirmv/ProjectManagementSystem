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
    public class SaveProjectInfoCommand : ICommand
    {
        public SaveProjectInfoCommand()
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
                    List<ProjectRole> resList = new List<ProjectRole>();

                    foreach (var item in tmpProjectRoles)
                    {
                        ProjectRole projectRole = new ProjectRole();
                        projectRole.ProjectID = projectInfoVM.Project.ProjectID;
                        projectRole.SectionNum = item.SectionNum;
                        projectRole.MemberID = item.MemberID;
                        projectRole.RoleID = item.RoleID;
                        projectRole.ComponentID = 0;
                        resList.Add(projectRole);
                    }

                    ProjectRoleDB projectRoleDB = new ProjectRoleDB();
                    projectRoleDB.Write(resList);

                    ProjectDB projectDB = new ProjectDB();
                    projectDB.UpdateRecord<Project>(projectInfoVM.Project);

                    ProjectDetailsViewModel projectDetailsVM = new ProjectDetailsViewModel(projectInfoVM.Project, projectInfoVM.Parent.Parent, projectInfoVM.Parent.Parent.Parent, true);

                    projectInfoVM.Parent.Parent.Parent.CurrentView = projectDetailsVM;
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
