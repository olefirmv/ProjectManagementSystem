using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Core;
using PMS.BL.Core;
using PMS.BL.Model;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using PMS.Bl.Extensions;

namespace PMS.MVVM.ViewModel
{
    public class CreateProjectViewModel: ObservableObject
    {
        
        public CreateProjectAndEnableSectionAndStageCommand CreateProjectAndEnableSectionsAndStagesCommand { get; private set; }
        public CreateSectionsAndStagesCommand CreateSectionsAndStagesCommand { get; private set; }

        public MainViewModel ParentMain { get; set; }
        public AllProjectsViewModel Parent { get; set; }
        
        public Project Project { get; }
        public Section SRWsection { get; set; }
        public Section EDWsection { get; set; }

        private static DateTime _dateTime = DateTime.Now;
        public static DateTime MyTime
        {
            get { return _dateTime; }
        }
        #region Project properties
        public string Name
        {
            get
            {
                return Project.Name;
            }
            set
            {
                Project.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime StartDate
        {
            get
            {
                return Project.StartDate;
            }
            set
            {
                Project.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get
            {
                return Project.EndDate;
            }
            set
            {
                Project.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        
        public string Status
        {
            get
            {
                return Project.Status.ToString();
            }
            set
            {
                Project.Status = CheckProjectStatus(value);
                OnPropertyChanged(nameof(Status));
            }
        }
        #endregion

        
        public BindingList<ProjectStatusEnum> ProjectStatuses { get; set; }

        private object _projectStatusesList;
        public object ProjectStatusesListPage
        {
            get { return _projectStatusesList; }
            set
            {
                _projectStatusesList = value;
            }
        }
        private bool _onchecked;
        public bool OnChecked
        {
            get { return _onchecked; }
            set
            {
                _onchecked = value;
                OnPropertyChanged();
            }
        }
        private bool _unchecked;
        public bool UnChecked
        {
            get { return _unchecked; }
            set
            {
                _unchecked = value;
                OnPropertyChanged();
            }
        }

        private string _selectedStartEDWContent;
        public string SelectedStartEDWContent
        {
            get
            {
                return _selectedStartEDWContent;
            }
            set
            {
                _selectedStartEDWContent = value;
                DisableMidEDWStages(_selectedStartEDWContent);
                OnPropertyChanged();
            }
        }

        private bool _isEnabledMidEDW = true;
        public bool IsEnabledMidEDW
        {
            get
            {
                return _isEnabledMidEDW;
            }
            set
            {
                _isEnabledMidEDW = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledEDW = true;
        public bool IsEnabledEDW
        {
            get
            {
                return _isEnabledEDW;
            }
            set
            {
                _isEnabledEDW = value;
                OnPropertyChanged();
            }
        }
        private bool _isEnabledSRW = true;
        public bool IsEnabledSRW
        {
            get
            {
                return _isEnabledSRW;
            }
            set
            {
                _isEnabledSRW = value;
                OnPropertyChanged();
            }
        }

        private void DisableMidEDWStages(string value)
        {
            if (value == "Этапы, начиная с Разработки ЭП, заканчивая Изготовлением ОО изделия ВТ и проведение ПИ объединены")
            {
                IsEnabledMidEDW = false;
                SelectedMiddleEDWContent = "";
            }
            else
            {
                IsEnabledMidEDW = true;
            }

        }

        private string _selectedMiddleEDWContent;
        public string SelectedMiddleEDWContent
        {
            get
            {
                return _selectedMiddleEDWContent;
            }
            set
            {
                _selectedMiddleEDWContent = value;
                OnPropertyChanged();
            }
        }
        private string _selectedEndEDWContent;
        public string SelectedEndEDWContent
        {
            get
            {
                return _selectedEndEDWContent;
            }
            set
            {
                _selectedEndEDWContent = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand GetStartEDWStagesCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    SelectedStartEDWContent = (string)p;
                });
            }
        }
        public DelegateCommand GetMiddleEDWStagesCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    SelectedMiddleEDWContent = (string)p;
                });
            }
        }
        public DelegateCommand GetEndEDWStagesCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    SelectedEndEDWContent = (string)p;
                });
            }
        }


        #region srw and edw properties - Cipher
        private string _SRWCipher;
        public string SRWCipher
        {
            get
            {
                return _SRWCipher;
            }
            set
            {
                _SRWCipher = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _ERWCipher;
        public string EDWCipher
        {
            get
            {
                return _ERWCipher;
            }
            set
            {
                _ERWCipher = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region bools for sections and stages
        //нир
        //1
        private bool _oneSRW;
        public bool OneSRW
        {
            get { return _oneSRW; }
            set
            {
                _oneSRW = value;
                OnPropertyChanged();
            }
        }
        //2
        private bool _twoSRW;
        public bool TwoSRW
        {
            get { return _twoSRW; }
            set
            {
                _twoSRW = value;
                OnPropertyChanged();
            }
        }
        //3
        private bool _threeSRW;
        public bool ThreeSRW
        {
            get { return _threeSRW; }
            set
            {
                _threeSRW = value;
                OnPropertyChanged();
            }
        }
        //4
        private bool _fourSRW;
        public bool FourSRW
        {
            get { return _fourSRW; }
            set
            {
                _fourSRW = value;
                OnPropertyChanged();
            }
        }
        //Исключить НИР
        private bool _resetSRW;
        public bool ResetSRW
        {
            get { return _resetSRW; }
            set
            {
                _resetSRW = value;
                DisableSRWStages(_resetSRW);
                OnPropertyChanged();
            }
        }
        
        //исключить окр
        private bool _resetEDW;
        public bool ResetEDW
        {
            get { return _resetEDW; }
            set
            {
                _resetEDW = value;
                DisableEDWStages(_resetEDW);
                OnPropertyChanged();
            }
        }
        

        #endregion

        public CreateProjectViewModel()
        {
            Project = new Project();
            Project.StartDate = new DateTime(2022, 1, 1);
            Project.EndDate = new DateTime(2022, 1, 1);

            ProjectStatusesListPage = InitBindingListProjectStatuses();
            UnChecked = true;
            InitCommand();
        }

        public void InitCommand()
        {
            CreateProjectAndEnableSectionsAndStagesCommand = new CreateProjectAndEnableSectionAndStageCommand();
            CreateSectionsAndStagesCommand = new CreateSectionsAndStagesCommand();
        }

        private void CreateSectionsAndStages()
        {
            if(!ResetSRW)
            {
                try
                {
                    SRWsection = Project.AddSection(SectionEnum.SRW, SRWCipher);
                    SRWsection.InitSRWStages(new List<bool>() { OneSRW, TwoSRW, ThreeSRW, FourSRW });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось добавить секцию. " + ex.Message);
                }
            }

            if (!ResetEDW)
            {
                try
                {
                    EDWsection = Project.AddSection(SectionEnum.EDW, EDWCipher);
                    EDWsection.InitEDWStages(new List<string>() { SelectedStartEDWContent, SelectedMiddleEDWContent, SelectedEndEDWContent });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось добавить секцию. " + ex.Message);
                }
            }

            MainViewModel mainViewModel = this.ParentMain;
            AllProjectsViewModel allProjectsViewModel = new AllProjectsViewModel(Parent.Employee)
            {
                Parent = mainViewModel
            };
            mainViewModel.AllProjectsVM = allProjectsViewModel;
            mainViewModel.CurrentView = mainViewModel.AllProjectsVM;
        }

        private void CreateProjectAndEnableSectionAndStage()
        {
            Project project = this.Project;
            project.ModifyDateTime = DateTime.Now; 

            if (project!=null)
            {
                try
                {
                    ProjectDB projectDB = new ProjectDB();
                    projectDB.InsertMethod(project);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Проект не создан");
            }

            try
            {
                ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();

                PrivelegeDB privelegeDB = new PrivelegeDB();
                Privelege privelege = privelegeDB.FindOrCreate(new Privelege(true,true,true,true));
                
                ProjectEmployee projectEmployee = new ProjectEmployee(project.ProjectID, ParentMain.Employee.EmployeeID, privelege.PrivelegeID);
                projectEmployeeDB.InsertMethod(projectEmployee);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            this.OnChecked = true;
            this.UnChecked = !OnChecked;
        }
        private void DisableSRWStages(bool resetSRW)
        {
            if (resetSRW)
            {
                IsEnabledSRW = false;
            }
            else
            {
                IsEnabledSRW = true;
            }
        }

        private void DisableEDWStages(bool resetEDW)
        {
            if (resetEDW)
            {
                IsEnabledEDW = false;
            }
            else
            {
                IsEnabledEDW = true;
            }
        }
        private BindingList<String> InitBindingListProjectStatuses()
        {
            var _projectStatuses = new BindingList<String>();

            string projectStatusPending = ProjectStatusEnum.Pending.GetDescription();
            string projectStatusInProgress = ProjectStatusEnum.InProgress.GetDescription();

            _projectStatuses.Add(projectStatusPending);
            _projectStatuses.Add(projectStatusInProgress);
            return _projectStatuses;
        }

        private int CheckProjectStatus(string value)
        {
            int result;
            switch (value)
            {
                case "Ожидает выполнения":
                    result = 1;
                    return result;
                case "В процессе":
                    result = 2;
                    return result;
                default: return 1;
            }
        }
    }
}
