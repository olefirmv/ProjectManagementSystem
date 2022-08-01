using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMS.BL.Core;
using PMS.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PMS.BL.Test
{
    [TestClass]
    public class DeleteProcesses
    {
        private TransactionScope scope;

        [TestMethod, Rollback]
        public void DeleteMemberTest()
        {
            Project project = TestHelper.CreateDefaultProject();
            Section section = project.AddSection(SectionEnum.SRW, TestValue.testCipher);

            section.InitStages();

            Role roleCustomer = TestHelper.CreateRoleCustomer();

            Member member = TestHelper.CreateDefaultMember(project.ProjectID);

            MemberDB memberDB = new MemberDB();
            bool ret = memberDB.DeleteRecord(member);


            Assert.IsTrue(ret, "Member has been deleted");
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
