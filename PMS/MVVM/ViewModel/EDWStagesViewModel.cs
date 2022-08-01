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
using System.Windows.Media;

namespace PMS.MVVM.ViewModel
{
    public class EDWStagesViewModel : ObservableObject, IStageViewModel
    {
        private List<System.Drawing.Color> colors;

        public SetStageLinesCommand FirstEDWStageCommand { get; private set; }
        public SetStageLinesCommand SecondEDWStageCommand { get; private set; }
        public SetStageLinesCommand ThirdEDWStageCommand { get; private set; }
        public SetStageLinesCommand FourthEDWStageCommand { get; private set; }
        public SetStageLinesCommand FifthEDWStageCommand { get; private set; }
        public SetStageLinesCommand SixthEDWStageCommand { get; private set; }

        public MainViewModel ParentMain {get; set; }
        public ProjectDetailsViewModel Parent {get; set; }
        public Section SectionEDW {get; set; }
        public Stage Stage {get; set; }
        public string EDWSectionCipher {get; set; }

        public object FirstBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(0);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                
                OnPropertyChanged();
            }
        }

        public object SecondBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(1);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                
                OnPropertyChanged();
            }
        }

        public object ThirdBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(2);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public object FourthBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(3);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public object FifthBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(4);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public object SixthBackground
        {
            get
            {
                System.Drawing.Color color = colors.ElementAt(5);

                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            set
            {
                OnPropertyChanged();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
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

        private List<Stage> _stagesEDWList;
        public List<Stage> StagesEDWList
        {
            get { return _stagesEDWList; }
            set
            {
                _stagesEDWList = value;
                OnPropertyChanged();
            }
        }

        public EDWStagesViewModel(Section section)
        {
            SectionEDW = section;
            StagesEDWList = SectionEDW.GetStages();
            EDWSectionCipher = SectionEDW.Cipher;

            FirstEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.PD));
            SecondEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.TP));
            ThirdEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.DWDD));
            FourthEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.MPaCPT));
            FifthEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.CST));
            SixthEDWStageCommand = new SetStageLinesCommand(GetStage((int)EDWStagesEnum.AWDD));

            colors = SectionEDW.GetSectionColorList();
        }

        private Stage GetStage(int value)
        {
            StageDB stageDB = new StageDB();
            Stage stage = stageDB.SelectMethod<Stage>()
                .First(s => s.ProjectID == SectionEDW.ProjectID && s.SectionNum == SectionEDW.SectionNum && s.StageNum == value);
            return stage;
        }


        public ProjectDetailsViewModel GetProjectDetailsViewModel()
        {
            return Parent;
        }
    }
}
