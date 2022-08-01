using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS.MVVM.ViewModel
{
    public class StageLinesViewModel: ObservableObject 
    {
        private BindingList<StageLine> stageLineList;

        public AddEmptyStageLine AddEmptyStageLine { get; private set;}
        
        public MainViewModel ParentMain { get; set; }
        public IStageViewModel Parent { get; set; }
        public Section Section { get; private set; }
        public Stage Stage { get; private set; }
        public Employee Employee { get; private set; }
        public Privelege PrivelegeByUser { get; private set; }
        public bool IsPermissible { get; private set; }
        

        private object _activeTmpStageLine;
        public object ActiveTmpStageLine
        {
            get
            {
                return _activeTmpStageLine;
            }
            set
            {
                _activeTmpStageLine = value;
                OnItemChanged();
                OnPropertyChanged();
            }
        }


        private object _helperView;
        public object HelperView
        {
            get
            {
                return _helperView;
            }
            set
            {
                _helperView = value;
                OnPropertyChanged();
            }
        }
        
        private BindingList<TmpStageLine> _tmpStageLineList;
        public BindingList<TmpStageLine> TmpStageLineList
        {
            get { return _tmpStageLineList; }
            set
            {
                _tmpStageLineList = value;
                OnPropertyChanged();
            }
        }

        public StageLinesViewModel(Section section, Stage stage, IStageViewModel parent, Employee employee, bool isPermissible, Privelege privelegeByUser)
        {
            
            this.Section = section;
            this.Stage = stage;
            Employee = employee;
            PrivelegeByUser = privelegeByUser;
            IsPermissible = isPermissible;
            SetStageLineList();
            AddEmptyStageLine = new AddEmptyStageLine();
        }
        private void OnItemChanged()
        {
            if (ActiveTmpStageLine != null)
            {
                StageLineInfoViewModel stageLineInfoVM = new StageLineInfoViewModel
                    (Parent, ActiveTmpStageLine as TmpStageLine, PrivelegeByUser.EditFull || PrivelegeByUser.EditOne && (ActiveTmpStageLine as TmpStageLine).EmployeeID == Employee.EmployeeID)
                {
                    mainViewModel = ParentMain,
                    parent = this
                };

                HelperView = stageLineInfoVM;
            }
        }

        public TmpStageLine InitEmptyTmpStageLine(int counter)
        {
            TmpStageLine tmpStageLine = new TmpStageLine();
            tmpStageLine.ProjectID = Section.ProjectID;
            tmpStageLine.SectionNum = Section.SectionNum;
            tmpStageLine.StageNum = Stage.StageNum;
            tmpStageLine.DocumentName = "";
            tmpStageLine.Counter = counter;

            return tmpStageLine;
        }

        public void SetStageLineList()
        {
            StageLineDB stageLineDB = new StageLineDB();


            if (PrivelegeByUser.ReadFull)
            {
                stageLineList = new BindingList<StageLine>(stageLineDB.SelectMethod<StageLine>()
                .Where(sl => sl.ProjectID == Stage.ProjectID && sl.SectionNum == Stage.SectionNum && sl.StageNum == Stage.StageNum)
                .ToList());
            }
            else
            {
                stageLineList = new BindingList<StageLine>(stageLineDB.SelectMethod<StageLine>()
                .Where(sl => sl.ProjectID == Stage.ProjectID && sl.SectionNum == Stage.SectionNum && sl.StageNum == Stage.StageNum && sl.EmployeeID == Employee.EmployeeID)
                .ToList());
            }

            if (stageLineList.Count == 0)
            {
                TmpStageLineList = new BindingList<TmpStageLine>();
                TmpStageLineList.Add(InitEmptyTmpStageLine(1));
            }
            else
            {
                TmpStageLineList = new BindingList<TmpStageLine>();
                for (int i = 0; i < stageLineList.Count; i++)
                {
                    TmpStageLineList.Add(new TmpStageLine(stageLineList[i]));
                }
            }
        }
    }
}
