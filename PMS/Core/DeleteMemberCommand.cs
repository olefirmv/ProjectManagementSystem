using PMS.BL.Core;
using PMS.BL.Model;
using PMS.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMS.Core
{
    public class DeleteMemberCommand : ICommand
    {
        
        public DeleteMemberCommand()
        {
            
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is MemberListPageViewModel)
            {
                MemberListPageViewModel memberListPageVM = parameter as MemberListPageViewModel;

                if (memberListPageVM.SelectedMember != null)
                {
                    BindingList<Member> collection = memberListPageVM.MemberList as BindingList<Member>;

                    Member member = memberListPageVM.SelectedMember as Member;
                    MemberDB memberDB = new MemberDB();
                    memberDB.DeleteRecord<Member>(member);
                    collection.Remove(member);

                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
