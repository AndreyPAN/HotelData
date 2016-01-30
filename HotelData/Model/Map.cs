using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace HotelData.Model
{
	public class Map
	{
		MySQL sql;
		long id_room;
		long id_book;
		DateTime day_calendar;

		public string status { get; private set; }
		public int adults { get; private set; }
		public int children { get; private set; }

		public void SetStatus(string status)
		{
			this.status = status;
		}

		public void setAdults(int adults)
		{
			this.adults = adults;
		}

		public void SetChildren(int children)
		{
			this.children = children;
		}

		/// <summary>
		/// Конструктор класса Map
		/// </summary>
		/// <param name="sql"></param>
		public Map(MySQL sql)
		{

			this.sql = sql;
			id_room = -1;
			id_book = -1;
			day_calendar = DateTime.MinValue;
		}
/// <summary>
/// Получение карты загрузки отеля по параметрам №комнаты, даты, № бронировки
/// </summary>
/// <param name="id_room"></param>
/// <param name="id_book"></param>
/// <param name="day_calendar"></param>
		public void SelectMap(long id_room, long id_book, DateTime day_calendar) // Я добавил возвр значение, и изменил для теста было void
		{
			this.id_room = id_room;
			this.id_book = id_book;
			this.day_calendar = day_calendar;

			DataTable mapRow;
			do mapRow = sql.Select("select status, adults, children " +
			" from Map where " +
			"id_room='" + sql.addslashes(id_room.ToString()) + "' " +
			"AND id_book='" + sql.addslashes(id_book.ToString()) + "' " +
			"AND day_calendar='" + sql.DateToString(this.day_calendar) + "';"

		);

			while (sql.SqlError());

			if (mapRow.Rows.Count == 0)
			{
				InsertMapNone();
			}

			else
			{
				this.status = mapRow.Rows[0]["status"].ToString();
				this.adults = int.Parse(mapRow.Rows[0]["adults"].ToString());
				this.children = int.Parse(mapRow.Rows[0]["children"].ToString());
			}

			//return mapRow;
		}

		/// <summary>
		/// Метод, инициирующий значения при отсутствии данных по запросу для метода SelectMap (служебный)
		/// </summary>
		private void InsertMapNone()
		{
			this.status = "NONE";
			this.adults = 0;
			this.children = 0;
			InsertMap();

		}

		/// <summary>
		/// Добавление записи в карту  отеля
		/// </summary>
		public void InsertMap()
		{

			do this.sql.Insert(
			@"INSERT INTO Map
		set id_room=" + this.id_room + @",
		id_book=" + this.id_book + @",
		day_calendar=" + this.sql.DateToString(this.day_calendar) + @",
		status=" + this.status + @",
		adults=" + this.adults + @",
		children=" + this.children + @";"
			);
			while (this.sql.SqlError());
		}


		/// <summary>
		/// Получение карты отеля в промежутке между заданными датами
		/// </summary>
		/// <param name="from_day"></param>
		/// <param name="till_day"></param>
		/// <returns></returns>
		public DataTable SelectMap(DateTime from_day, DateTime till_day)
		{
			DataTable map;
			do map = sql.Select(
			@"select id_room, id_book, day_calendar, status, adults, children
			from Map
			where day_calendar BETWEEN '"+
			sql.DateToString(from_day) + "' AND '"+
			sql.DateToString(till_day) + "';"
			);
			 while (this.sql.SqlError()) ;

			return map;
		}

		/// <summary>
		/// Удаление записи из карты отеля
		/// </summary>
		public void DeleteMap ()
		{
			if (id_room < 0) return;
			if (id_book < 0) return;
			if (day_calendar == DateTime.MinValue) return;
			do sql.Update(
			"DELETE" +
				" from Map where " +
				"id_room='" + sql.addslashes(id_room.ToString()) + "' " +
				"AND id_book='" + sql.addslashes(id_book.ToString()) + "' " +
				"AND day_calendar='" + sql.DateToString(this.day_calendar) + "'"+
				" LIMIT 1;"
			);
			while (sql.SqlError());
		}


		/// <summary>
		/// Изменение записи в карте отеля
		/// </summary>
		public void UpdateMap ()
		{
			do sql.Update(
				"UPDATE MAP" +
					" SET status=" + sql.addslashes(status) +
					" adults=" + adults + ","+
					" children=" + children+ ","+
					" WHERE id_room='" + sql.addslashes(id_room.ToString()) + "' " +
					"AND id_book='" + sql.addslashes(id_book.ToString()) + "' " +
					"AND day_calendar='" + sql.DateToString(this.day_calendar) + "'" +
					" LIMIT 1;"
				);
			while (sql.SqlError());
		}

	}
}
