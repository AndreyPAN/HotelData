ModelClient.InsertClient ()
<Client>Регистрация нового клиента в базе;

INSERT INTO Client (client, e_mail, phone, address, info)
VALUES ('Andrey Panarin', 'andrey.panarin@gmail.com', '+380 4545546', 'address', 'testtest');


ModelClient.SelectClients ()
<Client> Получение списка клиентов ;

select * from Client;

ModelClient.SelectClients (string param)
<Client> Получение списка клиентов по фильтру;

select * from Client where client like '%param%' 
or e_mail like '%param%'
or phone like '%param%'
or address like '%param%'
or info like '%param%'
or id_client = 'param';


ModelClient.SelectClient (int id_client)
<Client> Получение инфы по заданному клиенту;

select id_client, client, e_mail, phone, address, info
from Client 
where id_client='1';


ModelClient.UpdateClient (int id_client)
<Client>Исправление\дополнение данных по существующему клиенту;

UPDATE Client
set client='Gikita Derun',
	e_mail='ta@jec.ww.com',
	phone='380 555 9898989',
	address='world',
	info='afterlife'
	where id_client=3
	LIMIT 1;


ModelCalendar.InsertDays (int year)
<Calendar> Генерация календаря на заданный год;

INSERT IGNORE INTO Calendar
 set day='2016-01-01', 
 weekend='1',	
 holiday='0';


ModelCalendar.SetHoliday (string day)
ModelCalendar.DelHoliday (string day)
<Calendar> Внесение/изменение данных в полях "weekend" и "holiday";

	UPDATE Calendar
	set holiday=1
	where day= '2016-01-01'
	LIMIT 1;


ModelRoom.SelectRooms ()
<Room> Получение списка комнат;

select  id_room, room, beds, floor, step, info
from Room
order by step;


ModelRoom.InsertRoom ()
<Room>Внесение данных о новой комнате в базу;

INSERT INTO Room 
set room= 'test',
beds= 999,
floor='test',
info='test';
UPDATE Room
set step=1
where id_room=1
LIMIT 1;
//
 UPDATE Room
 set step= id_room;  // 

//


ModelRoom.UpdateRoom (int id_room)
<Room>Изменение\дополнение данных о комнате;

UPDATE Room
set room='21 Double',
	beds='2',
	floor='2',
	step='3',
	info='semi lux'
	where id_room=3
	LIMIT 1;


ModelRoom.SelectRoom ( int id_room)
<Room>Получение информации по заданной комнате;

select id_room, room, beds, floor, step, info
 from Room
where id_room=4;


ModelRoom.ShiftRoomUp (int id_room)
ModelRoom.ShiftRoomDown (int id_room)
** <Room>Перенос комнаты вверх или вниз по списку поиска;

select step from Room where 

//


ModelBook.InsertBook ()
<Book> Создание новой бронировки;
INSERT INTO Book
	SET id_client=2,
		book_date = date(now()),
		from_day = '2016-01-01',
		till_day = '2016-01-01',
		adults= 1,
		children =0,
		status = 'wait',
		info= 'Not specified';
	
ModelBook.SelectBook(long id_book)
<Book> Выбор заданной бронировки


waiting
confirm
deleted

ModelBook.UpdateStatus ( int id_book, string status)
<Book> Изменение статуса бронировки 

UPDATE Book
	set status = 'confirm'
	where id_book = 2
	LIMIT 1;


ModelBook.UpdateBook (int id_book)
<Book> Редактирование регистрации без дат

UPDATE Book
	set adults= 1,
		children = 1,
		info= 'Not'
		where id_book = 2
		LIMIT 1; 



ModelBook.UpdateFromDay (int id_book, string day)
ModelBook.UpdateTillDay (int id_book, string day)
<Book> Редактирование дат регистрации 

UPDATE Book
set from_day= '2016-01-01',
	till_day = '2016-01-01'
	where id_book=2
	LIMIT 1;


ModelBook.SelectBooks ()
<Book>  Получение списка бронировок

select c.id_client, client, book_date, from_day, till_day, 
adults, children, status, b.info 
from Book b   
LEFT JOIN Client c 
ON b.id_client=c.id_client 
ORDER BY book_date;                   
--where id_book=2;

ModelBook.SelectBook ( string param)
<Book>  Получение списка бронировок по фильтру

select b.id_client, client, book_date, from_day, till_day, 
adults, children, status, b.info 
from Book b   
LEFT JOIN Client c 
ON b.id_client=c.id_client                    
where client like '%param%'
	or book_date like '%param%'
	or from_day like '%param%'
	or till_day  like '%param%'
	or adults like 'param'
	or children like 'param'
	or status like 'param'
	or b.info like '%param%';



ModelMap.SelectMap (string from_day, string till_day)
<Map> Получение карты (загруженност бронировки) отеля за период;

select id_room, id_book, day_calendar, status, adults, children
 from Map
 where day_calendar BETWEEN '2015-12-01' AND '2016-01-01';



ModelMap.InsertMap ()
<Map> Формирование (внесение) на основе конкретной бронировки списка комнат, количества человек (взрослых и детей) по каждой комнате;
	INSERT INTO Map
	set id_room = 1,
		id_book=1,
		day_calendar= '2016-01-01',
		status= 'confirm',
		adults =1,
		children = 1;


ModelMap.UpdateMap ()
<Map> Изменить запись в <Map>

UPDATE Map
	set status ='wait',
		adults = 1,
		children = 1
		where id_room = 1
		  AND id_book = 1
		  AND day_calendar = '2016-01-01';



ModelMap.ModelMap (int id_room, int id_book, string day_calendar)

ModelMap.DeleteMap ()
<Map> Удалить запись в <Map>;

DELETE from Map
where id_room = 1
		  AND id_book = 1
		  AND day_calendar = '2016-01-01';



ModelRoom.SelectFreeRooms (string day)
<Map> Получение списка свободных комнат на заданный период;
select  * from Room
where id_room NOT IN (select id_room 
						from Map
						where day_calendar = '2016-01-02');

<Book> // Опционально // создать лог-файл изменений в бронировке с возможностью просмотра данных по дате,времени и информации изменений;
<Map> //Формирование отчётов по каждой бронировке с предоставлением информации по комнатам, датам, количеству людей, обычным и праздничным дням;