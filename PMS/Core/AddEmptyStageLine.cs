using PMS.BL.Core;
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
    public class AddEmptyStageLine: ICommand
    {
        public AddEmptyStageLine()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is StageLinesViewModel)
            {
                StageLinesViewModel stageLinesVM = parameter as StageLinesViewModel;

                if (stageLinesVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is StageLinesViewModel)
            {
                StageLinesViewModel stageLinesVM = parameter as StageLinesViewModel;

                if (stageLinesVM != null)
                {
                    stageLinesVM.TmpStageLineList.Add(stageLinesVM.InitEmptyTmpStageLine(stageLinesVM.TmpStageLineList.Count + 1));
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
