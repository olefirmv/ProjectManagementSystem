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
using System.Drawing;
using System.Windows.Media;

namespace PMS.MVVM.ViewModel
{
    public class SRWStagesViewModel : ObservableObject, IStageViewModel
    {
        private List<System.Drawing.Color> colors;
        
        public SetStageLinesCommand FirstSRWStageCommand { get; private set;}
        public SetStageLinesCommand SecondSRWStageCommand { get; private set;}
        public SetStageLinesCommand ThirdSRWStageCommand { get; private set;}
        public SetStageLinesCommand FourthSRWStageCommand { get; private set;}

        public MainViewModel ParentMain { get; set; }
        public ProjectDetailsViewModel Parent { get; set; }
        public Section SectionSRW { get; set; }
        public Stage Stage { get; set; }
        public string SRWSectionCipher { get; set; }

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

        private List<Stage> _stagesSRWList;
        public List<Stage> StagesSRWList
        {
            get { return _stagesSRWList; }
            set
            {
                _stagesSRWList = value;
                OnPropertyChanged();
            }
        }

        public SRWStagesViewModel(Section section)
        {
            SectionSRW = section;
            StagesSRWList = SectionSRW.GetStages();
            SRWSectionCipher = SectionSRW.Cipher;

            FirstSRWStageCommand = new SetStageLinesCommand(GetStage((int)SRWStagesEnum.RD));
            SecondSRWStageCommand = new SetStageLinesCommand(GetStage((int)SRWStagesEnum.TaER));
            ThirdSRWStageCommand = new SetStageLinesCommand(GetStage((int)SRWStagesEnum.ETW));
            FourthSRWStageCommand = new SetStageLinesCommand(GetStage((int)SRWStagesEnum.SRWA));

            colors = SectionSRW.GetSectionColorList();
        }

        private Stage GetStage(int value)
        {
            StageDB stageDB = new StageDB();
            Stage stage = stageDB.SelectMethod<Stage>()
                .First(s => s.ProjectID == SectionSRW.ProjectID && s.SectionNum == SectionSRW.SectionNum && s.StageNum == value);
            return stage;
        }

        public ProjectDetailsViewModel GetProjectDetailsViewModel()
        {
            return Parent;
        }
    }
}
