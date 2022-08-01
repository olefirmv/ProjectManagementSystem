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
    public class EmployeeInfoViewModel: ObservableObject
    {
        public ChangeEmployeePasswordCommand ChangeEmployeePasswordCommand { get; private set; }
        public SaveEmployeeInfoCommand SaveEmployeeInfoCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }

        public MainViewModel ParentMain { get; set;}
        public AllProjectsViewModel Parent { get; set;}

        public Employee Employee { get; set;}

        public string Name
        {
            get
            {
                return Employee.Name;
            }
            set
            {
                Employee.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        
        public string SecondName
        {
            get
            {
                return Employee.SecondName;
            }
            set
            {
                Employee.SecondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }
        
        public string Patronymic
        {
            get
            {
                return Employee.Patronymic;
            }
            set
            {
                Employee.Patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        public string Email
        {
            get
            {
                return Employee.Mail;
            }
            set
            {
                Employee.Mail = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        
        public string Login
        {
            get
            {
                return Employee.Login;
            }
            set
            {
                Employee.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }


        public EmployeeInfoViewModel(Employee employee)
        {
            Employee = employee;
            ChangeEmployeePasswordCommand = new ChangeEmployeePasswordCommand();
            SaveEmployeeInfoCommand = new SaveEmployeeInfoCommand();

            BackCommand = new RelayCommand(parameter =>
            {
                Parent.HelperView = null;
            });
        }


    }
}
