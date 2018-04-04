using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GeTime;

namespace GeTime
{
	public partial  class ControllerTimeSheet
	{
		 public bool CompilaHM(DateTime giorno, int HM, int id){
            SqlConnection connection = new SqlConnection(GetConnection());
            try{
                string sql;
                connection.Open();
                Giorno g = SearchGiorno(id, giorno);
                if (g == null) {
                    sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}'0,0,{HM},0);";
                }
                else {
                    sql = $"update TimeDB set HM = {HM} where id_Utente={id}";
                }
                SqlCommand command = new SqlCommand(sql, connection);
                if (command.ExecuteNonQuery() == 0){
                    throw new Exception();
                }
                command.Dispose();
                return true;
            }
            catch (Exception) {
                return false;
            }
            finally{
                connection.Close();
            }
        }
        public bool CompilaHP(DateTime giorno, int HP, int id) {
            SqlConnection connection = new SqlConnection(GetConnection());
            try{
                string sql;
                connection.Open();
                Giorno g = SearchGiorno(id, giorno);
                if (g == null){
                    sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}'0,0,0,{HP});";
                }
                else{
                    sql = $"update TimeDB set HP = {HP} where id_Utente={id}";
                }
                SqlCommand command = new SqlCommand(sql, connection);
                if (command.ExecuteNonQuery() == 0) {
                    throw new Exception();
                }
                command.Dispose();
                return true;
            }
            catch (Exception){
                return false;
            }
            finally{
                connection.Close();
            }
        }

		private Giorno SearchGiorno(int id,DateTime giorno)
		{
			throw new NotImplementedException();
		}

		private string GetConnection()
		{
			throw new NotImplementedException();
		}
	}
}
