using PMS.Bl.Extensions;
using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.MVVM.ViewModel
{
    public class ComponentProjectRoleViewModel : ObservableObject
    {
        public AddTmpComponentProjectRoleCommand AddTmpComponentProjectRoleCommand {get; private set;}
        public DeleteTmpComponentProjectRoleCommand DeleteTmpComponentProjectRoleCommand {get; private set;}
        public SaveComponentProjectRoleCommand SaveComponentProjectRoleCommand {get; private set;}

        public ComponentInfoViewModel Parent { get; set;}
        public Project Project { get; set;}
        public BL.Model.Component Component {get; set;}


        private object _tmpComponentProjectRoles;
        public object TmpComponentProjectRoles
        {
            get
            {
                return _tmpComponentProjectRoles;
            }
            set
            {
                _tmpComponentProjectRoles = value;
                OnPropertyChanged();
            }
        }

        private object _tmpComponentProjectRole;
        public object SelectedTmpComponentProjectRole
        {
            get
            {
                return _tmpComponentProjectRole;
            }
            set
            {
                _tmpComponentProjectRole = value;
                OnItemChanged();
                OnPropertyChanged();
            }
        }

        private object _selectedMember;
        public object SelectedMember
        {
            get
            {
                return _selectedMember;
            }
            set
            {
                _selectedMember = value;
                OnPropertyChanged();
            }
        }

        private object _memberList;
        public object MemberList
        {
            get
            {
                return _memberList;
            }
            set
            {
                _memberList = value;
                OnPropertyChanged();
            }
        }

        private object _selectedRole;
        public object SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        private object _roleList;
        public object RoleList
        {
            get
            {
                return _roleList;
            }
            set
            {
                _roleList = value;
                OnPropertyChanged();
            }
        }

        public ComponentProjectRoleViewModel(ComponentInfoViewModel parent)
        {
            Parent = parent;
            Project = parent.Project;
            Component = parent.SelectedComponent as BL.Model.Component;

            MemberList = InitMemberList();
            RoleList = InitRoleList();
            TmpComponentProjectRoles = InitTmpComponentProjectRoles();

            AddTmpComponentProjectRoleCommand = new AddTmpComponentProjectRoleCommand();
            DeleteTmpComponentProjectRoleCommand = new DeleteTmpComponentProjectRoleCommand();
            SaveComponentProjectRoleCommand = new SaveComponentProjectRoleCommand();

        }

        private void OnItemChanged()
        {
            if (SelectedTmpComponentProjectRole != null)
            {
                SelectedMember = (SelectedTmpComponentProjectRole as TmpProjectRole).MemberID;
                SelectedRole = (SelectedTmpComponentProjectRole as TmpProjectRole).RoleID;
            }
        }

        private BindingList<TmpProjectRole> InitTmpComponentProjectRoles()
        {
            BindingList<TmpProjectRole> list = new BindingList<TmpProjectRole>();
            List<ProjectRole> projectRoles = new List<ProjectRole>();
            ProjectRoleDB projectRoleDB = new ProjectRoleDB();

            projectRoles = projectRoleDB.SelectMethod<ProjectRole>().
                Where(pR => pR.ProjectID == Component.ProjectID && pR.SectionNum == Component.SectionNum && pR.ComponentID == Component.ComponentID).ToList();

            MemberDB memberDB = new MemberDB();
            RoleDB roleDB = new RoleDB();
            foreach (ProjectRole pR in projectRoles)
            {
                TmpProjectRole objectClass = new TmpProjectRole()
                {
                    MemberID = pR.MemberID,
                    RoleID = pR.RoleID,
                    SectionNum = pR.SectionNum
                };
                list.Add(objectClass);
            }

            return list;
        }


        private List<ObjectClass> InitMemberList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            MemberDB memberDB = new MemberDB();
            List<Member> memberList = memberDB.SelectMethod<Member>()
                .Where(pE => pE.ProjectID == Project.ProjectID).ToList();

            foreach (Member m in memberList)
            {

                ObjectClass objectClass = new ObjectClass()
                {
                    Value = m.MemberID,
                    Name = m.Name
                };
                list.Add(objectClass);
            }

            return list;
        }


        private List<ObjectClass> InitRoleList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            RoleDB roleDB = new RoleDB();
            List<Role> roleList = roleDB.SelectMethod<Role>().ToList();

            foreach (Role r in roleList)
            {

                ObjectClass objectClass = new ObjectClass()
                {
                    Value = r.RoleID,
                    Name = r.Name
                };
                list.Add(objectClass);
            }

            return list;
        }
    }
}
