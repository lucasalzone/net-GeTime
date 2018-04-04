using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GeTime {
    public partial class ConntrollerTimeSheet{
        public List<Giorno> SearchCommessa(string nomeCommessa,int id){ 
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

        public void InsertCommessa(string nome, string descrizione, int capienza){ 
            SqlConnection con = new SqlConnection (GetConnection());
            try{ 
                 con.Open();
                SqlCommand cmd = new SqlCommand("InsertCommessa",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome",System.Data.SqlDbType.NVarChar).Value = nome;
                cmd.Parameters.Add("@descrizione",System.Data.SqlDbType.NVarChar).Value = descrizione;
                cmd.Parameters.Add("@capienza",System.Data.SqlDbType.NVarChar).Value = capienza;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }catch(Exception e){ 
                throw e;    
            }finally{ 
                con.Dispose();    
            }
        }

        public bool CompilaHL(DateTime giorno, int HL,string commessa,int id){ 
            SqlConnection con = new SqlConnection(GetConnection());
            try{
                con.Open();
                SqlCommand cmd = new SqlCommand("AddHL",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@giono",System.Data.SqlDbType.Date).Value = giorno;
                cmd.Parameters.Add("@nOre",System.Data.SqlDbType.Int).Value = HL;
                cmd.Parameters.Add("@idU",System.Data.SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@commessa",System.Data.SqlDbType.NVarChar).Value = commessa;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }catch(SqlException e){ 
                throw new Exception (e.Message);
            }catch(Exception e){ 
                throw e;    
            }finally{ 
                con.Dispose();    
            }
        }
        
    }        
    public partial class Commessa {
            
        public Commessa(string nome, string descrizione, int capienza){ 
            this._nome = nome;
            this._descrizione = descrizione;
            this._capacita = capienza;
        }
         public Commessa(string nome,int capienza){ 
            this._nome = nome;
            //this._descrizione = descrizione;
            this._capacita = capienza;
        }
    }
}

   
