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
    public class EditDocumentCommand : ICommand
    {
        public EditDocumentCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {            
            bool ret = true;
            if (parameter is DocumentListPageViewModel)
            {
                DocumentListPageViewModel documentListPageVM = parameter as DocumentListPageViewModel;

                if (documentListPageVM == null || documentListPageVM.ActiveDocument == null )
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }                
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is DocumentListPageViewModel)
            {
                DocumentListPageViewModel documentVM = parameter as DocumentListPageViewModel;

                if (documentVM != null)
                {

                    DocumentDetailsViewModel documentDetailsVM = new DocumentDetailsViewModel(documentVM.ActiveDocument as Document)
                    {
                        Parent = documentVM,
                        ParentMain = documentVM.mainViewModel,
                    };

                    documentVM.mainViewModel.CurrentView = documentDetailsVM;
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
