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
create Procedure TestSearchGiorno
as
	declare @idC int; 
	declare @idG int;
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(2,2,'2018-03-31',200);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(3,2,'2018-03-31',200);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(4,2,'2018-03-31',200);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(1,2,'2018-03-31',200);
	set @idG =(select IDENT_CURRENT('Giorni'));
	insert into Commesse(nome,descrizione,capienza) values('Prova Nauman','provo di searchGiorno',20);
	set @idC = (select IDENT_CURRENT('Commesse'));
	insert into giorniCommesse(idGiorno,idCommessa) values(@idG,@idC);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(1,2,'2018-03-31',200);
	set @idG =(select IDENT_CURRENT('Giorni'));
	insert into Commesse(nome,descrizione,capienza) values('Prova 2 Nauman','provo2 di searchGiorno',20);
	set @idC = (select IDENT_CURRENT('Commesse'));
	insert into giorniCommesse(idGiorno,idCommessa) values(@idG,@idC);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(2,2,'2018-03-31',201);
	insert into Giorni(TipoOre,ore,giorno,idUtenti) values(1,2,'2018-03-31',201);
		set @idG =(select IDENT_CURRENT('Giorni'));
		insert into Commesse(nome,descrizione,capienza) values('Prova Nauman','provo di searchGiorno',20);
	set @idC = (select IDENT_CURRENT('Commesse'));
		insert into giorniCommesse(idGiorno,idCommessa) values(@idG,@idC);
go
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
go
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
	declare @id int;
	set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 4 and idUtenti=@Utenti);
	if @id is null 
		Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (4,@ore,@Giorno,@Utenti);
	else
	Update giorni set Ore=@ore where id=@id;
go


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
go

create procedure AddHF
	@Ore int,
	@Giorno date,
	@Utenti int
as
	declare @id int;
	set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 2 and idUtenti=@Utenti);
	if @id is null 
		Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (2,@ore,@Giorno,@Utenti);
	else
		Update giorni set Ore=@ore where id=@id;
go
create procedure AddHP
	@Ore int,
	@Giorno date,
	@Utenti int
as
	declare @id int;
	set @id = (Select top 1 id from Giorni where giorno=@giorno and TipoOre = 3 and idUtenti=@Utenti);
	if @id is null 
		Insert into giorni (TipoOre,Ore,Giorno,idUtenti) values (3,@ore,@Giorno,@Utenti); 
	else
		Update giorni set Ore=@ore where id=@id;
go
