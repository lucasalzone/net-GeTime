using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial  class ControllerTimeSheet{

         public void CompilaHF(DateTime giorno, int Hf, int id){
            SqlConnection connection = new SqlConnection(GetConnection()); //da sistemare il GetConnection()
                string sql ="dbo.AddHF";
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@giorno",System.Data.SqlDbType.Date).Value = giorno;
                command.Parameters.Add("@HF",System.Data.SqlDbType.Int).Value = Hf;
                command.Parameters.Add("@id",System.Data.SqlDbType.Int).Value = id;
 
                command.Dispose();
               
                connection.Close();
         }
    } 
}
