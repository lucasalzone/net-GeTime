using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial  class ConntrollerTimeSheet{

         public bool CompilaHF(DateTime giorno, int Hf, int id){
            SqlConnection connection = new SqlConnection(GetConnection()); //da sistemare il GetConnection()
                
			try { 
                connection.Open();
                SqlCommand command = new SqlCommand("AddHF", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@giorno",System.Data.SqlDbType.Date).Value = giorno.ToString("yyyy-MM-dd");
                command.Parameters.Add("@Ore",System.Data.SqlDbType.Int).Value = Hf;
                command.Parameters.Add("@Utenti",System.Data.SqlDbType.Int).Value = id;
				int a = command.ExecuteNonQuery();
				if(a == 0)
				{
					throw new Exception();
				}

				command.Dispose();
				return true;
                
			}catch (Exception )
			{
				return false;
				
			} 
			finally { 
                connection.Dispose();
			}
         }
    } 
}
