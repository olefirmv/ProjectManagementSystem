using PMS.BL.Core;
using PMS.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BL.Test
{
    public class TestHelper
    {
        public static Project CreateDefaultProject()
        {
            Project project = new Project(TestValue.projName, DateTime.Now, DateTime.Now.AddDays(1), TestValue.projStatusPending)
            {
                ModifyDateTime = DateTime.Now
            };

            ProjectDB projectDB = new ProjectDB();
            projectDB.InsertMethod(project);

            return project;
        }

        public static Role CreateRoleAdmin()
        {
            Role role = new Role(TestValue.roleAdmin);
            RoleDB roleDB = new RoleDB();

            roleDB.InsertMethod(role);
            return role;
        }

        public static Role CreateRoleDeveloper()
        {
            Role role = new Role(TestValue.roleDeveloer);
            RoleDB roleDB = new RoleDB();

            roleDB.InsertMethod(role);
            return role;
        }

        public static Role CreateRoleCustomer()
        {
            Role role = new Role(TestValue.roleCustomer);
            RoleDB roleDB = new RoleDB();

            roleDB.InsertMethod(role);
            return role;
        }

        public static Form CreateDefaultForm()
        {
            Form form = new Form(TestValue.formNameTest);
            FormDB formDB = new FormDB();

            formDB.InsertMethod(form);
            return form;
        }

        public static Credential CreateDefaultCredential()
        {
            Credential credential = new Credential(TestValue.loginAdmin, TestValue.passwordAdmin);
            CredentialDB credentialDB = new CredentialDB();

            credentialDB.InsertMethod(credential);
            return credential;
        }

        public static Document CreateDefaultDocument()
        {
            DataBase db = new DataBase();

            var roleCustomer = db.SelectMethod<Role>().First(r => r.Name == TestValue.roleCustomer).RoleID;
            var roleDeveloper = db.SelectMethod<Role>().First(r => r.Name == TestValue.roleDeveloer).RoleID;
            var roleAdmin = db.SelectMethod<Role>().First(r => r.Name == TestValue.roleAdmin).RoleID;

            var formId = db.SelectMethod<Form>().First(f => f.Name == TestValue.formNameTest).FormID;

            Document document = new Document(
                TestValue.documentNameTest,
                roleCustomer + ";" + roleAdmin,
                "" + roleAdmin,
                "" + roleDeveloper,
                "test document comment",
                formId,
                TestValue.sectionNumTest,
                TestValue.stageNumTest);

            db.InsertMethod(document);

            return document;
        }

        public static Employee CreateDefaultEmployee()
        {
            Employee employee = new Employee(
                TestValue.employeeName,
                TestValue.employeeSecondName,
                TestValue.employeePatronymic,
                TestValue.testEmail,
                TestValue.loginAdmin);

            DataBase db = new DataBase();

            db.InsertMethod(employee);
            return employee;
        }

        public static Member CreateDefaultMember(int projectId)
        {
            Member member = new Member(TestValue.testMember, projectId);
            DataBase db = new DataBase();

            db.InsertMethod(member);
            return member;
        }

        public static Privelege CreateDefaultPrivelege()
        {
            Privelege privelege = new Privelege(true, true, true, true);
            PrivelegeDB privelegeDB = new PrivelegeDB();

            Privelege resPriv =  privelegeDB.FindOrCreate(privelege);
            return resPriv;
        }

        public static ProjectEmployee CreateDefaultPrivelege(int projectId, int employeeId, int privelegeId)
        {
            ProjectEmployee employee = new ProjectEmployee(projectId, employeeId, privelegeId);
            DataBase dataBase = new DataBase();

            dataBase.InsertMethod(employee);
            return employee;
        }

        public static Section CreateSRWSection(int projectId)
        {
            Section section = new Section(projectId, (int)SectionEnum.SRW, TestValue.testCipher);
            DataBase dataBase = new DataBase();

            dataBase.InsertMethod(section);
            return section;
        }
    }
}
