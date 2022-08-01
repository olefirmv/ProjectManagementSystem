using PMS.BL.Core;
using PMS.BL.Model;
using PMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.MVVM.ViewModel
{
    public class DocumentDetailsViewModel : ObservableObject
    {
        public UpdateDocumentCommand UpdateDocumentCommand { get; private set; }
        public CancelDocumentCommand CancelDocumentCommand { get; private set; }
        public EditDevelopsCommand EditDevelopsCommand { get; private set; }
        public EditAgreesCommand EditAgreesCommand { get; private set; }
        public EditClaimsCommand EditClaimsCommand { get; private set; }


        public DocumentListPageViewModel Parent { get; set;}
        public MainViewModel ParentMain { get; set;}
        public Document document;

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


        private object _formList;
        public object FormList
        {
            get
            {
                return _formList;
            }
            set
            {
                _formList = value;
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

        private object _stageList;
        public object StageList
        {
            get
            {
                return _stageList;
            }
            set
            {
                _stageList = value;
                OnPropertyChanged();
            }
        }
        public object FormID
        {
            get
            {
                return document.FormID;
            }
            set
            {
                document.FormID = (int)value;
                OnPropertyChanged();
            }
        }

        public object SectionNum
        {
            get
            {
                return document.SectionNum;
            }
            set
            {
                document.SectionNum = (int)value;
                OnPropertyChanged();
                OnSectionChanged();
            }
        }

        public object Name
        {
            get
            {
                return document.Name;
            }
            set
            {
                document.Name = value.ToString();
                OnPropertyChanged();
            }
        }

        public object Comment
        {
            get
            {
                return document.Comment;
            }
            set
            {
                document.Comment = value.ToString();
                OnPropertyChanged();
            }
        }

        public object StageNum
        {
            get
            {
                return document.StageNum;
            }
            set
            {
                document.StageNum = (int)value;
                OnPropertyChanged();
            }
        }

        public object Develops
        {
            get
            {
                return document.GetDevelops();
            }
            set
            {
                document.Develops = value.ToString();
                OnPropertyChanged();
            }
        }

        public object Agrees
        {
            get
            {
                return document.GetAgrees();
            }
            set
            {
                document.Agrees = value.ToString();
                OnPropertyChanged();
            }
        }

        public object Claims
        {
            get
            {
                return document.GetClaims();
            }
            set
            {
                document.Claims = value.ToString();
                OnPropertyChanged();
            }
        }

        public DocumentDetailsViewModel(Document document)
        {
            EditAgreesCommand = new EditAgreesCommand();
            EditClaimsCommand= new EditClaimsCommand();
            EditDevelopsCommand = new EditDevelopsCommand();
            UpdateDocumentCommand = new UpdateDocumentCommand();
            CancelDocumentCommand = new CancelDocumentCommand();

            FormList = InitFormsList();            

            if (document == null)
            {
                this.document = new Document();
                SectionList = InitSectionsList();
            }
            else
            {
                this.document = document;
                SectionList = InitSectionsList();
                StageList = InitStagesList();
            }                        
        }

        private void OnSectionChanged()
        {
            StageList = InitStagesList();
        }

        private List<ObjectClass> InitFormsList()
        {
            FormDB formDB = new FormDB();

            List<ObjectClass> list = new List<ObjectClass>();
            List<Form> forms = formDB.SelectMethod<Form>();

            foreach (Form form in forms)
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Name = form.Name,
                    Value = form.FormID
                };

                list.Add(objectClass);
            }

            return list;
        }

        private List<ObjectClass> InitSectionsList()
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

        private List<ObjectClass> InitStagesList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            if (SectionNum != null && (SectionEnum)SectionNum != 0)
            {
                SectionEnum sectionEnum = (SectionEnum)SectionNum;

                switch (sectionEnum)
                {
                    case SectionEnum.SRW:
                        foreach (var stageNum in Enum.GetValues(typeof(SRWStagesEnum)))
                        {
                            ObjectClass objectClass = new ObjectClass()
                            {
                                Value = (int)stageNum,
                                Name = PMSBLHelper.GetDescriptionByEnumType((SRWStagesEnum)stageNum)
                            };

                            list.Add(objectClass);
                        }
                        break;
                    case SectionEnum.EDW:
                        foreach (var stageNum in Enum.GetValues(typeof(EDWStagesEnum)))
                        {
                            ObjectClass objectClass = new ObjectClass()
                            {
                                Value = (int)stageNum,
                                Name = PMSBLHelper.GetDescriptionByEnumType((EDWStagesEnum)stageNum)
                            };

                            list.Add(objectClass);
                        }
                        break;
                    default:
                        throw new Exception("Wrong Section type");
                }
            }
            return list;
        }

    }
}
