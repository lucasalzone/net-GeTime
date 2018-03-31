using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime.Tests
{
	[TestClass()]
	public class ConntrollerTimeSheetTests
	{
        private ConntrollerTimeSheet cont = new ConntrollerTimeSheet("TestDB");
        [TestMethod()]
		public void CompilaHLTest()
		{
			//bool cavallo = cont.CompilaHL(DateTime.Today, 5, 10);
			//Assert.IsTrue(cavallo);
		}

		[TestMethod()]
		public void CompilaHFTest()
		{
			bool cavallo = cont.CompilaHF(DateTime.Today, 5, 10);
			Assert.IsTrue(cavallo);
		}

		[TestMethod()]
		public void CompilaHMTest()
		{
			bool cavallo = cont.CompilaHM(DateTime.Today,5,3);
			Assert.IsTrue(cavallo);
		}

		[TestMethod()]
		public void CompilaHPTest()
		{
			bool cavallo = cont.CompilaHP(DateTime.Today, 5, 10);
			Assert.IsTrue(cavallo);
		}
		[ClassInitialize]
		public static void InitClass(TestContext e)
		{
			ConntrollerTimeSheet.InitTest("TestDB", "TestDB.sql");
		}
		/*[TestInitialize]
		public void InitMethod()
		{
            cont.Drop();
		}*/

        [TestMethod]
        public void SearchGiornoTest()
        {
			cont.ExecP("TestSearchGiorno");
			Giorno giorno =cont.SearchGiorno(new DateTime(2018,03,31),200);
			Assert.IsNotNull(giorno);
            Assert.IsTrue(giorno.ID_UTENTE == 200);
			Assert.IsTrue(giorno.HL == 4);
			Assert.IsTrue(giorno.Commesse.Count == 2);
		}
    }
}