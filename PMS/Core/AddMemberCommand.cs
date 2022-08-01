using PMS.BL.Core;
using PMS.BL.Model;
using PMS.MVVM.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace PMS.Core
{
    public class AddMemberCommand : ICommand
    {
        
        public Project Project { get; private set; }
        public AddMemberCommand(Project project)
        {
            Project = project;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is MemberListPageViewModel)
            {
                MemberListPageViewModel memberListPageVM = parameter as MemberListPageViewModel;

                if (memberListPageVM == null || memberListPageVM.NewMember == null || memberListPageVM.NewMember.ToString() == string.Empty)
                {
                    ret = false;
                    MessageBox.Show("Неверное наименование участника");
                }
                else
                {
                    MemberDB memberDB = new MemberDB();

                    if (memberDB.SelectMethod<Member>().Exists(x => x.Name == memberListPageVM.NewMember.ToString() && x.ProjectID == Project.ProjectID))
                    {
                        ret = false;
                        MessageBox.Show("Такой участник уже существует");
                    }
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is MemberListPageViewModel)
            {
                MemberListPageViewModel memberListPageVM = parameter as MemberListPageViewModel;

                if (memberListPageVM != null && memberListPageVM.NewMember.ToString() != string.Empty)
                {
                    Member member = new Member(memberListPageVM.NewMember.ToString(), Project.ProjectID);

                    MemberDB memberDB = new MemberDB();
                    memberDB.InsertMethod(member);

                    memberListPageVM.SetMemberList(memberListPageVM.InitMemberList());

                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
