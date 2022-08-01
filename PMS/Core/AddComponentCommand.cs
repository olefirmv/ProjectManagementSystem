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
    public class AddComponentCommand : ICommand
    {

        public AddComponentCommand()
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
                    ret = false;
                    if ((createProjectVM.Name == null && createProjectVM.Name == string.Empty) || (createProjectVM.StartDate > createProjectVM.EndDate))
                    {
                        MessageBox.Show("Некорректные введенные данные");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }

            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is ComponentInfoViewModel)
            {
                ComponentInfoViewModel componentInfoVM = parameter as ComponentInfoViewModel;

                if (componentInfoVM != null)
                {
                    BindingList<BL.Model.Component> components = componentInfoVM.ComponentList as BindingList<BL.Model.Component>;

                    ComponentDB componentDB = new ComponentDB();
                    List<BL.Model.Component> list = componentDB.SelectMethod<BL.Model.Component>()
                        .Where(c => c.ProjectID == componentInfoVM.Project.ProjectID && c.SectionNum == (int)componentInfoVM.SelectedSection).ToList();
                    int maxComponentID;
                    if (list.Count == 0)
                    {
                        maxComponentID = 1;
                    }
                    else
                    {
                        maxComponentID = list.Last().ComponentID +1;
                    } 
                        

                    BL.Model.Component component = new BL.Model.Component();
                    component.ProjectID = componentInfoVM.Project.ProjectID;
                    component.SectionNum = (int) componentInfoVM.SelectedSection;
                    component.ComponentID = maxComponentID;
                    component.Name = componentInfoVM.NewName.ToString();
                    component.Cipher = componentInfoVM.NewCipher.ToString();

                    componentDB.InsertMethod(component);

                    components.Add(component);
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
