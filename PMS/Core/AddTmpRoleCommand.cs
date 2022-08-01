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
    public class AddTmpRoleCommand : ICommand
    {
        public AddTmpRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is TmpRoleSelectionViewModel)
            {
                TmpRoleSelectionViewModel tmpRoleSelectionVM = parameter as TmpRoleSelectionViewModel;

                if (tmpRoleSelectionVM == null || tmpRoleSelectionVM.RoleID == null || (int)tmpRoleSelectionVM.RoleID == 0)
                {
                    ret = false;
                    MessageBox.Show("Неверное имя роли");
                }
                else
                {
                    if(tmpRoleSelectionVM.roleList.Any(x => x.RoleID == (int)tmpRoleSelectionVM.RoleID))
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
            if (parameter is TmpRoleSelectionViewModel)
            {
                TmpRoleSelectionViewModel tmpRoleSelectionVM = parameter as TmpRoleSelectionViewModel;

                if (tmpRoleSelectionVM == null)
                {
                    MessageBox.Show("Ошибка");
                }
                else
                {
                    RoleDB roleDB = new RoleDB();
                    Role role = roleDB.SelectMethod<Role>().First(x => x.RoleID == (int)tmpRoleSelectionVM.RoleID);

                    tmpRoleSelectionVM.roleList.Add(role);
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
