using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PMS.MVVM.ViewModel
{
    public class ComponentInfoViewModel: ObservableObject
    {
        private BindingList<BL.Model.Component> componentList;

        public AddComponentCommand AddComponentCommand {get; private set;}
        public DeleteComponentCommand DeleteComponentCommand {get; private set;}
        public RelayCommand BackCommand {get; private set;}

        public ProjectDetailsViewModel Parent { get; set;}
        public Project Project { get; set;}
        
        private object _filter;
        public object Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                OnFilterChanged();
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

        private object _componentList;
        public object ComponentList
        {
            get
            {
                return _componentList;
            }
            set
            {
                _componentList = value;
                OnPropertyChanged();
            }
        }

        public object Name
        {
            get
            {
                return Project.Name;
            }
            set
            {
                Project.Name = value.ToString();
                OnPropertyChanged(nameof(Name));
            }
        }
        public object Status
        {
            get
            {
                return Project.Status.ToString();
            }
            set
            {
                Project.Status = (int)value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private object _selectedComponent;
        public object SelectedComponent
        {
            get
            {
                return _selectedComponent;
            }
            set
            {
                _selectedComponent = value;
                OnItemChanged();
                InitComponentProjectRoleViewModel();
                OnPropertyChanged();
            }
        }

        private void InitComponentProjectRoleViewModel()
        {
            ComponentProjectRoleViewModel componentProjectRoleViewModel = new ComponentProjectRoleViewModel(this);

            this.HelperView = componentProjectRoleViewModel;
        }

        private object _newComponent;
        public object NewComponent
        {
            get
            {
                return _newComponent;
            }
            set
            {
                _newComponent = value;
                OnPropertyChanged();
            }
        }

        private object _newName;
        public object NewName
        {
            get
            {
                return _newName;
            }
            set
            {
                _newName = value;
                OnPropertyChanged();
            }
        }

        private object _newCipher;
        public object NewCipher
        {
            get
            {
                return _newCipher;
            }
            set
            {
                _newCipher = value;
                OnPropertyChanged();
            }
        }


        private object _selectedSection;
        public object SelectedSection
        {
            get
            {
                return _selectedSection;
            }
            set
            {
                _selectedSection = value;
                OnPropertyChanged();
            }
        }

        private object _sectionList;
        public object SectionList
        {
            get
            {
                return _sectionList;
            }
            set
            {
                _sectionList = value;
                OnPropertyChanged();
            }
        }

        public ComponentInfoViewModel(ProjectDetailsViewModel parent)
        {
            Parent = parent;
            Project = parent.Project;
            ComponentList = InitComponentList();
            SectionList = InitSectionList();
            AddComponentCommand = new AddComponentCommand();
            DeleteComponentCommand = new DeleteComponentCommand();
            

            BackCommand = new RelayCommand(parameter =>
            {
                ProjectDetailsViewModel projectDetailsVM = new ProjectDetailsViewModel(Project, this.Parent.Parent, this.Parent.Parent.Parent, this.Parent.IsPermissible);

                this.Parent.Parent.Parent.CurrentView = projectDetailsVM;
            });
        }

        private List<ObjectClass> InitSectionList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            foreach (var sectionNum in Enum.GetValues(typeof(SectionEnum)))
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = (int)sectionNum,
                    Name = PMSBLHelper.GetDescriptionByEnumType((SectionEnum)sectionNum)
                };

                list.Add(objectClass);
            }

            return list;
        }

        private BindingList<BL.Model.Component> InitComponentList()
        {
            ComponentDB componentDB = new ComponentDB();
            BindingList<BL.Model.Component> list = new BindingList<BL.Model.Component>(componentDB.SelectMethod<BL.Model.Component>().Where(c => c.ProjectID == Project.ProjectID).ToList());
            return list;
        }

        private void OnItemChanged()
        {
            if (SelectedComponent != null)
            {
                NewName = (SelectedComponent as BL.Model.Component).Name;
                NewCipher = (SelectedComponent as BL.Model.Component).Cipher;
                SelectedSection = (SelectedComponent as BL.Model.Component).SectionNum;
            }
        }

        private void OnFilterChanged()
        {
            List<BL.Model.Component> components = componentList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            ComponentList = new BindingList<BL.Model.Component>(components);
        }

    }
}
