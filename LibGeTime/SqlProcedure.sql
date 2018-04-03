
create procedure SearchCommessa
	@nomeCommessa nvarchar(30),
	@idUtente int
as
	select c.nome,c.descrizione,c.capienza from commesse c
		inner join giorniCommesse gc
			on c.id = gc.idCommessa
		inner join giorni g 
			on gc.idGiorno = g.id
		where c.nome = @nomeCommessa and g.idUtenti = @idUtente
go;
use getime
select * from commesse
select * from giorni
exec SearchCommessa 'PrimaCommessa',32

create Procedure InsertCommessa
	@nome nvarchar (20),
	@descrizione nvarchar(200),
	@capienza int
as
	insert into commesse (nome,descrizione,capienza) values (@nome,@descrizione,@capienza)
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


create procedure AddHL
	@giorno date,
	@nOre int,
	@idU int,
	@nomeCommit nvarchar(30)
as
	declare @idGiorno int ;
	declare @idCommessa int;
	set @idCommessa = (select top 1 c.id from commesse c where c.nome = @nomeCommit);

	insert into Giorni (TipoOre,ore,giorno,idUtenti) values (1,@nOre,@giorno,@idU);
	if @@ERROR>0
		throw 52565,'Inserimento fallito',3;
	set @idGiorno = IDENT_CURRENT ('giorni');

	insert into giorniCommesse(idGiorno,idCommessa) values (@idGiorno,@idCommessa);
	if @@ERROR>0
		throw 564464, 'Inserimento giornoCommese fallito', 19;
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