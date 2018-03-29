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
create procedure SearchCommessa
	@nomeCommessa nvarchar,
	@idUtente int
as
	declare @cCommessa int
	set @cCommessa = (select id from commessa c 
							inner join giornoCommessa gc
								on c.id = gc.idCommessa
							inner join giorno g
								on gc.idGiorno = g.giorno
							where c.Nome = @nomeCommessa and g.idUtente = @idUtente);
	select * from giorni g 
		inner join giornoCommessa gc
			on g.Id = gc.idGiorno
		inner join commessa c
			on gc.idCommessa = c.id
		where c.id = @cCommessa;
go;

create Procedure AddHM
@Ore int,
@Giorno date,
@Utenti int
as
declare @id int ;
set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 4 )
	if @id is null 
	Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (4,@ore,@Giorno,@Utenti) 
	else
	Update giorni set Ore=@ore where id=@id;
go;

create procedure AddHF
@Ore int,
@Giorno date,
@Utenti int
as
declare @id int ;
set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 2 )
	if id is null 
	Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (2,@ore,@Giorno,@Utenti) 
	else
	Update giorni set Ore=@ore where id=@id;
go;

create procedure AddHP
@Ore int,
@Giorno date,
@Utenti int
as
declare @id int ;
set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 3 )
	if id is null 
	Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (3,@ore,@Giorno,@Utenti) 
	else
	Update giorni set Ore=@ore where id=@id;
go;
go

create Procedure AddHP
@Ore int,
@Giorno date,
@Utenti int
as

declare @id int ;
set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 3 )
	if @id is null 
	Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (3,@ore,@Giorno,@Utenti) 
	else
	Update giorni set Ore=@ore where id=@id;
go

