create procedure searchCommessa
	@nomeCommessa nvarchar()
	go;


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