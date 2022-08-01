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
    public class CreateProjectAndEnableSectionAndStageCommand: ICommand
    {
        public CreateProjectAndEnableSectionAndStageCommand()
        {

        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is CreateProjectViewModel)
            {
                CreateProjectViewModel createProjectVM = parameter as CreateProjectViewModel;

                if (createProjectVM != null)
                {
                    
                    if ((createProjectVM.Name == null && createProjectVM.Name == string.Empty) || (createProjectVM.StartDate > createProjectVM.EndDate))
                    {
                        ret = false;
                        MessageBox.Show("Некорректные введенные данные");
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
            if (parameter is CreateProjectViewModel)
            {
                CreateProjectViewModel createProjectVM = parameter as CreateProjectViewModel;

                if (createProjectVM != null)
                {
                    try
                    {
                        Project project = createProjectVM.Project;
                        project.ModifyDateTime = DateTime.Now;

                        try
                        {
                            ProjectDB projectDB = new ProjectDB();
                            projectDB.InsertMethod(project);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Не удалось добавить проект");
                        }

                        try
                        {
                            ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();

                            PrivelegeDB privelegeDB = new PrivelegeDB();
                            Privelege privelege = privelegeDB.FindOrCreate(new Privelege(true, true, true, true));

                            ProjectEmployee projectEmployee = new ProjectEmployee(project.ProjectID, createProjectVM.ParentMain.Employee.EmployeeID, privelege.PrivelegeID);
                            projectEmployeeDB.InsertMethod(projectEmployee);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Не удалось задать полные права на проект");
                        }
                        createProjectVM.OnChecked = true;
                        createProjectVM.UnChecked = !createProjectVM.OnChecked;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
