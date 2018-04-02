using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial  class ConntrollerTimeSheet{

         public bool CompilaHF(DateTime giorno, int Hf, int id){
			parameters[0].Value = Hf;
			parameters[1].Value = giorno.ToString("yyyy-MM-dd");
			parameters[2].Value = id;
			try {
				int a = ExecNonQProcedure("AddHF", parameters);
				if (a == 0) {
					return false;
				}
				return true;
			} catch (Exception) {
				return false;
			}
		}
    } 
}
