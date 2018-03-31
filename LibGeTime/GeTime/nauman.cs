using System;
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
				command.Parameters.Add("@giorno", SqlDbType.Date).Value = dateTime.ToString("yyyy-MM-dd");
				command.Parameters.Add("@id", SqlDbType.Int).Value = id;
				SqlDataReader data = command.ExecuteReader();
				Giorno giorno = null;
				if(data.Read()){ 
					giorno = new Giorno(dateTime);
					giorno.ID_UTENTE =id;
					do{
						String s = ($"{data.GetInt32(0)}  {data.GetDateTime(1)}  {data.GetString(2)}  {data.GetInt32(3)}");
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
								String s2 = ($"{data.GetInt32(4)},{data.GetString(5)} , {data.GetInt32(7)} , {data.GetString(6)}");
								giorno.AddCommessa(new Commessa(data.GetInt32(4), data.GetInt32(3), data.GetString(5), data.GetInt32(7), data.GetString(6)));
								break;
						}
					}while (data.Read());
				}
				return giorno;
			}catch(SqlException){
				throw new Exception("Errore server");
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}
	}
}