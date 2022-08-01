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
    public class DeleteComponentCommand : ICommand
    {
        public DeleteComponentCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is ComponentInfoViewModel)
            {
                ComponentInfoViewModel componentInfoVM = parameter as ComponentInfoViewModel;

                if (componentInfoVM == null)
                {
                    ret = false;
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
                    BL.Model.Component component = componentInfoVM.SelectedComponent as BL.Model.Component;
                    ComponentDB componentDB = new ComponentDB();
                    componentDB.DeleteRecord(component);

                    components.Remove(component);
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
