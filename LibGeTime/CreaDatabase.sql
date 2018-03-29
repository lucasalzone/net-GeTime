Create database GeTime;
Create table Utenti(
id int identity(1,1) primary key not null,
);
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
idUtenti int foreign key references Utenti
);
Create Table Commesse(
id int primary key not null,
nome nvarchar(50),
descrizione nvarchar(200),
capienza int 
);
Create Table giorniCommesse(
idGiorno int foreign key references Giorni,
idCommessa int foreign key references Commesse
);

insert into TipologiaOre(descrizione,acronimo) values('Ore lavorate','HL');

insert into TipologiaOre(descrizione,acronimo) values('Ore di ferie','HF');

insert into TipologiaOre(descrizione,acronimo) values('Ore di permesso','HP');

insert into TipologiaOre(descrizione,acronimo) values('Ore di malattia','HM');
