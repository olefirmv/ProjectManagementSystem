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

namespace PMS.MVVM.ViewModel
{
    public class AllProjectsViewModel: ObservableObject
    {
        private List<Project> projectsFullPrivelegeList;
        private List<Project> projectsOtherPrivelegeList;
        private BindingList<Project> projectsFullPrivelegeBindingList;
        private BindingList<Project> projectsOtherPrivelegeBindingList;

        public RelayCommand MouseLeftButtonDown { get; private set; }
        public RelayCommand MouseLeftClick { get; private set; }
        public RelayCommand RoleListPageCommand { get; private set; }
        public RelayCommand DocumentListPageCommand { get; private set; }
        public RelayCommand CreateProjectCommand { get; private set; }
        public DeleteProjectCommand DeleteProjectCommand { get; private set; }


        public MainViewModel Parent { get; set; }
        public Employee Employee { get; set; }

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

        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }
        

        private object _projectsFullPrivelegeList;
        public object ProjectsFullPrivelegeListPage
        {
            get { return _projectsFullPrivelegeList; }
            set
            {
                _projectsFullPrivelegeList = value;
                OnPropertyChanged();
            }
        }
        

        private object _projectsOtherPrivelegeListPage;
        public object ProjectsOtherPrivelegeListPage
        {
            get { return _projectsOtherPrivelegeListPage; }
            set
            {
                _projectsOtherPrivelegeListPage = value;
                OnPropertyChanged();
            }
        }

        public AllProjectsViewModel(Employee employee)
        {
            Employee = employee;
            projectsFullPrivelegeBindingList = GetProjectsByPrivelege(employee, new Privelege(true, true, true, true), true, out projectsFullPrivelegeList);
            projectsOtherPrivelegeBindingList = GetProjectsByPrivelege(employee, new Privelege(true, true, true, true), false, out projectsOtherPrivelegeList);
            ProjectsFullPrivelegeListPage = projectsFullPrivelegeBindingList;
            ProjectsOtherPrivelegeListPage = projectsOtherPrivelegeBindingList;

            InitCommand();
        }
        public void InitCommand()
        {
            CreateProjectCommand = new RelayCommand(parameter =>
            {
                CreateProjectViewModel createProjectViewModel = new CreateProjectViewModel()
                {
                    Parent = this,
                    ParentMain = this.Parent
                };

                Parent.CreateProjectVM = createProjectViewModel;
                Parent.CurrentView = Parent.CreateProjectVM;
            });

            MouseLeftButtonDown = new RelayCommand(_object =>
            {
                MouseLeftButtonDownFunc(_object);
            });
            MouseLeftClick = new RelayCommand(_object =>
            {
                MouseLefClickFunc(_object);
            });

            RoleListPageCommand = new RelayCommand(parameter =>
            {
                RoleListPageViewModel roleListPageView = new RoleListPageViewModel()
                {
                    Parent = this
                };

                Parent.CurrentView = roleListPageView;
            });

            DocumentListPageCommand = new RelayCommand(parameter =>
            {
                DocumentListPageViewModel documentListPageView = new DocumentListPageViewModel()
                {
                    _parent = this,
                    mainViewModel = Parent
                    
                };

                Parent.CurrentView = documentListPageView;
            });

            DeleteProjectCommand = new DeleteProjectCommand();
        }

        private void MouseLeftButtonDownFunc(object _object)
        {
            Project project = _object as Project;
            bool isPermissible = SetupIsPermissible(project);
            ProjectDetailsViewModel projectDetailsViewModel = new ProjectDetailsViewModel(project, this, this.Parent, isPermissible);

            Parent.CurrentView = projectDetailsViewModel;
        }
        private void MouseLefClickFunc(object _object)
        {
            Project project = _object as Project;
            SelectedProject = project;
        }

        private bool SetupIsPermissible(Project project)
        {
            return projectsFullPrivelegeList.Contains(project);
        }

        private BindingList<Project> GetProjectsByPrivelege(Employee employee, Privelege privelege, bool full, out List<Project> projectsList)
        {
            PrivelegeDB privelegeDB = new PrivelegeDB();
            ProjectDB projectDB = new ProjectDB();
            var tempPrivelege = privelegeDB.FindOrCreate(privelege);

            projectsList = projectDB.GetProjectsByEmployeeAndPrivelege(employee, tempPrivelege.PrivelegeID, full);
            BindingList<Project> projects = new BindingList<Project>(projectsList);

            return projects;
        }
    }
}
