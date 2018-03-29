﻿use GeTime

create procedure SearchCommessa
	@nomeCommessa nvarchar,
	@idUtente int
as
	declare @cCommessa int
	select g.giorno, g.ore  from giorno g
		inner join giornoCommessa gc
			on g.id = gc.idGiorno
		inner join commessa c
			on gc.idCommessa = c.Id
		where c.Nome = @nomeCommessa and g.idUtente = @idUtente;
go

create Procedure AddHM
@Ore int,
@Giorno date,
@Utenti int
as

declare @id int ;
set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 4 )
	if id is null 
	Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (4,@ore,@Giorno,@Utenti) 
	else
	Update giorni set Ore=@ore where id=@id;
go;
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
	
