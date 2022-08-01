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
    public class TmpRoleSelectionViewModel : ObservableObject
    {
        public ICommand AddTmpRoleCommand { get; private set;}
        public ICommand UpdateTmpRoleCommand { get; private set;}
        public ICommand CancelTmpRoleCommand { get; private set;}
        public ICommand DeleteTmpRoleCommand { get; private set;}

        public DocumentDetailsViewModel Parent { get; set;}
        public MainViewModel ParentMain{ get; set;}

        public BindingList<Role> roleList;
        public DocumentRoleType roleType;

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

        private object _roleAddList;
        public object RoleAddList
        {
            get { return _roleAddList; }
            set
            {
                _roleAddList = value;
                OnPropertyChanged();
            }
        }

        private object _roleID;
        public object RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
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
        
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
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

        public TmpRoleSelectionViewModel(string roleContainer, DocumentRoleType documentRoleType)
        {
            RoleAddList = InitRoleAddList();
            roleList = InitRoleList(roleContainer);
            RoleList = roleList;

            AddTmpRoleCommand       = new AddTmpRoleCommand();
            UpdateTmpRoleCommand    = new UpdateTmpRoleCommand();
            CancelTmpRoleCommand    = new CancelTmpRoleCommand();
            DeleteTmpRoleCommand    = new DeleteTmpRoleCommand();

            roleType = documentRoleType;
        }

        private void OnFilterChanged()
        {
            List<Role> roles = roleList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            RoleList = new BindingList<Role>(roles);
        }

        private List<ObjectClass> InitRoleAddList()
        {
            RoleDB roleDB = new RoleDB();

            List<ObjectClass> list = new List<ObjectClass>();
            List<Role> roles = roleDB.SelectMethod<Role>();

            foreach (Role role in roles)
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Name = role.Name,
                    Value = role.RoleID
                };

                list.Add(objectClass);
            }

            return list;
        }
        private BindingList<Role> InitRoleList(string roleContainer)
        {
            List<Role> roleList;

            if (roleContainer != null && roleContainer != string.Empty)
            {
                List<string> roles = roleContainer.Split(';').ToList();

                roleList = new RoleDB().SelectMethod<Role>().Where(x => roles.Contains(x.RoleID.ToString())).ToList();
            }
            else
            {
                roleList = new List<Role>();
            }

            return new BindingList<Role>(roleList);
        }

        public void SetRoleList(BindingList<Role> roles)
        {
            roleList = roles;
            RoleList = roleList;
        }

        public string GetRoleContainer()
        {
            string roleContainer = "";

            if (roleList != null && roleList.Count > 0)
            {
                foreach (Role role in roleList)
                {
                    roleContainer += role.RoleID.ToString() + ";";
                }

                roleContainer = roleContainer.Remove(roleContainer.Length - 1);
            }
            return roleContainer;
        }
    }
}
