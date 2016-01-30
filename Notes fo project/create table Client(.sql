create table Client(
id_client int primary key auto_increment,
client varchar(255),
e_mail varchar(255),
phone varchar(255),
address varchar(255),
info text
) default charset=utf8;

create table Book(
id_book int primary key auto_increment,
id_client int,
book_date datetime,
from_day date,
till_day date,
adults int,
children int,
status varchar(255),
info text,
foreign key (id_client) references Client (id_client)
) default charset=utf8;

create table Room(
id_room int primary key auto_increment,
room varchar(255),
beds int,
floor varchar(255),
step int,
info text
) default charset=utf8;

Alter table Book
ADD foreign key (from_day)
references Calendar (day);


create table Map (
id_room int,
id_book int,
day_calendar date,
status varchar(255),
adults int,
children int,
primary key (id_room, id_book, day_calendar)
) default charset=utf8;
