

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

