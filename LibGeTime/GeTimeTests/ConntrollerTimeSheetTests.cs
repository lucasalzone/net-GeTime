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
        private ConntrollerTimeSheet cont = new ConntrollerTimeSheet();
        [TestMethod()]
		public void CompilaHLTest()
		{
            cont.InsertCommessa("Oggi","Oggi mi faccio i cazzi mia", 666);
			bool cavallo = cont.CompilaHL(DateTime.Today, 5,"oggi", 111);
			Assert.IsTrue(cavallo);
			Giorno giorno= cont.SearchGiorno(DateTime.Today, 111);
			Assert.IsNotNull(giorno);
			Assert.IsTrue(giorno.HL == 5);
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
			ConntrollerTimeSheet.InitTest();
		}

        [TestMethod]
        public void SearchGiornoTest()
        {
			/*
			cont.CompilaHF(DateTime.Today,2, 200);
			cont.CompilaHP(DateTime.Today, 2, 200);
			cont.CompilaHM(DateTime.Today, 2, 200);
			cont.InsertCommessa("Prova Nauman", "provo di searchGiorno", 20);
			cont.CompilaHL(DateTime.Today, 2, "Prova Nauman", 200);
			cont.InsertCommessa("Prova 2 Nauman", "provo di searchGiorno", 20);
			cont.CompilaHL(DateTime.Today, 2, "Prova 2 Nauman", 200);
			*/
			cont.ExecP("TestSearchGiorno");
			Giorno giorno =cont.SearchGiorno(new DateTime(2018,03,31),200);
			Assert.IsNotNull(giorno);
            Assert.IsTrue(giorno.ID_UTENTE == 200);
			Assert.IsTrue(giorno.HL == 4);
			Assert.IsTrue(giorno.Commesse.Count == 2);
		}
        [TestMethod]
        public void SearchCommessaTest()
        {
            cont.InsertCommessa("Oggi","Oggi mi faccio i cazzi mia", 666);
            bool cavallo1 = cont.CompilaHL(DateTime.Today, 1,"oggi", 111);
            bool cavallo2 = cont.CompilaHL(new DateTime(2018,11,03), 3,"oggi", 111);
            bool cavallo3 = cont.CompilaHL(new DateTime(2018,11,05), 4,"oggi", 111);
            List<Giorno> giorni = cont.SearchCommessa("oggi",111);
			Assert.IsNotNull(giorni);
			Assert.IsTrue(giorni.Count == 3);
		}
    }
}