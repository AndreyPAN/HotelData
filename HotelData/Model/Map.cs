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
		}

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
			"AND day_calendar='" + sql.DateToString(this.day_calendar) +  "';" 

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

		private void InsertMapNone ()
		{
			this.status = "NONE";
			this.adults = 0;
			this.children = 0;
			InsertMap();

		}

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
	}
}
