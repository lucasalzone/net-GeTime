using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeTime;

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
			Giorno giorno= cont.SearchGiorno(DateTime.Today, 10);
			Assert.IsNotNull(giorno);
			Assert.IsTrue(giorno.Ore[(int)HType.HF] == 5);
		}

		[TestMethod()]
		public void CompilaHMTest()
		{
			bool cavallo = cont.CompilaHM(DateTime.Today,5,3);
			Assert.IsTrue(cavallo);
			Giorno giorno = cont.SearchGiorno(DateTime.Today, 3);
			Assert.IsNotNull(giorno);
			Assert.IsTrue(giorno.Ore[(int)HType.HM] == 5);
			Assert.IsTrue(cavallo);
		}

		[TestMethod()]
		public void CompilaHPTest()
		{
			bool cavallo = cont.CompilaHP(DateTime.Today, 5, 5);
			Assert.IsTrue(cavallo);
			Giorno giorno = cont.SearchGiorno(DateTime.Today, 5);
			Assert.IsNotNull(giorno);
			Assert.IsTrue(giorno.Ore[(int)HType.HP] == 5);
		}
		[ClassInitialize]
		public static void InitClass(TestContext e)
		{
			ConntrollerTimeSheet.InitTest("TestDB","TestDB.sql");
		}

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