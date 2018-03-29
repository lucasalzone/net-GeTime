use GeTime

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

