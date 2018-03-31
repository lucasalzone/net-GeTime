using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GeTime
{
	public partial  class ConntrollerTimeSheet
	{
		 public bool CompilaHM(DateTime giorno, int HM, int id){
            SqlConnection connection = new SqlConnection(GetConnection());
            try{
                connection.Open();
               
                SqlCommand command = new SqlCommand("AddHM", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Ore",System.Data.SqlDbType.Int).Value=HM;
				command.Parameters.Add("@Giorno",System.Data.SqlDbType.Date).Value=giorno.ToString("yyyy-MM-dd");
				command.Parameters.Add("@Utenti",System.Data.SqlDbType.Int).Value = id;
				int a = command.ExecuteNonQuery();
                if (a == 0){
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
		public bool CompilaHP(DateTime giorno,int HP,int id)
		{
			SqlConnection connection = new SqlConnection(GetConnection());
            try{
                connection.Open();
               
                SqlCommand command = new SqlCommand("AddHP", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Ore",System.Data.SqlDbType.Int).Value=HP;
				command.Parameters.Add("@Giorno",System.Data.SqlDbType.Date).Value=giorno.ToString("yyyy-MM-dd");
				command.Parameters.Add("@Utenti",System.Data.SqlDbType.Int).Value = id;
				int a = command.ExecuteNonQuery();
                if (a == 0){
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
	}
}
