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
    public class AddRoleCommand : ICommand
    {
        public AddRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is RoleListPageViewModel)
            {
                RoleListPageViewModel roleListPageVM = parameter as RoleListPageViewModel;

                if (roleListPageVM == null || roleListPageVM.NewRole == null || roleListPageVM.NewRole.ToString() == string.Empty)
                {
                    ret = false;
                    MessageBox.Show("Неверное имя роли");
                }
                else 
                {
                    RoleDB roleDB = new RoleDB();

                    if (roleDB.SelectMethod<Role>().Exists(x => x.Name == roleListPageVM.NewRole.ToString()))
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
            if (parameter is RoleListPageViewModel)
            {
                RoleListPageViewModel loginVM = parameter as RoleListPageViewModel;

                if (loginVM != null && loginVM.NewRole.ToString() != string.Empty)
                {
                    Role role = new Role(loginVM.NewRole.ToString());

                    RoleDB roleDB = new RoleDB();
                    roleDB.InsertMethod(role);

                    loginVM.SetRoleList(loginVM.initRoleList());

                }              
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
