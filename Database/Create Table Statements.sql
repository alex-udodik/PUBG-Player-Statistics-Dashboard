CREATE TABLE Match_ (
	matchid_ varchar(100) unique,
	type_ varchar(100),
    createdAt_ timestamp,
	duration_ int,
	iscustommatch_ bool,
	matchtype_ varchar(100),
	gamemode_ varchar(100),
	titleid_ varchar(100),
	shardid_ varchar(100),
	mapname_ varchar(100),
	
	primary key (matchid_)
);

CREATE TABLE Roster_ (
	
	rosterid_ varchar(100) unique,
	matchid_ varchar(100),
	type_ varchar(100),
	rank_ int,
	teamid_ int,
	won_ bool,
	shardid_ varchar(100),
	
	primary key (rosterid_, matchid_),
	foreign key (matchid_) references Match_(matchid_)
);

CREATE TABLE Participant_ (
	
	participantid_ varchar(100) unique,
	rosterid_ varchar(100),
	matchid_ varchar(100),
	
	type_ varchar(100),
	dbnos_ int,
	assists_ int,
	boosts_ int,
	damagedealt_ float(53),
	deathtype_ varchar(100),
	headshotkills_ int,
	heals_ int,
	killplace_ int,
	killstreaks_ int,
	kills_ int,
	name_ varchar(100),
	playerid_ varchar(100),
	revives_ int,
	ridedistance_ int,
	roadkills_ int,
	swimdistance_ int,
	teamkills_ int,
	timesurvived_ float(53),
	vehicledestroys_ int,
	walkdistance_ float(53),
	weaponsacquired_ int,
	winplace_ int,
	actor_ varchar(100),
	shardid_ varchar(100),
	
	primary key (participantid_, rosterid_, matchid_),
	foreign key (matchid_) references Match_(matchid_),
	foreign key (rosterid_) references Roster_(rosterid_)
);

CREATE TABLE ASSET_ (
	assetid_ varchar(100) unique,
	matchid_ varchar(100),
	
	type_ varchar(100),
	createdat_ timestamp,
	url_ varchar(500),
	name_ varchar(100),
	
	primary key (assetid_, matchid_),
	foreign key (matchid_) references Match_ (matchid_)
	
);