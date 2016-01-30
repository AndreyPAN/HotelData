using System;
using System.Data;

namespace HotelData.Model
{
	public class Book
	{
		public long id_book { get; private set; }
		public long id_client { get; private set; }
		public DateTime book_date { get; private set; }
		public DateTime from_day { get; private set; }
		public DateTime till_day { get; private set; }
		public int adults { get; private set; }  //apdetable
		public int children { get; private set; }  //apdetable
		public string status { get; private set; }
		public string info { get; private set; }    //apdetable

		MySQL sql;
		/// <summary>
		/// Конструктор класса резервации
		/// </summary>
		/// <param name="sql">подключение к БД</param>
		public Book(MySQL sql)
		{
			id_book = 0;
			this.sql = sql;
		}


		public void SetAdults(int adults)
		{
			this.adults = adults;
		}

		public void SetChildren(int children)
		{
			this.children = children;
		}

		public void SetInfo(string info)
		{
			this.info = info;
		}

		/// <summary>
		/// Создание резервации
		/// </summary>
		/// <param name="id_client">Для какого клиента</param>
		/// <param name="from_day">С какой даты</param>
		/// <param name="till_day">По какую дату</param>
		/// <returns>True успешно, False - ошибка</returns>
		public bool InsertBook(long id_client, DateTime from_day, DateTime till_day)
		{
			do this.id_book = this.sql.Insert(
					   "INSERT INTO Book " +
					   "SET id_client='" + id_client + "', " +
					   "book_date = date(now()), " +
					   "from_day='" + sql.DateToString(from_day) + "', " +
					   "till_day='" + sql.DateToString(till_day) + "', " +
					   "adults='" + this.adults + "', " +
					   "children='" + this.children + "', " +
					   "status='waiting', " +// ДОДЕЛАТЬ!!!!
					   "info='" + sql.addslashes(this.info) + "';");

			while (sql.SqlError());

			return (this.id_book > 0);
		}

		/// <summary>
		/// Выбор всё о данной резервации
		/// </summary>
		/// <param name="id_book">Номер резервации</param>
		/// <returns>True успешно, False - ошибка</returns>
		public bool SelectBook(long id_book)
		{
			DataTable book;
			this.id_book = id_book;
			do book = sql.Select("select id_client,book_date, from_day, till_day, adults, children, status, b.info" +
			" from Book where " +
		"id_book='" + sql.addslashes(id_book.ToString()) + "';");

			while (sql.SqlError());
			if (book.Rows.Count == 0)
				return false;

			this.id_book = long.Parse(book.Rows[0]["id_book"].ToString());
			this.id_client = long.Parse(book.Rows[0]["id_client"].ToString());
			this.book_date = DateTime.Parse(book.Rows[0]["book_date"].ToString());
			this.from_day = DateTime.Parse(book.Rows[0]["from_day"].ToString());
			this.till_day = DateTime.Parse(book.Rows[0]["till_day"].ToString());
			this.adults = int.Parse(book.Rows[0]["adults"].ToString());
			this.children = int.Parse(book.Rows[0]["children"].ToString());
			this.status = book.Rows[0]["status"].ToString();
			this.info = book.Rows[0]["info"].ToString();

			return true;
		}

		/// <summary>
		/// Редактирование регистрации без дат
		/// </summary>
		/// <param name="id_book">Номер резервации</param>
		/// <returns>True успешно, False - ошибка</returns>
		public bool UpdateBook(long id_book)
		{

			if (this.id_book <= 0)
				return false;

			int result = 0;
			do result = sql.Update("UPDATE Book " +
			"set adults='" + this.adults.ToString() + "', " +
			"children='" + this.children.ToString() + "', " +
			"info='" + sql.addslashes(this.info) + "' " +
			"where id_book=" + sql.addslashes(id_book.ToString()) +
			" Limit 1;");

			while (sql.SqlError());


			if (result == 0)
				return false;
			return true;
		}


		/// <summary>
		/// Изменение статуса бронировки
		/// </summary>
		/// <param name="status"></param>
		/// <returns>True успешно, False - ошибка</returns>
		private bool UpdateStatus(string status)
		{
			if (this.id_book <= 0)
				return false;
			if (status != "waiying" && status != "confirm" && status != "deleted")
				return false;
			int result = 0;
			do result = sql.Update("UPDATE Book " +
			"set status='" + status + "' " +
			"where id_book=" + sql.addslashes(id_book.ToString()) +
			" Limit 1;");

			while (sql.SqlError());

			return (result == 1);


		}

		/// <summary>
		/// Ожидание подтверждения брони
		/// </summary>
		/// <returns>True успешно, False - ошибка</returns>
		public bool SetStatusWaiting()
		{
			return UpdateStatus("waiting");
		}

		/// <summary>
		/// Подтверждение брони
		/// </summary>
		/// <returns>True успешно, False - ошибка</returns>
		public bool SetStatusConfirm()
		{
			return UpdateStatus("confirm");
		}

		/// <summary>
		/// Изменение статуса на удаление бронировки
		/// </summary>
		/// <returns>True успешно, False - ошибка</returns>
		public bool SetStatusDeleted()
		{
			return UpdateStatus("deleted");
		}



		/// <summary>
		/// Изменения начала бронировки
		/// </summary>
		/// <param name="from_day"></param>
		/// <returns>True успешно, False - ошибка</returns>
		public bool UpdateFromDay(DateTime from_day)
		{
			this.from_day = from_day;
			if (this.id_book <= 0)
				return false;
			int result = 0;
			do result = sql.Update("UPDATE Book " +
			"set from_day='" + sql.DateToString(from_day) + "' " +
			"where id_book=" + sql.addslashes(id_book.ToString()) +
			" Limit 1;");

			while (sql.SqlError());

			return (result == 1);
		}

		/// <summary>
		/// Изменение даты окончания бронировки
		/// </summary>
		/// <param name="till_day"></param>
		/// <returns>True успешно, False - ошибка</returns>
		public bool UpdateTillDay(DateTime till_day)
		{
			this.till_day = till_day;
			if (this.id_book <= 0)
				return false;
			int result = 0;
			do result = sql.Update("UPDATE Book " +
			"set till_day='" + sql.DateToString(till_day) + "' " +
			"where id_book=" + sql.addslashes(id_book.ToString()) +
			" Limit 1;");

			while (sql.SqlError());

			return (result == 1);
		}

		/// <summary>
		/// Получение списка всех бронировок
		/// </summary>
		/// <returns>список всех бронировок</returns>
		public DataTable SelectBooks()

		{
			DataTable book;
			do book = sql.Select(
			@"select b.id_client, client, book_date, from_day, till_day, 
			adults, children, status, b.info
			from Book b 
			LEFT JOIN Client c 
			ON b.id_client=c.id_client 
			ORDER BY book_date;");   //@ - многострочная вставка

			while (sql.SqlError());
			return book;

		}
		/// <summary>
		/// Выбор бронировок по фильтру
		/// </summary>
		/// <param name="param"></param>
		/// <returns>отфильтрованные бронировки в таблице</returns>
		public DataTable SelectBook(string param)

		{
			DataTable book;
			string paramSlash = sql.addslashes(param);
			do book = sql.Select(
			@"select b.id_client, client, book_date, from_day, till_day, 
			adults, children, status, b.info
			from Book b 
			LEFT JOIN Client c 
			ON b.id_client=c.id_client 
			where client like '%"+paramSlash+"%' "+
			"or book_date like '"+paramSlash+"%' "+
			"or from_day like '"+paramSlash+"%' "+
			"or till_day  like '"+paramSlash+"%' "+
			"or adults like '"+paramSlash+"' "+
			"or children like '"+paramSlash+"' "+
			"or status like '"+paramSlash+"' "+
			"or b.info like '%"+paramSlash+"%' "+
			"ORDER BY book_date;");   //@ - многострочная вставка

			while (sql.SqlError());
			return book;

		}


	}
}
