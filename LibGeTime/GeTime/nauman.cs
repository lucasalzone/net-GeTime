using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using LibreriaDB;

namespace GeTime {
	public partial class ConntrollerTimeSheet {
		public Giorno SearchGiorno(DateTime dateTime, int idU) {
			using(var db = new GeTimeEntities()){
				List<searchGiorno_Result> result = db.searchGiorno(dateTime, idU).ToList<searchGiorno_Result>();
				return TrasformInG(result,idU);
			}
		}
		public Giorno TrasformInG(List<searchGiorno_Result> listD, int idU){
			Giorno giorno = null;
			int i=0;
			if(listD.Count>i){
				giorno = new Giorno((DateTime)listD[0].giorno);
				List<int> IdList =new List<int>();
				giorno.ID_UTENTE= idU;
				giorno.ID = IdList;
				do { 
					switch (listD[i].acronimo){
						case "HF":
							giorno.Ore[(int)HType.HF] = (int)listD[i].ore;
							break;
						case "HM":
							giorno.Ore[(int)HType.HM] = (int)listD[i].ore;
							break;
						case "HP":
							giorno.Ore[(int)HType.HP] = (int)listD[i].ore;
							break;
						case "HL":
							giorno.AddCommessa(new Commessa((int)listD[i].IdComessa, (int)listD[i].ore, (string)listD[i].nome, (int)listD[i].capienza, (string)listD[i].descrizione));
							break;
					}
					IdList.Add(listD[i++].id);
				}while(listD.Count > i);
			}
			return giorno;
		}
	}
}