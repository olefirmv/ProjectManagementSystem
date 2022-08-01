using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMS.BL.Core;
using System.Linq;
using PMS.BL.Model;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace PMS.BL.Test
{
    [TestClass]
    public class CreationProcesses
    {
        private TransactionScope scope;
        
        [TestMethod, Rollback]
        public void CreateProjectTest()
        {
            Project project = TestHelper.CreateDefaultProject();
            var projectDB = new ProjectDB();

            var resProj = projectDB.SelectMethod<Project>().First(p => p.ProjectID == project.ProjectID);

            Assert.IsTrue(resProj.ProjectID != 0);            
        }

        [TestMethod, Rollback]
        public void CreateCredentialTest()
        { 
            Credential credential = TestHelper.CreateDefaultCredential();

            CredentialDB credentialDB = new CredentialDB();

            Assert.IsTrue(credentialDB.IsExists(credential.Login), "Credential does not exist");
            Assert.IsTrue(PMSBLHelper.EncryptPassword(TestValue.passwordAdmin) == credential.Password, "Wrong password is found");
        }

        [TestMethod, Rollback]
        public void CreatePrivelegeTest()
        {
            Privelege privelege = TestHelper.CreateDefaultPrivelege();

            Assert.IsTrue(privelege.PrivelegeID != 0, "Privelege does not exist");
        }

        [TestMethod, Rollback]
        public void CreateSectionTest()
        {
            Project project = TestHelper.CreateDefaultProject();
            Section section = project.AddSection(SectionEnum.SRW, TestValue.testCipher);

            DataBase dataBase = new DataBase();
            Section resSection =  dataBase.SelectMethod<Section>().First(p => p.ProjectID == section.ProjectID && p.SectionNum == section.SectionNum);

            Assert.IsTrue(resSection.SectionNum != 0, "Section does not exist");
            Assert.IsTrue(resSection.ProjectID != 0, "Section does not exist");
        }

        [TestMethod, Rollback]
        public void CreateStagesTest()
        {
            Project project = TestHelper.CreateDefaultProject();
            Section section = project.AddSection(SectionEnum.SRW, TestValue.testCipher);

            section.InitStages();

            DataBase dataBase = new DataBase();
            List<Stage> stages = dataBase.SelectMethod<Stage>().Where(p => p.ProjectID == section.ProjectID && p.SectionNum == section.SectionNum).ToList();

            Assert.IsTrue(stages.Count == 4, "Section does not exist");
        }

        [TestMethod, Rollback]
        public void CreateStageLineTest()
        {
            Project project = TestHelper.CreateDefaultProject();
            Section section = project.AddSection(SectionEnum.SRW, TestValue.testCipher);

            section.InitStages();

            DataBase dataBase = new DataBase();
            List<Stage> stages = dataBase.SelectMethod<Stage>().Where(p => p.ProjectID == section.ProjectID && p.SectionNum == section.SectionNum).ToList();
            Stage stage = stages.First();

            Role roleAdmin = TestHelper.CreateRoleAdmin();
            Role roleDeveloper = TestHelper.CreateRoleDeveloper();
            Role roleCustomer = TestHelper.CreateRoleCustomer();

            Form form = TestHelper.CreateDefaultForm();

            Document document = TestHelper.CreateDefaultDocument();

            Credential credential = TestHelper.CreateDefaultCredential();
            Employee employee = TestHelper.CreateDefaultEmployee();

            StageLine stageLine = stage.AddStageLine(TestValue.testStageLineName, document.DocumentID, DateTime.Now, DateTime.Now.AddDays(1), employee.EmployeeID);

            Assert.IsTrue(stageLine.StageLineID != 0, "Stage line does not exist");
            Assert.IsTrue(stageLine.ProjectID == project.ProjectID, "Wrong project id is found");
        }

        [TestMethod, Rollback]
        public void AddProjectMember()
        {
            Project project = TestHelper.CreateDefaultProject();
            Section section = project.AddSection(SectionEnum.SRW, TestValue.testCipher);

            section.InitStages();

            Role roleCustomer = TestHelper.CreateRoleCustomer();

            Member member = TestHelper.CreateDefaultMember(project.ProjectID);

            ProjectRole projectRole = project.AddProjectMemberRole(section.SectionNum, 0, roleCustomer.RoleID, member.MemberID);

            Assert.IsTrue(projectRole != null, "Project role does not exist");
            Assert.IsTrue(projectRole.RoleID == roleCustomer.RoleID, "Wrong role id is found");
        }


        [TestInitialize]
        public void Initialize()
        {
            this.scope = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();
        }
    }
}
