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
    public class ProjectInfoViewModel : ObservableObject
    {
        private BindingList<ProjectStatusEnum> ProjectStatuses { get; set; }
        
        public SaveProjectInfoCommand SaveProjectInfoCommand { get; private set; }
        public DeleteTmpProjectRoleCommand DeleteTmpProjectRoleCommand { get; private set; }
        public AddTmpProjectRoleCommand AddTmpProjectRoleCommand { get; private set; }
        public EditTmpProjectRoleCommand EditTmpProjectRoleCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }

        public ProjectDetailsViewModel Parent { get; set;}
        public Project Project { get; set; }

        public object Name
        {
            get
            {
                return Project.Name;
            }
            set
            {
                Project.Name = value.ToString();
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime StartDate
        {
            get
            {
                return Project.StartDate;
            }
            set
            {
                Project.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get
            {
                return Project.EndDate;
            }
            set
            {
                Project.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public object Status
        {
            get
            {
                return Project.Status.ToString();
            }
            set
            {
                Project.Status = (int) value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private object _tmpProjectRoles;
        public object TmpProjectRoles
        {
            get
            {
                return _tmpProjectRoles;
            }
            set
            {
                _tmpProjectRoles = value;
                OnPropertyChanged();
            }
        } 

        private object _tmpProjectRole;
        public object SelectedTmpProjectRole
        {
            get
            {
                return _tmpProjectRole;
            }
            set
            {
                _tmpProjectRole = value;
                OnItemChanged();
                OnPropertyChanged();
            }
        }

        private object _projectStatusesList;
        public object ProjectStatusesListPage
        {
            get { return _projectStatusesList; }
            set
            {
                _projectStatusesList = value;
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

        private object _selectedSection;
        public object SelectedSection
        {
            get
            {
                return _selectedSection;
            }
            set
            {
                _selectedSection = value;
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
        
        
        private object _sectionList;
        public object SectionList
        {
            get
            {
                return _sectionList;
            }
            set
            {
                _sectionList = value;
                OnPropertyChanged();
            }
        }

        public ProjectInfoViewModel(ProjectDetailsViewModel parent)
        {
            Parent = parent;
            Project = parent.Project;
            Name = Project.Name;
            StartDate = Project.StartDate;
            EndDate = Project.EndDate;
            Status = Project.Status;

            ProjectStatusesListPage = InitStatusList();
            MemberList = InitMemberList();
            RoleList = InitRoleList();
            SectionList = InitSectionList();
            TmpProjectRoles = InitTmpProjectRoles();

            DeleteTmpProjectRoleCommand = new DeleteTmpProjectRoleCommand();
            AddTmpProjectRoleCommand = new AddTmpProjectRoleCommand();
            SaveProjectInfoCommand = new SaveProjectInfoCommand();
            EditTmpProjectRoleCommand = new EditTmpProjectRoleCommand();

            BackCommand = new RelayCommand(parameter =>
            {
                ProjectDetailsViewModel projectDetailsVM = new ProjectDetailsViewModel(Project, this.Parent.Parent, this.Parent.Parent.Parent, this.Parent.IsPermissible);

                this.Parent.Parent.Parent.CurrentView = projectDetailsVM;
            });

        }

        private void OnItemChanged()
        {
            if (SelectedTmpProjectRole != null)
            {
                SelectedMember = (SelectedTmpProjectRole as TmpProjectRole).MemberID;
                SelectedRole = (SelectedTmpProjectRole as TmpProjectRole).RoleID;
                SelectedSection = (SelectedTmpProjectRole as TmpProjectRole).SectionNum;
            }
        }

        private List<ObjectClass> InitStatusList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            foreach (var stageLineStatusNum in Enum.GetValues(typeof(StageLineStatusEnum)))
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = (int)stageLineStatusNum,
                    Name = PMSBLHelper.GetDescriptionByEnumType((StageLineStatusEnum)stageLineStatusNum)
                };

                list.Add(objectClass);
            }

            return list;
        }

        private List<ObjectClass> InitSectionList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            foreach (var sectionNum in Enum.GetValues(typeof(SectionEnum)))
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = (int)sectionNum,
                    Name = PMSBLHelper.GetDescriptionByEnumType((SectionEnum)sectionNum)
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

        private BindingList<TmpProjectRole> InitTmpProjectRoles()
        {
            BindingList<TmpProjectRole> list = new BindingList<TmpProjectRole>();
            List<ProjectRole> projectRoles = new List<ProjectRole>();
            ProjectRoleDB projectRoleDB = new ProjectRoleDB();

            projectRoles = projectRoleDB.SelectMethod<ProjectRole>().
                Where(pR => pR.ProjectID == Project.ProjectID && pR.ComponentID == 0).ToList();

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
    }
}
