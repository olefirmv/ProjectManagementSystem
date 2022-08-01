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
    public class DocumentListPageViewModel : ObservableObject
    {
        public RelayCommand FormListPageCommand { get; set; }
        public ICommand AddDocumentCommand { get; }
        public ICommand EditDocumentCommand { get; }
        public ICommand DeleteDocumentCommand { get; }
        public RelayCommand BackCommand {get; }
        public AllProjectsViewModel _parent;
        public MainViewModel mainViewModel;

        private object _activeDocument;
        public object ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                OnPropertyChanged();
            }
        }

        private BindingList<Document> documentList;

        private object _documentList;
        public object DocumentList
        {
            get { return _documentList; }
            set
            {
                _documentList = value;
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

        private object _newDocument;
        public object NewDocument
        {
            get { return _newDocument; }
            set
            {
                _newDocument = value;
                OnPropertyChanged();
            }
        }

        private void OnFilterChanged()
        {
            List<Document> documents = documentList.Where(x => x.Name.Contains(Filter.ToString())).ToList();

            DocumentList = new BindingList<Document>(documents);
        }

        public DocumentListPageViewModel()
        {
            documentList = InitDocumentList();
            DocumentList = documentList;

            AddDocumentCommand = new AddDocumentCommand();
            EditDocumentCommand = new EditDocumentCommand();
            DeleteDocumentCommand = new DeleteDocumentCommand();

            FormListPageCommand = new RelayCommand(parameter =>
            {
                FormListPageViewModel formListPageView = new FormListPageViewModel()
                {
                    Parent = this
                };
                mainViewModel.CurrentView = formListPageView;
            });

            BackCommand = new RelayCommand(parameter =>
            {
                AllProjectsViewModel allProjectsVM = new AllProjectsViewModel(_parent.Employee)
                {
                    Parent = _parent.Parent
                };
                _parent.Parent.CurrentView = allProjectsVM;
            });
        }

        public void SetDocumentList(BindingList<Document> documents)
        {
            documentList = documents;
            DocumentList = documentList;
        }

        public BindingList<Document> InitDocumentList()
        {
            return new BindingList<Document>(new DocumentDB().SelectMethod<Document>());
        }
    }
}
