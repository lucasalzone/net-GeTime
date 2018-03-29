using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GeTime
{
    public interface IControllerTimeSheet
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
        public string DataB { get { return _dataB; }set { _dataB = value; } }
        public ConntrollerTimeSheet(string datab = "TimeSheet"){
            this._dataB = datab;
        }
        public string GetConnection() {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = DataB;
            return builder.ToString();
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
        public bool CompilaHF(DateTime giorno, int HF, int id){
            SqlConnection connection = new SqlConnection(GetConnection());
            try {
                string sql;
                connection.Open();
                Giorno g = SearchGiorno(id, giorno);
                if (g == null){
                    sql = $"insert into TimeDB (id_Utente,giorno,hl,hm,hp,hf) values ({id},'{giorno.ToString("yyyy-MM-dd")}',0,{HF},0,0);";
                }
                else {
                    sql = $"update TimeDB set HF = {HF} where id_Utente={id}";
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
            finally {
                connection.Close();
            }
        }
        public bool CompilaHM(DateTime giorno, int HM, int id){
            SqlConnection connection = new SqlConnection(GetConnection());
            try{
                string sql;
                connection.Open();
                Giorno g = SearchGiorno(id, giorno);
                g.HM = HM;
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

        public Giorno SearchGiorno(int id, DateTime dateTime)
        {
            SqlConnection con = new SqlConnection(GetConnection());
            try
            {
                Giorno g = null;
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, id_Utente, giorno, hl, hm, hp, hf ");
                sql.Append(" from TimeDB ");
                sql.Append($" where id_Utente={id}  and giorno='{dateTime.ToString("yyyy-MM-dd")}';");
                SqlCommand sqlCommand = new SqlCommand(sql.ToString(), con);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.HasRows)
                {
                    sqlData.Read();
                    g = new Giorno(sqlData.GetDateTime(2), sqlData.GetInt32(3), sqlData.GetInt32(5), sqlData.GetInt32(4), sqlData.GetInt32(6), sqlData.GetInt32(0), sqlData.GetInt32(1));
                }
                sqlData.Dispose();
                sqlCommand.Dispose();
                return g;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static void InitTest()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            SqlConnection con = new SqlConnection(builder.ToString());
            try
            {
                con.Open();
                string sql = "drop database if exists TestTimeSheet;";
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                sql = "create database TestTimeSheet;";
                sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                con.Dispose();
                builder.InitialCatalog = "TestTimeSheet";
                con = new SqlConnection(builder.ToString());
                con.Open();
                sql = "CREATE TABLE TimeDB ( id INT identity NOT NULL  PRIMARY KEY, id_Utente INT  NOT NULL, giorno DATE NOT NULL, hl INT  NOT NULL, hm INT  NOT NULL, hp INT  NOT NULL, hf INT  NOT NULL); ";
                sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            catch (Exception ex )
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public void Drop()
        {
            SqlConnection con = new SqlConnection(GetConnection());
            try
            {
                con.Open();
                string sql = "delete TimeDB where 0=0;";
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

        public List<Giorno> SearchTimeUtente(int id)
        {
            DateTime oggi = DateTime.Now;
            List<Giorno> timelapse = new List<Giorno>();
            for (int i = 1; i < 30; i++)
            {
                DateTime data = oggi.AddDays(-i);
                Giorno tmp = SearchGiorno(id, data);
                if (tmp != null) { timelapse.Add(tmp); }
            }
            return timelapse;
        }

        public Giorno SearchMeseStudente(int id, DateTime dateTime)
        {
            Giorno G = null;
            SqlConnection connection = new SqlConnection(GetConnection());
            try
            {
                string query = "select  hf, hl, hm, hp from TimeDB " +
                                        "WHERE id_Utente = " + id + " and giorno =' " + dateTime.ToString() + " ' ; ";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader letto = command.ExecuteReader();
                G = new Giorno(dateTime, letto.GetInt32(1), letto.GetInt32(2), letto.GetInt32(3), letto.GetInt32(4), letto.GetInt32(0), letto.GetInt32(5));
                command.Dispose();
            }
            catch (Exception banana) { throw banana; }
            finally
            {
                connection.Close();
            }
            return G;
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

    public enum  HType{HL,HP,HF,HM};

    public class Giorno
    {
        private int _id;
        private int _id_utente;
        private int [] ore = new int[4];
        private DateTime data;
        public DateTime Data { get { return data; }}
        public int ID { get { return _id; }set { _id = value; }}
        public int ID_UTENTE {get { return _id_utente; }set { _id_utente = value; }}
         
        public int HL { get{ return TotCom();  } }

        private List<Commessa> comme;
        

        public Giorno(DateTime data){ this.data = data;}
        public Giorno (DateTime data, int ore,Commessa commessaS){ 
            this.data = data;
            SearchComm(commessaS.Id).OreLavorate+=ore;
        }
        public Giorno(DateTime data, int HP, int HM, int HF, int id, int id_utente){
            this.data = data;
            ore[(int)HType.HP] = HP;
            ore[(int)HType.HM] = HM;
            ore[(int)HType.HF] = HF;
            _id = id;
            _id_utente = id_utente;
        }
        public Commessa SearchComm(int id){ 
            foreach (Commessa com in comme){ 
                if (com.Id==id)
                    return com;
            } 
            return null;
        }
        public void AddCommessa(Commessa com){ 
            comme.Add(com);    
        }
        private int TotCom(){ 
            int tot=0;
            foreach(Commessa com in comme){
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

        public Commessa(string nome,int capacita,string descrizione) {
            this._nome = nome;
            Capacita = capacita;
            Descrizione = descrizione;
        }

        public Commessa(int id,string nome,int capacita,string descrizione){
            this._nome = nome;
            this._id = id;
            this._capacita = capacita;
            this._descrizione = descrizione;
        }

        public Commessa(int capacita,string descrizione) {
            Capacita = capacita;
            Descrizione = descrizione;
        }

        public override bool Equals(object obj) {
            if(this.Nome!=null)
                return this.Nome.Equals(((Commessa)obj).Nome);
            return false;
        }

        public Commessa(string nome,int oreLavorate) {
            Nome = nome;
            OreLavorate = oreLavorate;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        }//cio
    }
}