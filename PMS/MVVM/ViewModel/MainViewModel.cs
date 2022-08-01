using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BL.Model;
using PMS.Core;

namespace PMS.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand LogoutCommand { get; private set; }
        public RelayCommand HomeCommand { get; private set; }
        public RelayCommand ShowEmployeeInfoCommand { get; private set; }


        public LoginViewModel LoginVM { get; set; }
        public CreateProjectViewModel CreateProjectVM { get; set; }
        public AllProjectsViewModel AllProjectsVM { get; set; }
        public EmployeeInfoViewModel EmployeeInfoVM { get; set; }

        private Employee _employee;
        public Employee Employee 
        {   
            get { return _employee; }
            set
            {
                _employee = value;
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

        public object Name
        {
            get
            {
                return Employee.Name;
            }
            set
            {
                Employee.Name = value.ToString();
                OnPropertyChanged();
            }
        }

        private bool _onChecked;
        public bool OnChecked
        {
            get { return _onChecked; }
            set
            {
                _onChecked = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            LoginVM = new LoginViewModel()
            {
                ParentMain = this
            };
            
            CreateProjectVM = new CreateProjectViewModel();
            
            Employee = new Employee()
            {
                Name = "Неавторизован"
            };

            CurrentView = LoginVM;

            LogoutCommand = new RelayCommand(o =>
            {                
                Employee = new Employee()
                {
                    Name = "Неавторизован"
                };
                OnChecked = false;
                LoginVM = new LoginViewModel()
                {
                    ParentMain = this
                };
                CurrentView = LoginVM;
            });

            HomeCommand = new RelayCommand(o =>
            {
                AllProjectsVM = new AllProjectsViewModel(Employee)
                {
                    Parent = this
            };
                CurrentView = AllProjectsVM;
            });


            ShowEmployeeInfoCommand = new RelayCommand(o =>
            {
                AllProjectsVM = new AllProjectsViewModel(Employee)
                {
                    Parent = this
                };
                CurrentView = AllProjectsVM;
                
                EmployeeInfoVM = new EmployeeInfoViewModel(Employee)
                {
                    Parent = AllProjectsVM,
                    ParentMain = this
                };
                AllProjectsVM.HelperView = EmployeeInfoVM;
            });
        }
    }
}
