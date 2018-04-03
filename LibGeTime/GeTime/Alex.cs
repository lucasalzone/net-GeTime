using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial  class ConntrollerTimeSheet{

		public bool CompilaHF(DateTime giorno, int HF, int id){
			return Compila(giorno, HF, "AddHF", id);
		}
    } 
}
