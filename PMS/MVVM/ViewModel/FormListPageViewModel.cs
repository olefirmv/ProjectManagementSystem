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
    public class FormListPageViewModel : ObservableObject
    {
        private BindingList<Form> formList;

        public ICommand AddFormCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }

        public DocumentListPageViewModel Parent { get; set; }

        private object _formList;
        public object FormList
        {
            get { return _formList; }
            set
            {
                _formList = value;
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

        private object _newForm;
        public object NewForm
        {
            get { return _newForm; }
            set
            {
                _newForm = value;
                OnPropertyChanged();
            }
        }

        public FormListPageViewModel()
        {
            formList = InitFormList();
            FormList = formList;

            AddFormCommand = new AddFormCommand();
            BackCommand = new RelayCommand(parameter =>
            {
                DocumentListPageViewModel documentListPagesVM = new DocumentListPageViewModel()
                {
                    mainViewModel = Parent.mainViewModel,
                    _parent = Parent._parent
                };
                Parent.mainViewModel.CurrentView = documentListPagesVM;
            });
        }

        private void OnFilterChanged()
        {
            List<Form> forms = formList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            FormList = new BindingList<Form>(forms);
        }

        public void SetFormList(BindingList<Form> forms)
        {
            formList = forms;
            FormList = formList;
        }


        public BindingList<Form> InitFormList()
        {
            return new BindingList<Form>(new FormDB().SelectMethod<Form>());
        }
    }
}
