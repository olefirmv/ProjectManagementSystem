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
    public class ProjectDetailsViewModel : ObservableObject
    {
        public RelayCommand ChangeSectionCommand { get; private set; }
        public RelayCommand MemberManagerCommand { get; private set; }
        public RelayCommand EmployeeManagerCommand { get; private set; }
        public RelayCommand ComponentManagerCommand { get; private set; }
        public RelayCommand ProjectInfoCommand { get; private set; }
        public RelayCommand ChangeProjectStatusCommand { get; private set; }


        public EDWStagesViewModel EDWStagesVM { get; set; }
        public SRWStagesViewModel SRWStagesVM { get; set; }
        public MemberListPageViewModel MemberListPageVM { get; set; }


        public MainViewModel ParentMain { get; set; }
        public AllProjectsViewModel Parent { get; set; }
        public Project Project { get; set; }
        public Section sectionSRW { get; set; }
        public Section sectionEDW { get; set; }
        public Employee Employee { get; set; }
        public bool IsSRWExists { get; set; }
        public bool IsEDWExists { get; set; }

        private bool _isChangeable;
        public bool IsChangeable
        {
            get { return _isChangeable; }
            set
            {
                _isChangeable = value;
                OnPropertyChanged();
            }
        }

        private bool _isPermissible;
        public bool IsPermissible
        {
            get
            {
                return _isPermissible;
            }
            set
            {
                _isPermissible = value;
                OnPropertyChanged();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public Privelege privelegeByUser {get; internal set;}

        public ProjectDetailsViewModel(Project project, AllProjectsViewModel allProjectsVM, MainViewModel mainVM, bool isPermissible)
        {
            Project = project;
            Parent = allProjectsVM;
            ParentMain = mainVM;
            Employee = allProjectsVM.Employee;
            InitEDWProperties();
            InitSRWProperties();
            SetViewByInit();
            IsChangeable = SetupIsChangeable();
            IsPermissible = isPermissible;
            privelegeByUser = InitPrivelegeByUser();

            InitCommands();
        }

        private Privelege InitPrivelegeByUser()
        {
            PrivelegeDB privelegeDB = new PrivelegeDB();
            return privelegeDB.GetPrivelegeByEmployeeAndProject(Employee, Project);
        }

        public void InitCommands()
        {
            ChangeSectionCommand = new RelayCommand(parameter =>
            {
                ChangeSectionView();
            });

            MemberManagerCommand = new RelayCommand(parameter =>
            {
                MemberListPageViewModel memberListPageVM = new MemberListPageViewModel(Project)
                {
                    Parent = this
                };
                this.CurrentView = memberListPageVM;

            });
            EmployeeManagerCommand = new RelayCommand(parameter =>
            {
                EmployeeListPageViewModel employeeListPageVM = new EmployeeListPageViewModel(Project)
                {
                    Parent = this
                };
                this.CurrentView = employeeListPageVM;
            });

            ProjectInfoCommand = new RelayCommand(parameter =>
            {
                ProjectInfoViewModel projectInfoListPageVM = new ProjectInfoViewModel(this);
                this.CurrentView = projectInfoListPageVM;
            });

            ComponentManagerCommand = new RelayCommand(parameter =>
            {
                ComponentInfoViewModel componentInfoViewVM = new ComponentInfoViewModel(this);
                this.CurrentView = componentInfoViewVM;

            });
        }

        public void ChangeSectionView()
        {
            if (CurrentView == SRWStagesVM && EDWStagesVM != null)
            {
                EDWStagesVM = new EDWStagesViewModel(sectionEDW)
                {
                    Parent = this,
                    ParentMain = this.ParentMain
                };
                CurrentView = EDWStagesVM;
            }

            else if (CurrentView == EDWStagesVM && SRWStagesVM != null)
            {
                SRWStagesVM = new SRWStagesViewModel(sectionSRW)
                {
                    Parent = this,
                    ParentMain = this.ParentMain
                };
                CurrentView = SRWStagesVM;
            }
        }

        public bool SetupIsChangeable()
        {
            bool result = false;

            if(IsSRWExists && IsEDWExists)
            {
                result = true;
            }
            
            return result;
        }

        public void InitEDWProperties()
        {
            IsEDWExists = Project.IsSectionExist(2);
            if (IsEDWExists)
            {
                sectionEDW = Project.GetSectionByNumber(2);
            }
        }
        public void InitSRWProperties()
        {
            IsSRWExists = Project.IsSectionExist(1);
            if (IsSRWExists)
            {
                sectionSRW = Project.GetSectionByNumber(1);
            }
        }

        public void SetViewByInit()
        {
            if(IsEDWExists)
            {
                EDWStagesVM = new EDWStagesViewModel(sectionEDW)
                {
                    Parent = this,
                    ParentMain = this.ParentMain
                };

                CurrentView = EDWStagesVM;
            }
            if (IsSRWExists)
            {
                SRWStagesVM = new SRWStagesViewModel(sectionSRW)
                {
                    Parent = this,
                    ParentMain = this.ParentMain
                };
                CurrentView = SRWStagesVM;
            }
        }

    }
}
