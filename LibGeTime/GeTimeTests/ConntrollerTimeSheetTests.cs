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
        private ConntrollerTimeSheet cont = new ConntrollerTimeSheet("TestTimeSheet");
        [TestMethod()]
		public void CompilaHLTest()
		{
			bool cavallo = cont.CompilaHL(DateTime.Today, 5, 10);
			Assert.IsTrue(cavallo);
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
			bool cavallo = cont.CompilaHM(DateTime.Today, 5, 10);
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
			ConntrollerTimeSheet.InitTest();
		}
		[TestInitialize]
		public void InitMethod()
		{
            cont.Drop();
		}

		[TestMethod()]
		public void SearchTimeUtente()
		{
			Utente pino = new Utente(9374);
			Assert.IsTrue(cont.SearchTimeUtente(pino.ID).Count == 0);
			DateTime x = DateTime.Now;
			cont.CompilaHF(x.AddDays(-1), 5, pino.ID);
			cont.CompilaHF(x.AddDays(-2), 5, pino.ID);
			cont.CompilaHF(x.AddDays(-3), 5, pino.ID);
			cont.CompilaHF(x.AddDays(-4), 5, pino.ID);
			Assert.IsTrue(cont.SearchTimeUtente(pino.ID).Count == 4);
		}
        [TestMethod]
        public void SearchGiornoTest()
        {
            Utente utente = new Utente(200);
            Assert.IsNull(cont.SearchGiorno(utente.ID, DateTime.Today));
            cont.CompilaHF(DateTime.Today, 5, utente.ID);
            Giorno giorno = cont.SearchGiorno(utente.ID, DateTime.Today);
            Assert.IsTrue(giorno.ID_UTENTE == utente.ID && giorno.Data == DateTime.Today);
        }
    }
}