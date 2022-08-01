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
    public class SaveStageLineCommand: ICommand
    {
        public SaveStageLineCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is StageLineInfoViewModel)
            {
                StageLineInfoViewModel stageLineInfoVM = parameter as StageLineInfoViewModel;

                if (stageLineInfoVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;
        }

        public void Execute(object parameter)
        {
            if (parameter is StageLineInfoViewModel)
            {
                StageLineInfoViewModel stageLineInfoVM = parameter as StageLineInfoViewModel;

                if (stageLineInfoVM != null)
                {
                    StageLine stageLine = new StageLine();

                    stageLine.Name = stageLineInfoVM.tmpStageLine.Name;
                    stageLine.DocumentID = stageLineInfoVM.tmpStageLine.DocumentID;
                    stageLine.ProjectID = stageLineInfoVM.tmpStageLine.ProjectID;
                    stageLine.SectionNum = stageLineInfoVM.tmpStageLine.SectionNum;
                    stageLine.StageNum = stageLineInfoVM.tmpStageLine.StageNum;
                    stageLine.StageLineID = stageLineInfoVM.tmpStageLine.Counter;
                    stageLine.EmployeeID = stageLineInfoVM.tmpStageLine.EmployeeID;
                    stageLine.Status = stageLineInfoVM.tmpStageLine.StatusStringToInt(stageLineInfoVM.tmpStageLine.Status);
                    stageLine.ModifyDateTime = DateTime.Now;
                    stageLine.StartDate = stageLineInfoVM.tmpStageLine.StartDate;
                    stageLine.EndDate = stageLineInfoVM.tmpStageLine.EndDate;

                    stageLine.ComponentID = stageLineInfoVM.tmpStageLine.ComponentID;

                    StageLineDB stageLineDB = new StageLineDB();
                    var tmp = stageLineDB.SelectMethod<StageLine>()
                        .Where(sL => sL.ProjectID == stageLine.ProjectID && sL.SectionNum == stageLine.SectionNum && sL.StageNum == stageLine.StageNum)
                        .FirstOrDefault(sL => sL.StageLineID == stageLine.StageLineID);

                    if (tmp != null)
                    {
                        
                        stageLineDB.UpdateRecord<StageLine>(stageLine);
                    }
                    else
                    {
                        stageLineDB.InsertMethod<StageLine>(stageLine);
                    }

                    stageLineInfoVM.parent.SetStageLineList();
                    stageLineInfoVM.parent.HelperView = null;

                }

            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}

