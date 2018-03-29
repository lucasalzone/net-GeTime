create procedure searchGiorno
	@giorno date,
	@id int
as 
	select G.id, G.giorno,T.acronimo,G.ore, C.id,C.nome,C.descrizione,C.capienza
	from Giorni G left join giorniCommesse GC on G.id=GC.idGiorno 
				left join Commesse C on C.id = GC.idCommessa
				left join TipologiaOre T on G.TipoOre = T.id
	where @id = G.idUtenti and @giorno = G.giorno;
go
	
