using PMS.BL.Core;
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
    public class EditClaimsCommand : ICommand
    {
        public EditClaimsCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is DocumentDetailsViewModel)
            {
                DocumentDetailsViewModel documentDetailsVM = parameter as DocumentDetailsViewModel;

                if (documentDetailsVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is DocumentDetailsViewModel)
            {
                DocumentDetailsViewModel documentDetailsVM = parameter as DocumentDetailsViewModel;

                if (documentDetailsVM == null)
                {
                    MessageBox.Show("Ошибка");
                }

                TmpRoleSelectionViewModel tmpRoleSelectionViewModel = new TmpRoleSelectionViewModel(documentDetailsVM.document.Claims, DocumentRoleType.Claims)
                {
                    ParentMain = documentDetailsVM.ParentMain,
                    Parent = documentDetailsVM
                };

                documentDetailsVM.HelperView = tmpRoleSelectionViewModel;
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
