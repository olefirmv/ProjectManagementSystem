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
    public class AddFormCommand : ICommand
    {
        public AddFormCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is FormListPageViewModel)
            {
                FormListPageViewModel formListVM = parameter as FormListPageViewModel;

                if (formListVM == null || formListVM.NewForm == null || formListVM.NewForm.ToString() == string.Empty)
                {
                    ret = false;
                    MessageBox.Show("Неверное имя роли");
                }
                else
                {
                    FormDB formDB = new FormDB();

                    if (formDB.SelectMethod<Form>().Exists(x => x.Name == formListVM.NewForm.ToString()))
                    {
                        ret = false;
                        MessageBox.Show("Такая роль уже существует");
                    }
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is FormListPageViewModel)
            {
                FormListPageViewModel formListVM = parameter as FormListPageViewModel;

                if (formListVM != null && formListVM.NewForm.ToString() != string.Empty)
                {
                    Form form = new Form(formListVM.NewForm.ToString());

                    FormDB formDB = new FormDB();
                    formDB.InsertMethod(form);

                    formListVM.SetFormList(formListVM.InitFormList());

                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
