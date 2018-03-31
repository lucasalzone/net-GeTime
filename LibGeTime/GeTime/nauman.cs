using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GeTime {
	public partial class ConntrollerTimeSheet {
		public Giorno SearchGiorno(DateTime dateTime, int id) {
			SqlConnection connection = new SqlConnection(GetConnection());
			try{ 
				connection.Open();
				SqlCommand command = new SqlCommand("searchGiorno", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add("@giorno", SqlDbType.Date).Value = dateTime;
				command.Parameters.Add("@id", SqlDbType.Int).Value = id;
				SqlDataReader data = command.ExecuteReader();
				while(data.Read()){
					Giorno giorno = new Giorno(dateTime);
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
							giorno.AddCommessa(new Commessa(data.GetInt32(4), data.GetString(5), data.GetInt32(7), data.GetString(6)));
							break;
					}
				}
			}catch(SqlException){
				throw new Exception("Errore server");
			}catch(Exception e){
				throw e;
			}
			return null;
		}
	}
}