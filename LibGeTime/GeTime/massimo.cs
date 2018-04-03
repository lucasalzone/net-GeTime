using System;
using System.Data.SqlClient;
using LibreriaDB;

namespace GeTime
{
	public partial  class ConntrollerTimeSheet
	{
		public bool CompilaHM(DateTime giorno, int HM, int id){
		return Compila(giorno, HM, "AddHM", id);
		}
		public bool CompilaHP(DateTime giorno,int HP,int id)
		{
			return Compila(giorno,HP,"AddHP",id);
		}
		public bool Compila(DateTime giorno, int HOre,string Htype, int id) {
			parameters[0].Value = HOre;
			parameters[1].Value = giorno.ToString("yyyy-MM-dd");
			parameters[2].Value = id;
			try {
				int a = DB.ExecNonQProcedure(Htype, parameters,_dataB);
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
