using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PMS.MVVM.ViewModel
{
    public class MemberListPageViewModel: ObservableObject
    {
        private BindingList<Member> memberList;

        public ICommand AddMemberCommand { get; private set; }
        public ICommand DeleteMemberCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }

        public ProjectDetailsViewModel Parent{ get; set; }

        public Project Project { get; set; }

        private object _memberList;
        public object MemberList
        {
            get { return _memberList; }
            set
            {
                _memberList = value;
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

        private object _selectedMember;
        public object SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                OnPropertyChanged();
            }
        }

        private object _newMember;
        public object NewMember
        {
            get { return _newMember; }
            set
            {
                _newMember = value;
                OnPropertyChanged();
            }
        }

        public MemberListPageViewModel(Project project)
        {
            Project = project;
            memberList = InitMemberList();
            MemberList = memberList;

            AddMemberCommand = new AddMemberCommand(Project);
            DeleteMemberCommand = new DeleteMemberCommand();
            BackCommand = new RelayCommand(parameter =>
            {
                ProjectDetailsViewModel projectDetailsVM = new ProjectDetailsViewModel(Project, Parent.Parent, Parent.ParentMain, this.Parent.IsPermissible);
                Parent.ParentMain.CurrentView = projectDetailsVM;
            });


        }
        private void OnFilterChanged()
        {
            List<Member> members = memberList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            MemberList = new BindingList<Member>(members);
        }

        public void SetMemberList(BindingList<Member> members)
        {
            memberList = members;
            MemberList = memberList;
        }

        public BindingList<Member> InitMemberList()
        {
            return new BindingList<Member>(new MemberDB().SelectMethod<Member>().Where(x => x.ProjectID == Project.ProjectID).ToList());
        }
    }
}
