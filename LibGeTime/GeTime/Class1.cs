using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using LibreriaDB;
namespace GeTime
{
    public interface IConntrollerTimeSheet
    {
        bool CompilaHL(DateTime giorno, int HL,string commessa,int id);
        bool CompilaHF(DateTime giorno, int HF,int id);
        bool CompilaHM(DateTime giorno, int HM, int id);
        bool CompilaHP(DateTime giorno, int HP, int id);
        Giorno SearchGiorno(DateTime dateTime,int id);//Restituisce il giorno di una determinata persona per un tederminato datatime
        List<Giorno> SearchCommessa(string nomeCommessa,int id);//Restituisce la lista di giorni nei quali ho lavorato per quella commessa
         
    }

    public partial class ConntrollerTimeSheet
    {
        private string _dataB;
		private SqlParameter[] parameters = new SqlParameter[3];
		public string DataB { get { return _dataB; }set { _dataB = value; } }
        public ConntrollerTimeSheet(string datab= "GeTime") {
            this._dataB = datab;
			parameters[0] = new SqlParameter("@Ore", System.Data.SqlDbType.Int);
			parameters[1] = new SqlParameter("@Giorno", System.Data.SqlDbType.Date);
			parameters[2] = new SqlParameter("@Utenti", System.Data.SqlDbType.Int);
		}
        public string GetConnection() {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = DataB;
            return builder.ToString();
        }

		private int ExecNonQProcedure(string procedureName, SqlParameter[] sqlParameters) {
			SqlConnection connection = new SqlConnection(GetConnection());
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(procedureName, connection);
				command.CommandType = CommandType.StoredProcedure;
				if (sqlParameters != null) {
					command.Parameters.AddRange(sqlParameters);
				}
				int i = command.ExecuteNonQuery();
				command.Dispose();
				return i;
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}
		public void CompilaHL(DateTime giorno, int HL, int id,string commessa){ 
            SqlConnection con = new SqlConnection(GetConnection());
            try{
                con.Open();
                SqlCommand cmd = new SqlCommand("AddFunzione",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@giono",System.Data.SqlDbType.Date).Value = giorno;
                cmd.Parameters.Add("@HType",System.Data.SqlDbType.Int).Value = HL;
                cmd.Parameters.Add("@commessa",System.Data.SqlDbType.NVarChar).Value = commessa;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }catch(SqlException e){ 
                throw new Exception (e.Message);
            }catch(Exception e){ 
                throw e;    
            }finally{ 
                con.Dispose();    
            }
        }
        //public bool CompilaHF(DateTime giorno, int HF, int id){
        //    SqlConnection connection = new SqlConnection(GetConnection());
        //    try {
        //        string sql;
        //        connection.Open();
        //        Giorno g = SearchGiorno(id, giorno);
        //        if (g == null){
        //            sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}',0,{HF},0,0);";
        //        }
        //        else {
        //            sql = $"update TimeDB set HF = {HF} where id_Utente={id}";
        //        }
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        if (command.ExecuteNonQuery() == 0){
        //            throw new Exception();
        //        }
        //        command.Dispose();
        //        return true;
        //    }
        //    catch (Exception) {
        //        return false;
        //    }
        //    finally {
        //        connection.Close();
        //    }
        //}
        //public bool CompilaHM(DateTime giorno, int HM, int id){
        //    SqlConnection connection = new SqlConnection(GetConnection());
        //    try{
        //        string sql;
        //        connection.Open();
        //        Giorno g = SearchGiorno(id, giorno);
        //        g.HM = HM;
        //        if (g == null) {
        //            sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}'0,0,{HM},0);";
        //        }
        //        else {
        //            sql = $"update TimeDB set HM = {HM} where id_Utente={id}";
        //        }
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        if (command.ExecuteNonQuery() == 0){
        //            throw new Exception();
        //        }
        //        command.Dispose();
        //        return true;
        //    }
        //    catch (Exception) {
        //        return false;
        //    }
        //    finally{
        //        connection.Close();
        //    }
        //}
   //     public bool CompilaHP(DateTime giorno, int HP, int id) {
   //         //SqlConnection connection = new SqlConnection(GetConnection());
   //         //try{
   //         //    string sql;
   //         //    connection.Open();
   //         //    Giorno g = SearchGiorno(id, giorno);
   //         //    if (g == null){
   //         //        sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}'0,0,0,{HP});";
   //         //    }
   //         //    else{
   //         //        sql = $"update TimeDB set HP = {HP} where id_Utente={id}";
   //         //    }
   //         //    SqlCommand command = new SqlCommand(sql, connection);
   //         //    if (command.ExecuteNonQuery() == 0) {
   //         //        throw new Exception();
   //         //    }
   //         //    command.Dispose();
   //         //    return true;
   //         //}
   //         //catch (Exception){
   //         //    return false;
   //         //}
   //         //finally{
   //         //    connection.Close();
   //         //}
			//throw new NotImplementedException();
   //     }

		public static void InitTest(string DBName= "GeTime", string fileName = "CreaDatabase.sql") {
			DB.ExecQFromFile(@"C:\Users\max\source\GitHubRepo\GeTime\net-GeTime\LibGeTime\"+fileName);
			DB.ExecQFromFileProcedure(@"C:\Users\max\source\GitHubRepo\GeTime\net-GeTime\LibGeTime\SqlProcedure.sql", "go", DBName);
			
			//string path = Path.Combine(Environment.CurrentDirectory, @"LibGeTime\", fileName);
		}
		public void Drop()
        {
            SqlConnection con = new SqlConnection(GetConnection());
            try
            {
                con.Open();
                string sql = "delete giorniCommesse;delete Giorni;delete Commesse";
                SqlCommand command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Dispose();
            }
        }
		public void ExecP(string pro){
			SqlConnection con = new SqlConnection(GetConnection());
			try {
				con.Open();
				SqlCommand command = new SqlCommand(pro, con);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.ExecuteNonQuery();
				command.Dispose();
			} catch (Exception e) {
				throw e;
			} finally {
				con.Dispose();
			}
		}
    }

    public partial class Utente
    {
        private int _id;
        public int ID { get { return _id; } }
        public Utente(int id)
        {
            _id = id;
        }
    }

    public enum  HType{HP,HF,HM, HL};

    public class Giorno
    {
        private int _id;
        private int _id_utente;
        private int [] ore = new int[3];
        private DateTime data;
        public DateTime Data { get { return data; }}
        public int ID { get { return _id; }set { _id = value; }}
        public int ID_UTENTE {get { return _id_utente; }set { _id_utente = value; }}

        public int HL { get{ return TotCom();  } }

		public int[] Ore { get => ore; set => ore = value; }
		public List<Commessa> Commesse { get => commesse;}
		private List<Commessa> commesse;
        

        public Giorno(DateTime data){ this.data = data;}
        public Giorno(DateTime data, int HP, int HM, int HF, int id, int id_utente){
            this.data = data;
            Ore[(int)HType.HP] = HP;
            Ore[(int)HType.HM] = HM;
            Ore[(int)HType.HF] = HF;
            _id = id;
            _id_utente = id_utente;
        }

		public void AddCommessa(Commessa com){ 
			if(commesse == null)
				commesse = new List<Commessa>();
			commesse.Add(com);    
        }
        private int TotCom(){ 
            int tot=0;
            foreach(Commessa com in Commesse) {
                tot+=com.OreLavorate;
           }            return tot;
        }
        public override bool Equals(object obj) {
            return data.Equals(((Giorno)obj).data);
        }
        public override int GetHashCode(){
            return base.GetHashCode();
        }
    }

    public class Commessa{ 
        
        private int _id; public int Id { get;set;}
        public int Capacita { get => _capacita; set => _capacita = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int OreLavorate { get => oreLavorate; set => oreLavorate = value; }

        private int oreLavorate;

        private string _nome;
        private int _capacita;
        private string _descrizione;

		public Commessa(int id, int oreLavorate, string nome, int capacita, string descrizione) {
			_id = id;
			this.oreLavorate = oreLavorate;
			_nome = nome;
			_capacita = capacita;
			_descrizione = descrizione;
		}

		public Commessa(string nome) {
			_nome = nome;
		}
		public Commessa(string nome, int capacita, string descrizione) : this(nome) {
			_capacita = capacita;
			_descrizione = descrizione;
		}

		public override bool Equals(object obj) {
            if(this.Nome!=null)
                return this.Nome.Equals(((Commessa)obj).Nome);
            return false;
        }
        public override int GetHashCode(){
            return base.GetHashCode();
        }
    }
}