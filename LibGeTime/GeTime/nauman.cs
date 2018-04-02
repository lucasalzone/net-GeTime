using System;
using System.Data.SqlClient;
using System.Data;
using LibreriaDB;

namespace GeTime {
	public partial class ConntrollerTimeSheet {
		public Giorno SearchGiorno(DateTime dateTime, int id) {
			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@giorno", SqlDbType.Date);
			parameters[0].Value = dateTime.ToString("yyyy-MM-dd");
			parameters[1] = new SqlParameter("@id", SqlDbType.Int);
			parameters[1].Value = id;
			try{ 
				Giorno giorno = DB.ExecQProcedureReader("searchGiorno", TrasformInG,parameters, _dataB);
				if(giorno!=null)
					giorno.ID_UTENTE =id;
				return giorno;
			}catch(SqlException){
				throw new Exception("Errore server");
			}catch(Exception e){
				throw e;
			}
		}
		public Giorno TrasformInG(SqlDataReader data){
			Giorno giorno = null;
			if(data.Read()){
				giorno = new Giorno(data.GetDateTime(1));
				do{
					switch(data.GetString(2)){
						case "HF":
							giorno.Ore[(int)HType.HF] = data.GetInt32(3);
							break;
						case "HM":
							giorno.Ore[(int)HType.HM] = data.GetInt32(3);
							break;
						case "HP":
							giorno.Ore[(int)HType.HP] = data.GetInt32(3);
							break;
						case "HL":
							giorno.AddCommessa(new Commessa(data.GetInt32(4), data.GetInt32(3), data.GetString(5), data.GetInt32(7), data.GetString(6)));
							break;
					}
				}while (data.Read());
			}
			return giorno;
		}
	}
}