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
    public class UpdateTmpRoleCommand : ICommand
    {
        public UpdateTmpRoleCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is TmpRoleSelectionViewModel)
            {
                TmpRoleSelectionViewModel tmpRoleSelectionVM = parameter as TmpRoleSelectionViewModel;

                if (tmpRoleSelectionVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
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
                    DocumentDetailsViewModel documentDetailsViewModel = tmpRoleSelectionVM.Parent;

                    switch (tmpRoleSelectionVM.roleType)
                    {
                        case DocumentRoleType.Develops:
                            documentDetailsViewModel.Develops = tmpRoleSelectionVM.GetRoleContainer();
                            break;
                        case DocumentRoleType.Agrees:
                            documentDetailsViewModel.Agrees = tmpRoleSelectionVM.GetRoleContainer();
                            break;
                        case DocumentRoleType.Claims:
                            documentDetailsViewModel.Claims = tmpRoleSelectionVM.GetRoleContainer();
                            break;
                    }
                    
                    documentDetailsViewModel.HelperView = null;
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
