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
    public class SetStageLinesCommand : ICommand
    {
        public Stage Stage {get; private set;}
        
        public SetStageLinesCommand(Stage stage)
        {
            Stage = stage;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is SRWStagesViewModel)
            {
                SRWStagesViewModel srwStagesVM = parameter as SRWStagesViewModel;

                if (srwStagesVM != null)
                {
                    StageLinesViewModel stageLinesVM = new StageLinesViewModel(srwStagesVM.SectionSRW, Stage, srwStagesVM, srwStagesVM.Parent.Employee, srwStagesVM.Parent.IsPermissible, srwStagesVM.Parent.privelegeByUser)
                    {
                        ParentMain = srwStagesVM.ParentMain,
                        Parent = srwStagesVM
                    };
                    srwStagesVM.CurrentView = stageLinesVM;
                    srwStagesVM.HelperView = null;
                }
            }

            if (parameter is EDWStagesViewModel)
            {
                EDWStagesViewModel edwStagesVM = parameter as EDWStagesViewModel;

                if (edwStagesVM != null)
                {
                    StageLinesViewModel stageLinesVM = new StageLinesViewModel(edwStagesVM.SectionEDW, Stage, edwStagesVM, edwStagesVM.Parent.Employee, edwStagesVM.Parent.IsPermissible, edwStagesVM.Parent.privelegeByUser)
                    {
                        ParentMain = edwStagesVM.ParentMain
                    };
                    edwStagesVM.CurrentView = stageLinesVM;
                    edwStagesVM.HelperView = null;
                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}