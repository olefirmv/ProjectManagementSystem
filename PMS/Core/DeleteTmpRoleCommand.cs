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
    public class DeleteTmpRoleCommand : ICommand
    {
        public DeleteTmpRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is TmpRoleSelectionViewModel)
            {
                TmpRoleSelectionViewModel tmpRoleSelectionVM = parameter as TmpRoleSelectionViewModel;

                if (tmpRoleSelectionVM == null || tmpRoleSelectionVM.SelectedItem == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is TmpRoleSelectionViewModel)
            {
                TmpRoleSelectionViewModel tmpRoleSelectionVM = parameter as TmpRoleSelectionViewModel;

                if (tmpRoleSelectionVM == null)
                {
                    MessageBox.Show("Ошибка");
                }
                else
                {
                    BindingList<Role> roles = tmpRoleSelectionVM.roleList;

                    if (roles.Remove(tmpRoleSelectionVM.SelectedItem as Role))
                    {
                        tmpRoleSelectionVM.SetRoleList(roles);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
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
