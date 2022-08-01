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
    public class DeleteProjectCommand: ICommand
    {
        public DeleteProjectCommand()
        {

        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is AllProjectsViewModel)
            {
                AllProjectsViewModel allProjectsVM = parameter as AllProjectsViewModel;

                if (allProjectsVM != null)
                {

                    if (allProjectsVM.SelectedProject == null)
                    {
                        ret = false;
                        MessageBox.Show("Выберите проект для удаления");
                    }
                }
                else
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }

            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is AllProjectsViewModel)
            {
                AllProjectsViewModel allProjectsVM = parameter as AllProjectsViewModel;

                if (allProjectsVM != null)
                {
                    try
                    {
                        ProjectDB projectDB = new ProjectDB();
                        projectDB.DeleteProject(allProjectsVM.SelectedProject);

                        AllProjectsViewModel allProjectsViewModel = new AllProjectsViewModel(allProjectsVM.Employee)
                        {
                            Parent = allProjectsVM.Parent
                        };

                        allProjectsVM.Parent.CurrentView = allProjectsViewModel;
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show(ex.Message);
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
