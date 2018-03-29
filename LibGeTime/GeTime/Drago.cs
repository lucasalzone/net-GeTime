using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeTime {
    public partial class ConntrollerTimeSheet {
        List<Giorno> SearchCommessa(string nomeCommessa,int id){ 
            List<Giorno> risGiorno = null;
            SqlConnection con = new SqlConnection(GetConnection());
           try{ 
                risGiorno  = new List<Giorno>();
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SearchCommessa",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@nomeComemssa",System.Data.SqlDbType.NVarChar).Value = nomeCommessa;
                cmd.Parameters.Add("@id",System.Data.SqlDbType.Int).Value = id;
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.TableMappings.Add("Table","Giorno");
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                foreach(DataRow dr in ds.Tables["Giorno"].Rows){ 
                    DateTime data  = (DateTime) dr["giorno"]; 
                    int tipoOre = (int) dr ["tipoOre"];
                    int ore = (int) dr["ore"];
                    Giorno g =new Giorno(data);
                    if(risGiorno.Contains(g))
                        g= risGiorno[risGiorno.IndexOf(g)];
                    else
                        risGiorno.Add(g);
                    g.AddCommessa(new Commessa(nomeCommessa,ore));
                }
                adp.Dispose();
                cmd.Dispose();
                return risGiorno;
            }catch(Exception e ){ 
                throw e;    
            }finally{ 
                con.Dispose();    
            }
        }
}
