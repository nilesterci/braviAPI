# braviAPI

Banco em PostGRES usando docker com o seguinte comando:

docker run --hostname=c6aa1e7ef43e --mac-address=02:42:ac:11:00:02 --env=POSTGRES_PASSWORD=senha@67 --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/lib/postgresql/15/bin --env=GOSU_VERSION=1.16 --env=LANG=en_US.utf8 --env=PG_MAJOR=15 --env=PG_VERSION=15.3-1.pgdg120+1 --env=PGDATA=/var/lib/postgresql/data --volume=/var/lib/postgresql/data -p 5432:5432 --restart=no --runtime=runc -d postgres

Script de criação das tabelas:

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE person(
	person_id uuid DEFAULT uuid_generate_v4(),
	name varchar(200) not null,
	created_at date not null DEFAULT now(),
	updated_at date,
	birthday date, 
	primary key (person_id)
);

Create type contactType as enum('WhatsApp', 'Email', 'Telephone', 'CellPhone');

CREATE TABLE contact(
 	contact_id uuid DEFAULT uuid_generate_v4(),
	person_id uuid not null, 
	value varchar(50),
	contact_type contactType not null,
	created_at date DEFAULT now(),
	foreign key (person_id) references person(person_id)
);
