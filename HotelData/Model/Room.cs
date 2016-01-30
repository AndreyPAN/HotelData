using System.Data;

namespace HotelData.Model
{
	public class Room
	{

		MySQL sql;
		public long id_room { get; private set; }
		public string room { get; private set; }
		public int beds { get; private set; }
		public string floor { get; private set; }
		public long step { get; private set; }
		public string info { get; private set; }

		public Room(MySQL sql)
		{
			this.sql = sql;
		}

		public void SetRoom (string room)
		{
			this.room = room;
		}
		public void SetBeds (int beds)
		{
			this.beds = beds;
		}

		public void SetFloor (string floor)
		{
			this.floor = floor;
		}

		public void SetInfo (string info)
		{
			this.info = info;
		}


		/// <summary>
		/// Получение списка комнат;
		/// </summary>
		/// <returns></returns>
		public DataTable SelectRooms()

		{
			DataTable room;
			do room  = sql.Select("select id_room, room, beds, floor, step, info "+
			"from Room "+
			"order by step;");

			while (sql.SqlError());
			return room;

		}

		/// <summary>
		///Внесение данных о новой комнате в базу; 
		/// </summary>
		public bool InsertRoom()
		{
			string query = "INSERT INTO Room(room, beds, floor, info) " +
			"VALUES ('" + sql.addslashes(room) + 
			"', '" + sql.addslashes(beds.ToString()) + 
			"', '" + sql.addslashes(floor) +
			"', '" + sql.addslashes(info) + "');";

			do this.id_room = sql.Insert(query);

			while (this.sql.SqlError());

			do sql.Update("UPDATE Room " +
		"set step=" + this.id_room.ToString()+
		" where id_room=" + this.id_room.ToString() +
		" LIMIT 1;");

			while (sql.SqlError());
			if (id_room <= 0)
				return false;
			return true;

		}

		/// <summary>
		/// <Room>Получение информации по заданной комнате;
		/// </summary>
		/// <param name="id_room">N of room</param>
		/// <returns> True успешно, False -ошибка </returns>
		public bool SelectRoom(long id_room)
		{
			DataTable room;
			do room = sql.Select("select id_room, room, beds, floor, step, info" +
			" from Room where " +
		"id_room ='" + sql.addslashes( id_room.ToString()) + "';");

			while (sql.SqlError());
			if (room.Rows.Count == 0)
				return false;

			this.id_room = long.Parse(room.Rows[0]["id_room"].ToString());
			this.room = room.Rows[0]["room"].ToString();
			this.beds = int.Parse(room.Rows[0]["beds"].ToString());
			this.floor = room.Rows[0]["floor"].ToString();
			this.step = long.Parse( room.Rows[0]["step"].ToString());
			this.info = room.Rows[0]["info"].ToString();

			return true;


		}

		/// <summary>
		/// Изменение\дополнение данных о комнате;
		/// </summary>
		/// <param name="id_room"></param>
		/// <returns>true - успешно  false - нет </returns>
		public bool UpdateRoom(long id_room)
		{

			if (id_room <= 0)
				return false;
			int result;
			do result = sql.Update("UPDATE Room " +
			"set room='" +     sql.addslashes(this.room) + "', " +
			"beds='" +         sql.addslashes(this.beds.ToString()) + "', " +
			"floor='" +        sql.addslashes(this.floor) + "', " +
			"info='" +         sql.addslashes(this.info) + "' " +
			"where id_room=" + sql.addslashes(id_room.ToString()) + 
			" Limit 1;");

			while (sql.SqlError());


			if (result == 0)
				return false;
			return true;
		}

		/// <summary>
		/// Удаление комнаты из базы данных.
		/// </summary>
		/// <param name="id_room"></param>
		/// <returns></returns>
		public bool DeleteRoom(long id_room)
		{
			int result = 0;
			do result = sql.Update(
			"Delete from room where id_room=" + sql.addslashes(id_room.ToString()) +
			" limit 1 ;");
			while (sql.SqlError());

			return result == 1;
		}



		//		ModelRoom.ShiftRoomUp (int id_room)
		//ModelRoom.ShiftRoomDown (int id_room)
		//** <Room>Перенос комнаты вверх или вниз по списку поиска;

		//		select step from Room where

		//		//

	}
}
