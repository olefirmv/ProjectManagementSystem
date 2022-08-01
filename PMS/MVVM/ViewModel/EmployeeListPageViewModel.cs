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
    public class EmployeeListPageViewModel : ObservableObject
    {
        private BindingList<Employee> employeeList;
        private BindingList<TmpEmployeePrivelege> projectEmployeeList;
        private TmpEmployeePrivelege tmpEmployeePrivelege;

        public ICommand AddEmployeeCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }
        public RelayCommand BackCommand  { get; private set; }
        
        public ProjectDetailsViewModel Parent { get; set; }

        public Project Project { get; set; }

        private object _employeeList;
        public object EmployeeList
        {
            get { return _employeeList; }
            set
            {
                _employeeList = value;
                OnPropertyChanged();
            }
        }
        private object _projectEmployeeList;
        public object ProjectEmployeeList
        {
            get { return _projectEmployeeList; }
            set
            {
                _projectEmployeeList = value;
                OnPropertyChanged();
            }
        }

        public object EmployeeInfo
        {
            get { return tmpEmployeePrivelege.EmployeeInfo; }
            set
            {
                tmpEmployeePrivelege.EmployeeInfo = (string) value;
                OnPropertyChanged();
            }
        }
        
        public object _employeeID;
        public object EmployeeID
        {
            get { return _employeeID; }
            set
            {
                _employeeID = (int) value;
                OnPropertyChanged();
            }
        }

        public bool _readOne;
        public bool ReadOne
        {
            get { return _readOne; }
            set
            {
                _readOne = value;
                OnPropertyChanged();
            }
        }
        public bool _readFull;
        public bool ReadFull
        {
            get { return _readFull; }
            set
            {
                _readFull = value;
                OnPropertyChanged();
            }
        }
        public bool _editOne;
        public bool EditOne
        {
            get { return _editOne; }
            set
            {
                _editOne = value;
                OnPropertyChanged();
            }
        }
        public bool _editFull;
        public bool EditFull
        {
            get { return _editFull; }
            set
            {
                _editFull = value;
                OnPropertyChanged();
            }
        }

        private object _selectedMember;
        public object SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                OnPropertyChanged();
            }
        }

        private object _filter;
        public object Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnFilterChanged();
                OnPropertyChanged();
            }
        }

        private object _selectedEmployee;
        public object SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private object _newProjectEmployee;
        public object NewProjectEmployee
        {
            get { return _newProjectEmployee; }
            set
            {
                _newProjectEmployee = value;
                OnPropertyChanged();
            }
        }

        public EmployeeListPageViewModel(Project project)
        {
            Project = project;
            tmpEmployeePrivelege = new TmpEmployeePrivelege();

            ReadOne = false;
            ReadFull = false;
            EditOne = false;
            EditFull = false;
            EmployeeList = InitEmployeeList();

            projectEmployeeList = InitProjectEmployeeList();
            ProjectEmployeeList = projectEmployeeList;

            AddEmployeeCommand = new AddEmployeeCommand();
            DeleteEmployeeCommand = new DeleteEmployeeCommand();

            BackCommand = new RelayCommand(parameter =>
            {
                ProjectDetailsViewModel projectDetailsVM = new ProjectDetailsViewModel(Project, Parent.Parent, Parent.ParentMain, this.Parent.IsPermissible);
                Parent.ParentMain.CurrentView = projectDetailsVM;
            });
        }

        private void OnFilterChanged()
        {
            List<TmpEmployeePrivelege> projectEmployees = projectEmployeeList.Where(x => x.EmployeeInfo.Contains(Filter.ToString())).ToList();

            ProjectEmployeeList = new BindingList<TmpEmployeePrivelege>(projectEmployees);
        }

        public void SetTmpEmployeePrivelegeList(BindingList<TmpEmployeePrivelege> tmpEmployeePriveleges)
        {
            projectEmployeeList = tmpEmployeePriveleges;
            ProjectEmployeeList = projectEmployeeList;
        }

        public BindingList<TmpEmployeePrivelege> InitProjectEmployeeList()
        {
            ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();
            List<ProjectEmployee> projectEmployees = projectEmployeeDB.SelectMethod<ProjectEmployee>().Where(x => x.ProjectID == Project.ProjectID).ToList();
            List<TmpEmployeePrivelege> tmpEmployeePriveleges = new List<TmpEmployeePrivelege>();

            EmployeeDB employeeDB = new EmployeeDB();
            PrivelegeDB privelegeDB = new PrivelegeDB();

            foreach (var pE in projectEmployees)
            {
                TmpEmployeePrivelege tmpEmployeePrivelege = new TmpEmployeePrivelege();
                Employee employee = employeeDB.SelectMethod<Employee>().First(e => e.EmployeeID == pE.EmployeeID);
                tmpEmployeePrivelege.EmployeeInfo = tmpEmployeePrivelege.SetEmployeeInfo(employee);
                tmpEmployeePrivelege.EmployeeID = pE.EmployeeID;
                Privelege privelege = privelegeDB.SelectMethod<Privelege>().First(p => p.PrivelegeID == pE.PrivelegeID);
                tmpEmployeePrivelege.EditFull = tmpEmployeePrivelege.BoolToString(privelege.EditFull);
                tmpEmployeePrivelege.EditOne = tmpEmployeePrivelege.BoolToString(privelege.EditOne);
                tmpEmployeePrivelege.ReadOne = tmpEmployeePrivelege.BoolToString(privelege.ReadOne);
                tmpEmployeePrivelege.ReadFull = tmpEmployeePrivelege.BoolToString(privelege.ReadFull);
                tmpEmployeePriveleges.Add(tmpEmployeePrivelege);
            }
            return new BindingList<TmpEmployeePrivelege>(tmpEmployeePriveleges);
        }

        private List<ObjectClass> InitEmployeeList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            EmployeeDB employeeDB = new EmployeeDB();
            List<Employee> employeeList = employeeDB.SelectMethod<Employee>();

            foreach (Employee e in employeeList)
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = e.EmployeeID,
                    Name = e.SecondName + " " + e.Name + " " + e.Patronymic
                };

                list.Add(objectClass);
            }

            return list;
        }
    }

}
