create procedure CompilaHL







create procedure CompilaHF
	@id int ,
	@giorno data,
	@HF int
AS
	if @@Error >0 or @id is null
		begin
			rollback transaction
			print 'Attenzione Rollback effettuato';
		end
	else
		begin
			insert into







create procedure CompilaHM






create procedure CompilaHP