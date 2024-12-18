create database myfinance;
go;

use myfinance;

create table category(
	id int identity(1, 1) not null,
	description varchar(50) not null,
	categorytype char(1) not null,
	primary key (id)
)

create table financialtransaction(
	id int identity(1, 1) not null,
	historic text null,
	transactiondate date not null,
	amount decimal(9, 2),
	categoryid int not null,
	primary key(id),
	foreign key(categoryid) references category(id)
);

select * from category;