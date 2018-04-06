using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial  class ConntrollerTimeSheet{

		public bool CompilaHF(DateTime giorno, int HP, int id) {
			using (var db = new GeTimeEntities()) {
				int controllo = db.AddHF(HP, giorno, id);
				if (controllo > 0) {
					db.SaveChanges();
					return true;
				} else {
					return false;
				}
			}
		}
	} 
}
