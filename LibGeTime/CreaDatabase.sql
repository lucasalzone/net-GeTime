Create database GeTime;
Create table TipologiaOre(
id int identity(1,1) primary key not null,
descrizione nvarchar(50),
acronimo char(2)
);
Create table Giorni(
id int identity (1,1) not null primary key,
TipoOre int foreign key references TipologiaOre,
ore int,
giorno date,
idUtenti int
);
Create Table Commesse(
id int identity (1,1) primary key not null,
nome nvarchar(50),
descrizione nvarchar(200),
capienza int 
);
Create Table giorniCommesse(
	id int identity (1,1) primary key not null,
	idGiorno int foreign key references Giorni,
	idCommessa int foreign key references Commesse
);

insert into TipologiaOre(descrizione,acronimo) values('Ore lavorate','HL');

insert into TipologiaOre(descrizione,acronimo) values('Ore di ferie','HF');

insert into TipologiaOre(descrizione,acronimo) values('Ore di permesso','HP');

insert into TipologiaOre(descrizione,acronimo) values('Ore di malattia','HM');

drop Database GeTime

