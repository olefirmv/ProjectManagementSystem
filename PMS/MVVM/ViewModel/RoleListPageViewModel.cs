using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS.MVVM.ViewModel
{
    public class RoleListPageViewModel : ObservableObject
    {
        private BindingList<Role> roleList;

        public ICommand AddRoleCommand { get; private set;}
        public RelayCommand BackCommand { get; private set;}
        public AllProjectsViewModel Parent { get; set;}

        private object _roleList;
        public object RoleList
        {
            get { return _roleList; }
            set
            {
                _roleList = value;
                OnPropertyChanged();
            }
        }
        private object _filter;
        public object Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnFilterChanged();
                OnPropertyChanged();
            }
        }

        private object _newRole;
        public object NewRole
        {
            get { return _newRole; }
            set
            {
                _newRole = value;
                OnPropertyChanged();
            }
        }

        public RoleListPageViewModel()
        {
            roleList = initRoleList();
            RoleList = roleList;

            AddRoleCommand = new AddRoleCommand();
            BackCommand = new RelayCommand(parameter =>
            {
                
                AllProjectsViewModel allProjectsVM = new AllProjectsViewModel(Parent.Employee)
                {
                    Parent = Parent.Parent
                };
                Parent.Parent.CurrentView = allProjectsVM;
            });
        }

        private void OnFilterChanged()
        {
            List<Role> roles = roleList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            RoleList = new BindingList<Role>(roles);
        }

        public void SetRoleList(BindingList<Role> roles)
        {
            roleList = roles;
            RoleList = roleList;
        }

        public BindingList<Role> initRoleList()
        {
            return new BindingList<Role>(new RoleDB().SelectMethod<Role>());
        }


    }
}
