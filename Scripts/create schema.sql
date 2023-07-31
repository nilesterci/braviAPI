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