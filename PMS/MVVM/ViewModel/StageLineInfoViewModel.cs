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
    public class StageLineInfoViewModel : ObservableObject
    {
        public SaveStageLineCommand SaveStageLineCommand { get; private set;}

        public IStageViewModel parentStage;
        public StageLinesViewModel parent;
        public MainViewModel mainViewModel;
        private Document document;
        private BL.Model.Component component;

        public TmpStageLine tmpStageLine;

        public Section Section { get; set; }
        public Stage Stage { get; set; }
        public bool IsEditable { get; set; }
        #region Lists
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

        private object _statusList;
        public object StatusList
        {
            get
            {
                return _statusList;
            }
            set
            {
                _statusList = value;
                OnPropertyChanged();
            }
        }

        private object _componentList;
        public object ComponentList
        {
            get { return _componentList; }
            set
            {
                _componentList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region TmpStageLine props
        public object Name
        {
            get
            {
                return tmpStageLine.Name;
            }
            set
            {
                tmpStageLine.Name = value.ToString();
                OnPropertyChanged();
            }
        }

        public object DocumentID
        {
            get
            {
                return tmpStageLine.DocumentID;
            }
            set
            {
                tmpStageLine.DocumentID = (int)value;
                InitMainInfo();
                OnPropertyChanged();
            }
        }

        public object EmployeeID
        {
            get
            {
                return tmpStageLine.EmployeeID;
            }
            set
            {
                tmpStageLine.EmployeeID = (int)value;
                OnPropertyChanged();
            }
        }

        public object ComponentID
        {
            get
            {
                return tmpStageLine.ComponentID;
            }
            set
            {
                tmpStageLine.ComponentID = (int)value;
                InitComponentInfo();
                OnPropertyChanged();
            }
        }


        public DateTime StartDate
        {
            get
            {
                return tmpStageLine.StartDate;
            }
            set
            {
                tmpStageLine.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return tmpStageLine.EndDate;
            }
            set
            {
                tmpStageLine.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public int Status
        {
            get
            {
                return tmpStageLine.StatusStringToInt(tmpStageLine.Status);
            }
            set
            {
                tmpStageLine.Status = tmpStageLine.StatusIntToString(value);
                OnPropertyChanged(nameof(EndDate));
            }
        }
        #endregion

        #region document info
        private object _develops;
        public object Develops
        {
            get
            {
                return _develops;
            }
            set
            {
                _develops = value;
                OnPropertyChanged();
            }
        }
        private object _agrees;
        public object Agrees
        {
            get
            {
                return _agrees;
            }
            set
            {
                _agrees = value;
                OnPropertyChanged();
            }
        }
        private object _claims;
        public object Claims
        {
            get
            {
                return _claims;
            }
            set
            {
                _claims = value;
                OnPropertyChanged();
            }
        }
        private object _comment;
        public object Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
        private object _form;
        public object Form
        {
            get
            {
                return _form;
            }
            set
            {
                _form = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        public StageLineInfoViewModel(IStageViewModel parent, TmpStageLine TmpStageLine, bool isEditable)
        {

            this.parentStage = parent;
            tmpStageLine = TmpStageLine;
            EmployeeList = InitEmployeeList();
            DocumentList = InitDocumentList();
            ComponentList = InitComponentList();
            StatusList = InitStatusList();
            IsEditable = isEditable;
            if (tmpStageLine.Name != null)
            {
                InitDocumentInfo();
            }

            if (tmpStageLine.Name == null)
            {
                StartDate = new ProjectDB().SelectMethod<Project>().First(p => p.ProjectID == tmpStageLine.ProjectID).StartDate;
                EndDate = new ProjectDB().SelectMethod<Project>().First(p => p.ProjectID == tmpStageLine.ProjectID).StartDate;
            }
            SaveStageLineCommand = new SaveStageLineCommand();
        }

        private List<ObjectClass> InitEmployeeList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            ProjectEmployeeDB projectEmployeeDB = new ProjectEmployeeDB();
            List<ProjectEmployee> projectEmployeeList = projectEmployeeDB.SelectMethod<ProjectEmployee>()
                .Where(pE => pE.ProjectID == tmpStageLine.ProjectID).ToList();

            EmployeeDB employeeDB = new EmployeeDB();

            foreach (ProjectEmployee pE in projectEmployeeList)
            {
                Employee employee = employeeDB.SelectMethod<Employee>().First(e => e.EmployeeID == pE.EmployeeID);

                ObjectClass objectClass = new ObjectClass()
                {
                    Value = employee.EmployeeID,
                    Name = employee.SecondName + " " + employee.Name + " " + employee.Patronymic
                };
                list.Add(objectClass);
            }

            return list;
        }

        private List<ObjectClass> InitDocumentList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            DocumentDB documentDB = new DocumentDB();
            List<Document> documentList = documentDB.SelectMethod<Document>()
                .Where(d => d.SectionNum == tmpStageLine.SectionNum && d.StageNum == tmpStageLine.StageNum)
                .ToList();

            foreach (Document d in documentList)
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = d.DocumentID,
                    Name = d.Name
                };

                list.Add(objectClass);
            }

            return list;
        }


        private List<ObjectClass> InitComponentList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            ComponentDB componentDB = new ComponentDB();
            List<BL.Model.Component> componentList = componentDB.SelectMethod<BL.Model.Component>()
                .Where(d => d.ProjectID == tmpStageLine.ProjectID && d.SectionNum == tmpStageLine.SectionNum)
                .ToList();

            foreach (BL.Model.Component c in componentList)
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = c.ComponentID,
                    Name = c.Name
                };

                list.Add(objectClass);
            }

            return list;
        }

        private List<ObjectClass> InitStatusList()
        {
            List<ObjectClass> list = new List<ObjectClass>();

            foreach (var stageLineStatusNum in Enum.GetValues(typeof(StageLineStatusEnum)))
            {
                ObjectClass objectClass = new ObjectClass()
                {
                    Value = (int)stageLineStatusNum,
                    Name = PMSBLHelper.GetDescriptionByEnumType((StageLineStatusEnum)stageLineStatusNum)
                };

                list.Add(objectClass);
            }

            return list;
        }

        private void InitDocumentInfo()
        {
            //получил документ
            DocumentDB projectDB = new DocumentDB();
            document = projectDB.SelectMethod<Document>().First(d => d.DocumentID == tmpStageLine.DocumentID);

            //форма
            FormDB formDB = new FormDB();
            Form form = formDB.SelectMethod<Form>().First(f => f.FormID == document.FormID);

            //инит stageLine без компоненты
            InitMainInfo();
            //инит stageLine с компонентой
            if (tmpStageLine.ComponentID != 0)
            {
                InitComponentInfo();
            }


        }

        private void InitMainInfo()
        {
            DocumentDB projectDB = new DocumentDB();
            document = projectDB.SelectMethod<Document>().First(d => d.DocumentID == tmpStageLine.DocumentID);
            Form = document.FormName;
            Comment = document.Comment;
            // компонента с id = 0
            BL.Model.Component localComponent = new BL.Model.Component();
            localComponent.ProjectID = tmpStageLine.ProjectID;
            localComponent.SectionNum = tmpStageLine.SectionNum;
            localComponent.ComponentID = 0;

            string otherDevelops = GetInfoOther(localComponent, document.GetDevelopsRoleIDList());
            string otherAgrees = GetInfoOther(localComponent, document.GetAgreesRoleIDList());
            string otherClaims = GetInfoOther(localComponent, document.GetClaimsRoleIDList());

            Develops = otherDevelops == "" ? GetInfoMain(localComponent, document.GetDevelopsRoleIDList()) : GetInfoMain(localComponent, document.GetDevelopsRoleIDList()) + ", " + otherDevelops;
            Develops = Develops.ToString().StartsWith(", ") == true ? Develops.ToString().Remove(0, 2) : Develops;
            Agrees = otherAgrees == "" ? GetInfoMain(localComponent, document.GetAgreesRoleIDList()) : GetInfoMain(localComponent, document.GetAgreesRoleIDList()) + ", " + otherAgrees;
            Agrees = Agrees.ToString().StartsWith(", ") == true ? Agrees.ToString().Remove(0, 2) : Agrees;
            Claims = otherClaims == "" ? GetInfoMain(localComponent, document.GetClaimsRoleIDList()) : GetInfoMain(localComponent, document.GetClaimsRoleIDList()) + ", " + otherClaims;
            Claims = Claims.ToString().StartsWith(", ") == true ? Claims.ToString().Remove(0, 2) : Claims;
        }

        private void InitComponentInfo()
        {
            ComponentDB componentDB = new ComponentDB();
            component = componentDB.SelectMethod<BL.Model.Component>()
                .First(c => c.ProjectID == tmpStageLine.ProjectID && c.SectionNum == tmpStageLine.SectionNum && c.ComponentID == tmpStageLine.ComponentID);

            string componentDevelops = GetInfoMain(component, document.GetDevelopsRoleIDList());
            string componentAgrees = GetInfoMain(component, document.GetAgreesRoleIDList());
            string componentClaims = GetInfoMain(component, document.GetClaimsRoleIDList());

            Develops = componentDevelops == "" ? Develops: Develops + ", " + componentDevelops;
            Develops = Develops.ToString().StartsWith(", ") == true ? Develops.ToString().Remove(0, 2) : Develops;
            Agrees = componentAgrees == "" ? Agrees: Agrees + ", "+ componentAgrees;
            Agrees = Agrees.ToString().StartsWith(", ") == true ? Agrees.ToString().Remove(0, 2) : Agrees;
            Claims = componentClaims == "" ? Claims : Claims + ", " + componentClaims;
            Claims = Claims.ToString().StartsWith(", ") == true ? Claims.ToString().Remove(0, 2) : Claims;
        }

        private string GetInfoMain(BL.Model.Component component, List<int> roleIDList)
        {
            List<string> listNames = new List<string>();

            ProjectRoleDB projectRoleDB = new ProjectRoleDB();
            RoleDB roleDB = new RoleDB();
            MemberDB memberDB = new MemberDB();

            var tmpProjectRoles = projectRoleDB.SelectMethod<ProjectRole>();
            var tmpMembers = memberDB.SelectMethod<Member>();

            foreach (var roleID in roleIDList)
            {
                var tmp = tmpProjectRoles.FirstOrDefault(pR => pR.ProjectID == component.ProjectID && pR.SectionNum == component.SectionNum && pR.ComponentID == component.ComponentID && pR.RoleID == roleID);
                if (tmp != null)
                {
                    string roleName = tmpMembers.First(m => m.MemberID == tmp.MemberID).Name;
                    listNames.Add(roleName);
                }

            }

            return InitNames(listNames);
        }

        private string GetInfoOther(BL.Model.Component component, List<int> roleIDList)
        {
            List<string> listNames = new List<string>();

            ProjectRoleDB projectRoleDB = new ProjectRoleDB();
            RoleDB roleDB = new RoleDB();


            var tmpProjectRoles = projectRoleDB.SelectMethod<ProjectRole>();
            var tmpRoles = roleDB.SelectMethod<Role>();

            foreach (var roleID in roleIDList)
            {
                var tmp = tmpProjectRoles.FirstOrDefault(pR => pR.ProjectID == component.ProjectID && pR.SectionNum == component.SectionNum && pR.RoleID == roleID);
                if (tmp == null)
                {
                    string roleName = tmpRoles.First(r => r.RoleID == roleID).Name;
                    listNames.Add(roleName);
                }
            }

            return InitNames(listNames);
        }

        private string InitNames(List<string> listNames)
        {
            string result = "";
            if (listNames.Count != 0)
            {
                foreach (var s in listNames)
                {
                    result += s + ", ";
                }

                result = result.Remove(result.Length - 2);
            }
            
            return result;
        }
    }
}
