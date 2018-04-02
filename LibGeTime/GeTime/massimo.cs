using System;
using System.Data.SqlClient;

namespace GeTime
{
	public partial  class ConntrollerTimeSheet
	{
		 public bool CompilaHM(DateTime giorno, int HM, int id){
			parameters[0].Value = HM;
			parameters[1].Value = giorno.ToString("yyyy-MM-dd");
			parameters[2].Value = id;
			try {
				int a = ExecNonQProcedure("AddHM", parameters);
				if (a == 0) {
					return false;
				}
				return true;
			} catch (Exception) {
				return false;
			}
        }
		public bool CompilaHP(DateTime giorno,int HP,int id)
		{
			parameters[0].Value = HP;
			parameters[1].Value = giorno.ToString("yyyy-MM-dd");
			parameters[2].Value = id;
			try {
				int a = ExecNonQProcedure("AddHP", parameters);
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
