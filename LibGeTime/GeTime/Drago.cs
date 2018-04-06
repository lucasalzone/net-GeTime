using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GeTime {
    public partial class ConntrollerTimeSheet{
        public Giorno Converti(SearchCommessa1_Result fun) {
            DateTime data  = (DateTime) fun.giorno; 
            int ore =(int) fun.ore;
            return new Giorno(data,ore);
        }
        public List<Giorno> SearchCommessa(string nomeCommessa,int id) {
            using(var db = new GeTimeEntities()) {
                List<SearchCommessa1_Result> qwe = db.SearchCommessa1(nomeCommessa,id).ToList<SearchCommessa1_Result>();
                return qwe.ConvertAll<Giorno>(new Converter<SearchCommessa1_Result,Giorno>(Converti));
            }
        } 
        public void InsertCommessa(string _nome, string _descrizione, int _capienza){ 
            using (var db = new GeTimeEntities()){
                var commessa = new Commesse{
                    nome = _nome,
                    descrizione = _descrizione,
                    capienza = _capienza 
                };
                db.Commesse.Add(commessa);
                db.SaveChanges();
            }
        }
        public bool CompilaHL(DateTime giorno, int HL,string commessa,int id){ 
            using (var db = new GeTimeEntities()){ 
                try{ 
                    db.AddHL1(giorno,HL,commessa,id);
                    return true;
                }catch (Exception){ 
                    return false;    
                }
            }
        }
        /*
        public List<Giorno> SearchCommessa(string nomeCommessa,int id){ 
            List<Giorno> risGiorno = null;
            SqlConnection con = new SqlConnection(GetConnection());
           try{ 
                risGiorno  = new List<Giorno>();
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SearchCommessa",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@nomeCommessa",System.Data.SqlDbType.NVarChar).Value = nomeCommessa;
                cmd.Parameters.Add("@idUtente",System.Data.SqlDbType.Int).Value = id;
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.TableMappings.Add("Table","Giorni");
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                foreach(DataRow dr in ds.Tables["Giorni"].Rows){ 
                    DateTime data  = (DateTime) dr["giorno"]; 
                    //int tipoOre = (int) dr ["tipoOre"];
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
        */
        /*
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
        */
        /*
        public bool CompilaHL(DateTime giorno, int HL,string commessa,int id){ 
            SqlConnection con = new SqlConnection(GetConnection());
            try{
                con.Open();
                SqlCommand cmd = new SqlCommand("AddHL",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@giorno",System.Data.SqlDbType.Date).Value = giorno.ToString("yyyy-MM-dd");
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
        */
    }     
    public partial class Giorno{ 
        public Giorno(DateTime data, int ore){ 
                
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
            this._capacita = capienza;
        }
    }
}
   
