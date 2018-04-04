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

    public partial class ConntrollerTimeSheet : IConntrollerTimeSheet
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
		// drago aggiungi il tuo CompilaHL
		public bool CompilaHL(DateTime giorno, int HL, string commessa, int id) { 
            SqlConnection con = new SqlConnection(GetConnection());
            try{
                con.Open();
                SqlCommand cmd = new SqlCommand("AddHL",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@giono",System.Data.SqlDbType.Date).Value = giorno;
                cmd.Parameters.Add("@HType",System.Data.SqlDbType.Int).Value = HL;
                cmd.Parameters.Add("@commessa",System.Data.SqlDbType.NVarChar).Value = commessa;
                cmd.Parameters.Add("@idU",System.Data.SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }catch(SqlException e){ 
                throw new Exception (e.Message);
            }catch(Exception e){ 
                throw e;    
            }finally{ 
                con.Dispose();    
            }
			return false;
        }
		public static void InitTest(string DBName= "GeTime", string fileName = "CreaDatabase.sql") {
			DB.ExecQFromFile(@"C:\Users\max\source\GitHubRepo\GeTime\net-GeTime\LibGeTime\"+fileName);
			DB.ExecQFromFileProcedure(@"C:\Users\max\source\GitHubRepo\GeTime\net-GeTime\LibGeTime\SqlProcedure.sql", "go", DBName);
			
			//string path = Path.Combine(Environment.CurrentDirectory, @"LibGeTime\", fileName);
		}
		public void ExecP(string pro){
			DB.ExecNonQProcedure(pro,null,_dataB);
		}
	}

    public enum  HType{HP,HF,HM};
    public class Giorno
    {
        private int _id;
        private int _id_utente;
        private int [] ore = new int[3];
        private DateTime data;

        public DateTime Data { get { return data; }}
		private List<Commessa> commesse;

        public int ID_UTENTE {get { return _id_utente; }set { _id_utente = value; }}
        public int ID { get { return _id; }set { _id = value; }}
        public int HL { get{ return TotCom();  } }
		public int[] Ore { get => ore; set => ore = value; }
		public List<Commessa> Commesse { get => commesse;}
        

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

    public partial class Commessa{ 
        
        public int Capacita { get => _capacita; set => _capacita = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int OreLavorate { get => oreLavorate; set => oreLavorate = value; }


        private int _id; public int Id { get;set;}
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